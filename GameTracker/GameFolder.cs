﻿using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TheMvvmGuys.GameTracker
{
    public class GameFolder
    {
        public string Name { get; }

        /// <summary>
        /// The full name with all of the parents we know about (if any)
        /// </summary>
        private string FullKnownName { get; }

        private IEnumerable<string> Parents => FullKnownName
           .Split('\\')
           .Reverse()
           .Skip(1);

        public GameFolder(string folderPath)
        {
            Name = Path.GetFileName(folderPath);
            FullKnownName = folderPath;
        }

        public bool IsDirectoryParentEqual(DirectoryInfo directory)
        {
            var index = 0;
            DirectoryInfo currentDirectory = directory;
            while (GetParentsEquality(currentDirectory))
            {
                //Go up in the tree
                currentDirectory = currentDirectory?.Parent;
                index++;
            }

            return index > 0 && index >= Parents.Count();
        }

        private bool GetParentsEquality(DirectoryInfo d)
        {
            if (d?.Parent == null)
            {
                return false;
            }
            return d.Parent?.Name == Parents.First();
        }
    }
}