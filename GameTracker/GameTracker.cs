using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker
{
    internal class GameTracker
    {
        private readonly IList<GameFolder> _appropriateGameFolders = new List<GameFolder>();

        public IEnumerable<GameFolder> TrackGamesAllDrives()
        {
            //this will move
            string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            IEnumerable<string> gameFoldersDatabase = File.ReadLines($"{wantedPath}\\db.txt");
            foreach (string folderPath in gameFoldersDatabase)
            {
                _appropriateGameFolders.Add(new GameFolder(folderPath));
            }
            List<GameFolder> gameFolders = new List<GameFolder>();

            DriveInfo.GetDrives()
               .AsParallel()
               .ForAll(
                    drive =>
                    {
                        IEnumerable<GameFolder> driveGameFolders = EnumerateDirectoryChildren(drive.RootDirectory);
                        gameFolders.AddRange(driveGameFolders);
                    });

            return gameFolders;
        }

        private IEnumerable<GameFolder> EnumerateDirectoryChildren(DirectoryInfo di)
        {
            ConcurrentBag<GameFolder> gameFolders = new ConcurrentBag<GameFolder>();
            Parallel.ForEach(
                di.EnumerateDirectories(),
                (folder =>
                {
                    if (CheckDirectory(folder))
                    {
                        gameFolders.Add(new GameFolder(folder.FullName));
                    }
                }));
            return gameFolders;
        }

        private bool CheckDirectory(DirectoryInfo directory)
        {
            if (_appropriateGameFolders.Any(folder => folder.Name == directory.Name &&
                                           folder.IsDirectoryInfoParentEqual(directory)))
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
            catch (Exception)
            {
                // ignored
            }

            return false;
        }
    }
}