using System.Windows;
using TheMvvmGuys.FindMyGames.UI.Controls;
using TheMvvmGuys.FindMyGames.ViewModels;

namespace TheMvvmGuys.FindMyGames.Views
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        private MainWindowViewModel _vm;
        public MainWindow(MainWindowViewModel vm = null)
        {
            InitializeComponent();
            DataContext = _vm ?? new MainWindowViewModel();
        }
    }
}
