using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TheMvvmGuys.GameTracker
{
    public class GameTrackerClient
    {
        private const int MaximumTasks = 512;

        private readonly IList<GameFolder> _requiredGameFolders = new List<GameFolder>();
        private readonly ConcurrentBag<GameFolder> _gameFolders = new ConcurrentBag<GameFolder>();

        private CancellationToken _token;

        public event EventHandler<GameFolderEventArgs> GameFolderFound;

        protected virtual void OnGameFolderFound(GameFolder folder) 
            => GameFolderFound?.Invoke(this, new GameFolderEventArgs(folder));

        public async Task<IEnumerable<GameFolder>> TrackGameFoldersAsync(CancellationToken token = default(CancellationToken))
        {
            _token = token;

            try
            {
                return await Task.Run(() => TrackAllDrives(), token);
            }
            catch (OperationCanceledException )
            {
                return _gameFolders;
            }
        }

        private IEnumerable<GameFolder> TrackAllDrives()
        {
            ReadFromDb();

            GetParallelQuery(DriveInfo.GetDrives())
               .ForAll(
                    drive =>
                    {
                        try
                        {
                            EnumerateDirectoryChildren(drive.RootDirectory);
                        }
                        catch (IOException)
                        {
                            //Caused by a drive that can't be read
                        }
                    });
            return _gameFolders;
        }

        //TODO: use an actual DB / JSON, serialize it on app startup if it doesn't exist and stick it in appdata
        private void ReadFromDb()
        {
            //string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            //IEnumerable<string> gameFoldersDatabase = File.ReadLines($@"..\..\{wantedPath}\db.txt");
            IEnumerable<string> gameFoldersDatabase = new List<string>
            {
                "steamapps/common"
            };

            foreach (string folderPath in gameFoldersDatabase)
            {
                _requiredGameFolders.Add(new GameFolder(folderPath));
            }
        }

        private ParallelQuery<T> GetParallelQuery<T>(IEnumerable<T> enumerable) => enumerable.AsParallel()
           .WithCancellation(_token)
           .WithDegreeOfParallelism(MaximumTasks);

        private void EnumerateDirectoryChildren(DirectoryInfo directory)
        {
                void CheckIfGameFolder(DirectoryInfo dir)
                {
                    _token.ThrowIfCancellationRequested();

                    if (!CheckDirectory(dir))
                    {
                        return;
                    }

                    var folder = new GameFolder(dir.FullName);
                    _gameFolders.Add(folder);
                    OnGameFolderFound(folder);

                    _token.ThrowIfCancellationRequested();
            }

                directory.EnumerateDirectories().AsParallel()
                   .WithCancellation(_token)
                   .WithDegreeOfParallelism(MaximumTasks)
                   .ForAll(CheckIfGameFolder);
        }

        private bool CheckDirectory(DirectoryInfo directory)
        {
            if (_requiredGameFolders.Any(
                folder => folder.Name == directory.Name && folder.IsDirectoryParentEqual(directory)))
            {
                return true;
            }

            CheckDirectoryChildren(directory);
            return false;
        }

        private void CheckDirectoryChildren(DirectoryInfo directory)
        {
            try
            {
                if (directory.EnumerateDirectories()
                   .Any())
                {
                    EnumerateDirectoryChildren(directory);
                }
            }
            catch (UnauthorizedAccessException)
            {
                //ignored
            }
        }
    }
}