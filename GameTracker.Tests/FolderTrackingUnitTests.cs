using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TheMvvmGuys.GameTracker.Tests
{
    [TestClass]
    public class FolderTrackingUnitTests
    {
        [TestMethod]
        public async Task Test_10SecondTrack()
        {
            var cts = new CancellationTokenSource();
            var gameTracker = new GameFolderTracker(new []{ @"steamapps\common" });
            var actualFolders = new List<GameFolder>();
            gameTracker.GameFolderFound += (sender, e) => actualFolders.Add(e.Folder);
            //cts.CancelAfter(TimeSpan.FromSeconds(30));
            IEnumerable<GameFolder> folders = await gameTracker.TrackGameFoldersAsync(cts.Token);
            
            Assert.IsTrue(folders.Any());
        }
    }
}
