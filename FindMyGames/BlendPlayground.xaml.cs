using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TheMvvmGuys.FindMyGames.UI.Controls;

namespace TheMvvmGuys.FindMyGames
{
    public partial class BlendPlayground : ThemedWindow
    {
        public BlendPlayground()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToElementState(button.Parent as FrameworkElement, "Hovered", true);
        }
    }
}
