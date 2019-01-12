using System.Windows;
using TheMvvmGuys.FindMyGames.Views.FirstStartup;

namespace TheMvvmGuys.FindMyGames
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
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
