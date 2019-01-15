using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameTracker
{
    internal class Program
    {
        //This is all in very early stages - code is ugly. It will involve json soon.
        private readonly static List<Folder> _enumerable = new List<Folder>();

        private static void Main(string[] args)
        {
            IEnumerable<string> a = new List<string>();
            //this will move
            string wantedPath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            a = File.ReadLines($"{wantedPath}\\db.txt");
            foreach (string s in a)
            {
                _enumerable.Add(
                    new Folder
                    {
                        Name = s.Split('/')
                           .LastOrDefault(),
                        FullKnownName = s
                    });
            }

            DirectoryInfo di = new DriveInfo(@"C:\").RootDirectory;
            
            DirectoryInfo di1 = new DriveInfo(@"D:\").RootDirectory;
            DirectoryInfo di2 = new DriveInfo(@"E:\").RootDirectory;
            //Console.WriteLine(
            //    Enumerate(di)
            //       .FullName);
            //Console.WriteLine(
            //    Enumerate(di1)
            //       .FullName);
            Console.WriteLine(
                Enumerate(di2)
                   .FullName);
            Console.Read();
        }

        private static DirectoryInfo Enumerate(DirectoryInfo di)
        {
            DirectoryInfo result = null;
            Parallel.ForEach(
                di.EnumerateDirectories(),
                info =>
                {
                    if (_enumerable
                       .Any(folder => folder.Name == info.Name && folder.IsDirectoryInfoParentEqual(info)))
                    {
                        result = info;
                        return;
                    }

                    try
                    {
                        if (info.EnumerateDirectories()
                           .Any())
                        {
                            Enumerate(info);
                        }
                    }
                    catch (Exception)
                    {
                        //Console.WriteLine(e);
                    }
                });
            return result;
        }

        private class Folder
        {
            public string Name { get; set; }

            /// <summary>
            ///     The full name with all of the parents we know about (if any)
            /// </summary>
            public string FullKnownName { get; set; }

            private IEnumerable<string> Parents => FullKnownName
               .Split('/')
               .Reverse()
               .Skip(1);

            public bool IsDirectoryInfoParentEqual(DirectoryInfo directory)
            {
                var index = 0;
                DirectoryInfo currentDirectory = directory;
                while (GetParentsEquality(currentDirectory))
                {
                    //Go up in the tree
                    currentDirectory = currentDirectory?.Parent;
                    index++;
                }

                return index > 0;
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
}