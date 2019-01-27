namespace TheMvvmGuys.GameScanner
{
    public class ScannedGame
    {
        public string Name { get; }

        public string ExecutablePath { get; set; }

        public GameFolder Folder { get; }

        public GameFolder LauncherFolder { get; }

        public ScannedGame(
            string name,
            string executablePath,
            GameFolder folder,
            GameFolder launcherFolder)
        {
            Name = name;
            ExecutablePath = executablePath;
            Folder = folder;
            LauncherFolder = launcherFolder;
        }
    }
}