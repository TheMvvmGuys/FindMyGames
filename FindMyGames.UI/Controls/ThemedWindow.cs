using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using ControlzEx.Behaviors;
using Microsoft.Xaml.Behaviors;
using Ninject;
using Ninject.Modules;
using TheMvvmGuys.FindMyGames.UI.Interactivity.Modals;
using TheMvvmGuys.FindMyGames.UI.Utilities;

namespace TheMvvmGuys.FindMyGames.UI.Controls
{
    [TemplatePart(Name = "PART_MoveArea", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_MinimizeButton", Type = typeof(ButtonBase))]
    [TemplatePart(Name = "PART_RestoreButton", Type = typeof(ButtonBase))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(ButtonBase))]
    [TemplatePart(Name = "PART_WindowModal", Type = typeof(WindowModalOverlay))]
    public class ThemedWindow : Window
    {
        private dynamic _templateChild;

        protected dynamic TemplateChild => _templateChild ?? (_templateChild = new NameProvider(GetTemplateChild));
 
        static ThemedWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ThemedWindow), new FrameworkPropertyMetadata(typeof(ThemedWindow)));
            AllowsTransparencyProperty.OverrideMetadata(typeof(ThemedWindow), new FrameworkPropertyMetadata(true));
            WindowStyleProperty.OverrideMetadata(typeof(ThemedWindow), new FrameworkPropertyMetadata(WindowStyle.None));
        }

        public ThemedWindow()
        {
            Loaded += OnLoaded;
            InitializeWindowChromeBehavior();
        }
        
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            InitializeGlowWindowBehavior();
            _windowChrome.KeepBorderOnMaximize = true;
        }

        public override void OnApplyTemplate()
        {
            // Invokes GetTemplateChild("PART_MoveArea")
            if (TemplateChild.MoveArea is FrameworkElement moveArea)
            {
                moveArea.MouseDown += MouseDrag;
            }

            if (TemplateChild.MinimizeButton is ButtonBase minimizeButton)
            {
                minimizeButton.Click += Minimize;
            }

            if (TemplateChild.RestoreButton is ButtonBase restoreButton)
            {
                restoreButton.Click += RestoreOrMaximize;
            }

            if (TemplateChild.CloseButton is ButtonBase closeButton)
            {
                closeButton.Click += CloseRequest;
            }

            if (TemplateChild.WindowModal is WindowModalOverlay windowModal)
            {
                WindowModal = windowModal;
            }
        }

        #region WindowModal
        public static readonly DependencyProperty WindowModalProperty = DependencyProperty.Register(
            "WindowModal", typeof(WindowModalOverlay), typeof(ThemedWindow), new PropertyMetadata(default(WindowModalOverlay)));

        public WindowModalOverlay WindowModal
        {
            get => (WindowModalOverlay)GetValue(WindowModalProperty);
            set => SetValue(WindowModalProperty, value);
        }

        public static readonly DependencyProperty WindowModalDisplayProperty = DependencyProperty.Register(
            "WindowModalDisplay", typeof(ModalDisplay), typeof(ThemedWindow), new FrameworkPropertyMetadata(propertyChangedCallback: WindowModalCallback));

        private static void WindowModalCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is ThemedWindow t)) return;
            if (t.WindowModal is null) return;
            t.WindowModal.ModalDisplay = e.NewValue as ModalDisplay;
        }

        public ModalDisplay WindowModalDisplay
        {
            get => (ModalDisplay)GetValue(WindowModalDisplayProperty);
            set => SetValue(WindowModalDisplayProperty, value);
        }
        #endregion

        #region Window behavior
        private void CloseRequest(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RestoreOrMaximize(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                SystemCommands.RestoreWindow(this);
            else
                SystemCommands.MaximizeWindow(this);
        }

        private void Minimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MouseDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private WindowChromeBehavior _windowChrome;
        private void InitializeWindowChromeBehavior()
        {
            {
                _windowChrome = new WindowChromeBehavior
                {
                    IgnoreTaskbarOnMaximize = false,
                    ResizeBorderThickness = new Thickness(5)
                };
                Interaction.GetBehaviors(this).Add(_windowChrome);
            }
            // TODO: Bindings and dps
        }

        private void InitializeGlowWindowBehavior()
        {
            var glow = new GlowWindowBehavior
            {
                GlowBrush = Brushes.DodgerBlue,
                NonActiveGlowBrush = Brushes.CadetBlue,
                ResizeBorderThickness = new Thickness(5)
            };
            Interaction.GetBehaviors(this).Add(glow);
        }
        #endregion

    }
}