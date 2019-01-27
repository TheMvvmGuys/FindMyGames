using System;

namespace TheMvvmGuys.GameScanner
{
    public class GameFolderEventArgs : EventArgs
    {
        public GameFolder Folder { get; }

        public DateTime DateFound { get; }

        public GameFolderEventArgs(GameFolder folder)
        {
            Folder = folder;
            DateFound = DateTime.Now;
        }
    }
}