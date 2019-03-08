using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Jeuxjeux20.Mvvm;
using Ninject;
using TheMvvmGuys.FindMyGames.Extensions;
using TheMvvmGuys.FindMyGames.UI.Commands;
using TheMvvmGuys.FindMyGames.UI.Controls;

namespace TheMvvmGuys.FindMyGames.Views.FirstStartup
{
    public partial class FirstStartupWindow : ThemedWindow
    {
        public static readonly DependencyProperty NextPageCommandProperty = DependencyProperty.Register(
            "NextPageCommand", typeof(NavigationPageAggregatorCommand), typeof(FirstStartupWindow), new PropertyMetadata(null));

        public NavigationPageAggregatorCommand NextPageCommand
        {
            get => (NavigationPageAggregatorCommand) GetValue(NextPageCommandProperty);
            set => SetValue(NextPageCommandProperty, value);
        }

        
        public FirstStartupWindow(IEnumerable<Page> pages)
        {
            NextPageCommand = new NavigationPageAggregatorCommand(pages);

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
