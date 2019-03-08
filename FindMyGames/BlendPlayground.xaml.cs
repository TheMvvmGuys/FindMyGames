using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheMvvmGuys.FindMyGames.UI.Controls;
using TheMvvmGuys.FindMyGames.UI.Interactivity.Modals;

namespace TheMvvmGuys.FindMyGames
{
    public partial class BlendPlayground : ThemedWindow
    {
        public BlendPlayground()
        {
            InitializeComponent();
            Loaded += BlendPlayground_Loaded;
        }

        private void BlendPlayground_Loaded(object sender, RoutedEventArgs e)
        {
            WindowModalDisplay = new LoadingModal { Text = "Hi ya" };
        }
    }
}
