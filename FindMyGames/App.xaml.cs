using System;
using System.Linq;
using System.Runtime;
using System.Windows;
using System.Windows.Media.Animation;
using TheMvvmGuys.FindMyGames.UI.Themes;
using TheMvvmGuys.FindMyGames.Views;
using TheMvvmGuys.FindMyGames.Views.FirstStartup;

namespace TheMvvmGuys.FindMyGames
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App CurrentAsApp => Current as App;
        private const string ProfileFileName = "OptimizationProfile.profile";

        public App()
        {
            // Multi core JIT 
            ProfileOptimization.SetProfileRoot(AppDomain.CurrentDomain.BaseDirectory);
            ProfileOptimization.StartProfile(ProfileFileName);
        }

        public ThemeColorResourceDictionary ThemeColorsDictionary
            => (ThemeColorResourceDictionary) Resources.MergedDictionaries.FirstOrDefault(r =>
                r is ThemeColorResourceDictionary);
        protected override void OnStartup(StartupEventArgs e)
        {
            if (true) // TODO: detect
            {
                new FirstStartupWindow().Show();
            }
            else
            {
                new MainWindow().Show();
            }
            base.OnStartup(e);
        }
    }
}
