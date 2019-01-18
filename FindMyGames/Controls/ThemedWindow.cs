using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ControlzEx.Behaviors;
using ControlzEx.Controls;
using Microsoft.Xaml.Behaviors;

namespace TheMvvmGuys.FindMyGames.Controls
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
        }

        public ThemedWindow()
        {
            InitializeWindowChromeBehaviors();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= OnLoaded;
            InitializeGlowWindowBehavior();
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
            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
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
        private void InitializeWindowChromeBehaviors()
        {
            {
                var windowChrome = new WindowChromeBehavior
                {
                    IgnoreTaskbarOnMaximize = true,
                    ResizeBorderThickness = new Thickness(5),
                    KeepBorderOnMaximize = true,                  
                };
                Interaction.GetBehaviors(this).Add(windowChrome);
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