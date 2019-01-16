using System;
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
        public async Task Test1()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(10000));
            var folders = await new GameTracker().TrackGameFoldersAsync(cts.Token);
            Assert.IsFalse(folders.Any());
        }
    }
}
