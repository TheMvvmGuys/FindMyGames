using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheMvvmGuys.GameScanner.Tests
{
    [TestClass]
    public class FolderScanningUnitTests
    {
        [TestMethod]
        public async Task Test_10SecondTrack()
        {
            var cts = new CancellationTokenSource();
            var gameTracker = new LauncherFolderScanner(new []{ @"steamapps\common", @"Origin"});
            var actualFolders = new List<GameFolder>();
            gameTracker.GameFolderFound += (sender, e) => actualFolders.Add(e.Folder);
            cts.CancelAfter(TimeSpan.FromSeconds(20));
            IEnumerable<GameFolder> folders = await gameTracker.ScanDrivesAsync(cts.Token);

            //Assert.IsTrue(folders.Any());

            var gameScanner = new GameScanner();

            var a = await gameScanner.ScanLauncherFoldersAsync(actualFolders);
            
            Assert.IsTrue(a.Any());
        }
    }
}
