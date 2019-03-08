using System.Windows;
using System.Windows.Controls;
using TheMvvmGuys.FindMyGames.UI.Interactivity.Modals;

namespace TheMvvmGuys.FindMyGames.UI.Controls
{
    public class WindowModalOverlay : ContentControl, IModalInteraction
    {
        static WindowModalOverlay()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowModalOverlay), new FrameworkPropertyMetadata(typeof(WindowModalOverlay)));
            VisibilityProperty.OverrideMetadata(typeof(WindowModalOverlay), new FrameworkPropertyMetadata(Visibility.Collapsed));
        }

        public void ShowInteraction()
        {
            // TODO: Add animation.
            Visibility = Visibility.Visible;
        }

        public void CloseInteraction()
        {
            // TODO: Add animation 
            Visibility = Visibility.Collapsed;
        }

        public static readonly DependencyProperty ModalDisplayProperty = DependencyProperty.Register(
            nameof(ModalDisplay), typeof(ModalDisplay), typeof(WindowModalOverlay), new FrameworkPropertyMetadata(ModalDisplayChanged));

        private static void ModalDisplayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((WindowModalOverlay) d).Visibility = e.NewValue == null ? Visibility.Collapsed : Visibility.Visible;
        }

        public ModalDisplay ModalDisplay
        {
            get => (ModalDisplay) GetValue(ModalDisplayProperty);
            set => SetValue(ModalDisplayProperty, value);
        }
    }
}
