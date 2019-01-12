using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using TheMvvmGuys.FindMyGames.Extensions;

namespace TheMvvmGuys.FindMyGames.Views.FirstStartup
{
    /// <summary>
    /// Logique d'interaction pour FirstStartupWindow.xaml
    /// </summary>
    public partial class FirstStartupWindow : Window
    {
        public FirstStartupWindow()
        {
            InitializeComponent();
        }
        public NavigationPageAggregatorCommand NextPageCommand { get; } = new NavigationPageAggregatorCommand(PagesUri);
        private static readonly Uri[] PagesUri = new[]
        {
            "WelcomePage.xaml",
            "PathChooserPage.xaml"
        }.MakeRelativeUris();
    }
}
