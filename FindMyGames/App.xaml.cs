using System.Linq;
using System.Windows;
using TheMvvmGuys.FindMyGames.Themes;
using TheMvvmGuys.FindMyGames.Views.FirstStartup;

namespace TheMvvmGuys.FindMyGames
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App CurrentAsApp => Current as App;

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
