using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheMvvmGuys.FindMyGames.Views.FirstStartup
{
    /// <summary>
    /// Interaction logic for SummaryPage.xaml
    /// </summary>
    public partial class SummaryPage : Page
    {
        public SummaryPage()
        {
            InitializeComponent();
        }

        private void FinishButtonClick(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window is null) return;
            var mainWindow = App.CurrentAsApp.GetMainWindow();
            mainWindow.Width = window.Width;
            mainWindow.Height = window.Height;
            mainWindow.Top = window.Top;
            mainWindow.Left = window.Left;
            mainWindow.Show();
            window.Close();
        }
    }
}