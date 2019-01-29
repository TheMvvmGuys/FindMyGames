using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Jeuxjeux20.Mvvm;
using TheMvvmGuys.FindMyGames.Extensions;
using TheMvvmGuys.FindMyGames.UI.Controls;

namespace TheMvvmGuys.FindMyGames.Views.FirstStartup
{
    public partial class FirstStartupWindow : ThemedWindow
    {
        public FirstStartupWindow()
        {
            InitializeComponent();
#if DEBUG
            InputBindings.Add(new KeyBinding(_openDebugBlendPlayground, Key.D, ModifierKeys.Shift | ModifierKeys.Alt)); // Shift + Alt + D to open the playground window
#endif
        }
#if DEBUG
        private readonly DelegateCommand _openDebugBlendPlayground = new DelegateCommand(() => new BlendPlayground().Show());
#endif
    }
}
