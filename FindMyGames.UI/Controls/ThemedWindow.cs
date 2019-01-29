using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using ControlzEx.Behaviors;
using Microsoft.Xaml.Behaviors;

namespace TheMvvmGuys.FindMyGames.UI.Controls
{
    [TemplatePart(Name = "PART_MoveArea", Type = typeof(FrameworkElement))]
    [TemplatePart(Name = "PART_MinimizeButton", Type = typeof(ButtonBase))]
    [TemplatePart(Name = "PART_RestoreButton", Type = typeof(ButtonBase))]
    [TemplatePart(Name = "PART_CloseButton", Type = typeof(ButtonBase))]
    public class ThemedWindow : Window
    {
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
            if (GetTemplateChild("PART_MoveArea") is FrameworkElement moveArea)
            {
                moveArea.MouseDown += MouseDrag;
            }

            if (GetTemplateChild("PART_MinimizeButton") is ButtonBase minimizeButton)
            {
                minimizeButton.Click += Minimize;
            }

            if (GetTemplateChild("PART_RestoreButton") is ButtonBase restoreButton)
            {
                restoreButton.Click += RestoreOrMaximize;
            }

            if (GetTemplateChild("PART_CloseButton") is ButtonBase closeButton)
            {
                closeButton.Click += CloseRequest;
            }
        }

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
    }
}