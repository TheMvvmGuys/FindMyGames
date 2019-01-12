using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            var drive = new DriveInfo(@"C:\");
            DirectoryInfo di = drive.RootDirectory;
            Console.WriteLine(iterate(di).FullName);
            Console.Read();
        }

        private static DirectoryInfo iterate(DirectoryInfo di)
        {
            DirectoryInfo result = null;
            Parallel.ForEach(
                di.EnumerateDirectories(),
                info =>
                {
                    if (info.Name.Contains("steam"))
                    {
                        result = info;
                        return;
                    }

                    try
                    {
                        if (info.EnumerateDirectories().Any())
                        {
                            iterate(info);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    
                });
            return result;
        }
    }
}