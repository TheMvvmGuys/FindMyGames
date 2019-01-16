using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TheMvvmGuys.GameTracker
{
    public class GameTracker
    {
        private const int MaximumTasks = 400;

        private readonly IList<GameFolder> _requiredGameFolders = new List<GameFolder>();
        private readonly ConcurrentBag<GameFolder> _gameFolders = new ConcurrentBag<GameFolder>();

        private CancellationToken _token;

        public Task<IEnumerable<GameFolder>> TrackGameFoldersAsync(CancellationToken token = default(CancellationToken))
        {
            try
            {
                _token = token;

                return Task.FromResult(TrackAllDrives());
            }
            catch (Exception)
            {
                //ignored
            }
            //Default result
            return Task.FromResult(Enumerable.Empty<GameFolder>());
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
                        catch (OperationCanceledException)
                        {
                            //ignored
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
            GetParallelQuery(directory.EnumerateDirectories())
               .ForAll(
                    dir =>
                    {
                        if (CheckDirectory(dir))
                        {
                            _gameFolders.Add(new GameFolder(dir.FullName));
                        }
                    });
        }

        private bool CheckDirectory(DirectoryInfo directory)
        {
            if (_requiredGameFolders.Any(
                folder => folder.Name == directory.Name && folder.IsDirectoryInfoParentEqual(directory)))
            {
                return true;
            }

            try
            {
                if (directory.EnumerateDirectories()
                   .Any())
                {
                    EnumerateDirectoryChildren(directory);
                }
            }
            catch (UnauthorizedAccessException e)
            {
            }

            return false;
        }
    }
}