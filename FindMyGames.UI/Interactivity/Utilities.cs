using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace TheMvvmGuys.FindMyGames.UI.Interactivity
{
    public static class Utilities
    {
        public static Window GetWindowRunningOnThread() => Thread.CurrentThread.GetWindowRunningOnThread();
        public static Window GetWindowRunningOnThread(this Thread t)
        {
            var windows = Application.Current.Windows.Cast<Window>().ToList();
            return windows.FirstOrDefault(w => w.Dispatcher == Dispatcher.FromThread(t));
        }
    }
} 