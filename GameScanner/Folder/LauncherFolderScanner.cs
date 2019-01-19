using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TheMvvmGuys.GameScanner
{
    public class LauncherFolderScanner
    {
        private readonly IEnumerable<GameFolder> _launcherFolders;
        private readonly List<GameFolder> _gameFolders = new List<GameFolder>();

        private CancellationToken _token;

        public event EventHandler<GameFolderEventArgs> GameFolderFound;

        public LauncherFolderScanner(string launcherFolders)
        {
            _launcherFolders = PathToGameFolders(File.ReadAllLines(launcherFolders));
        }

        public LauncherFolderScanner(IEnumerable<string> launcherFolders)
        {
            _launcherFolders = PathToGameFolders(launcherFolders);
        }

        private IEnumerable<GameFolder> PathToGameFolders(IEnumerable<string> paths) 
            => paths.Select(path => new GameFolder(path));

        private void OnGameFolderFound(GameFolder folder) 
            => GameFolderFound?.Invoke(this, new GameFolderEventArgs(folder));

        public async Task<IEnumerable<GameFolder>> ScanGameFoldersAsync(CancellationToken token = default(CancellationToken))
        {
            _token = token;

            try
            {
                return await Task.Run(() => ScanAllDrives(), token);
            }
            catch (Exception)
            {
                return _gameFolders;
            }
        }

        private IEnumerable<GameFolder> ScanAllDrives()
        {
            GameFolderFound += (sender, args) => _gameFolders.Add(args.Folder);

            List<GameFolder> gameFolders = new List<GameFolder>();

            DriveInfo.GetDrives().AsParallel()
               .WithCancellation(_token)
               .ForAll(
                    drive =>
                    {
                        try
                        {
                            gameFolders.AddRange(EnumerateDirectoryChildren(drive.RootDirectory));
                        }
                        catch (IOException)
                        {
                            //Caused by a drive that can't be read
                        }
                    });
            return gameFolders;
        }

        private IEnumerable<GameFolder> EnumerateDirectoryChildren(DirectoryInfo directory)
        {
            GameFolder ToGameFolder(DirectoryInfo dir)
            {
                _token.ThrowIfCancellationRequested();

                //Check if its an expected gamefolder or random one
                if (!CheckDirectory(dir))
                {
                    return null;
                }

                _token.ThrowIfCancellationRequested();

                var folder = new GameFolder(dir.FullName);
                OnGameFolderFound(folder);

                return folder;
            }

            return directory.EnumerateDirectories()
               .Select(ToGameFolder)
               .Where(folder => folder != null)
               .ToList();
        }

        private bool CheckDirectory(DirectoryInfo directory)
        {
            if (_launcherFolders.Any(
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