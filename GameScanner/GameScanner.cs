using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TheMvvmGuys.GameScanner
{
    public class GameScanner
    {
        private readonly static string[] ForbiddenExecutables = 
        {
            "directx",
            "direct3d",
            "dotnet",
            "visualc",
            "opengl",
            "redist",
            "c++",
            "vulkan",
            "d3d"
        };

        public Task<Dictionary<GameFolder, IEnumerable<ScannedGame>>> ScanLauncherFoldersAsync(IEnumerable<GameFolder> launcherFolders) 
            => Task.Run(() => launcherFolders.ToDictionary(launcherFolder => launcherFolder, ScanGameFolders));
 
        public Task<IEnumerable<ScannedGame>> ScanLauncherFolderAsync(GameFolder launcherFolder) 
            => Task.Run(() => ScanGameFolders(launcherFolder));

        private static IEnumerable<ScannedGame> ScanGameFolders(GameFolder launcherFolder)
        {
            var directoryInfo = new DirectoryInfo(launcherFolder.FullKnownName);

            IEnumerable<ScannedGame> enumerateFiles = directoryInfo.EnumerateDirectories()
               .Select(dir => ScanGameDirectory(dir, launcherFolder))
               .Where(game => game != null);

            return enumerateFiles;
        }

        private static ScannedGame ScanGameDirectory(DirectoryInfo info, GameFolder launcherFolder)
        {
            IEnumerable<FileInfo> executables = info.EnumerateFiles("*.exe", SearchOption.AllDirectories);
            var infos = executables as FileInfo[] ?? executables.ToArray();
            if (!infos.Any())
            {
                return null;
            }
            string executablePath = GetGameExecutable(infos).FullName;

            return new ScannedGame(
                info.Name,
                executablePath,
                new GameFolder(info.FullName),
                launcherFolder);
        }

        private static FileInfo GetGameExecutable(IEnumerable<FileInfo> fileInfos)
        {
            IOrderedEnumerable<FileInfo> orderByDescending = fileInfos.OrderByDescending(fileInfo => fileInfo.Length);
            List<FileInfo> orderedEnumerable = orderByDescending.ToList();
            foreach (FileInfo fileInfo in orderedEnumerable)
            {
                // contains but case insensitive
                bool isDriverInstaller = ForbiddenExecutables.Any(driver => fileInfo.Name.IndexOf(driver, StringComparison.OrdinalIgnoreCase) > 0); 

                if (!isDriverInstaller)
                {
                    return fileInfo;
                }
            }

            return orderedEnumerable.FirstOrDefault();
        }
    }
}