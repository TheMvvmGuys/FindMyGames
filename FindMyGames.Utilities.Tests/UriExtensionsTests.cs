using System;
using System.Windows;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheMvvmGuys.FindMyGames.Utilities.Extensions;

namespace TheMvvmGuys.FindMyGames.Utilities.Tests
{
    [TestClass]
    public class UriExtensionsTests
    {
        [TestMethod]
        public void GetLastElement_WindowsPathUri_IsRight()
        { 
            var uri = new Uri("C:/Wow/Nice", UriKind.Absolute);
            Assert.AreEqual("Nice", uri.GetLastElement());
        }
        [TestMethod]
        public void GetLastElement_PackUri_IsRight()
        {
            new Application();
            var uri = new Uri("pack://application:,,,/FindMyGames;component/Views/FirstStartup/PathChooserPage.xaml", UriKind.Absolute);
            Assert.AreEqual("PathChooserPage.xaml", uri.GetLastElement());
        }
    }
}
