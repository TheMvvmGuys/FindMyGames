using System.Windows;
using System.Windows.Controls;

namespace TheMvvmGuys.FindMyGames.UI
{
    public static class ButtonAnimation
    {
        public static readonly DependencyProperty IsHoverAnimationEnabledProperty = DependencyProperty.RegisterAttached(
            "IsHoverAnimationEnabled", 
            typeof(bool), 
            typeof(ButtonAnimation),
            new PropertyMetadata(true));

        public static bool GetIsHoverAnimationEnabled(Button button) =>
            (bool) button.GetValue(IsHoverAnimationEnabledProperty);

        public static void SetIsHoverAnimationEnabled(Button button, bool value) =>
            button.SetValue(IsHoverAnimationEnabledProperty, value);

        public static readonly DependencyProperty ScaleUpProperty = DependencyProperty.RegisterAttached(
            "ScaleUp", 
            typeof(double),
            typeof(ButtonAnimation),
            new FrameworkPropertyMetadata(1.05, FrameworkPropertyMetadataOptions.Inherits));

        public static double GetScaleUp(FrameworkElement element) => (double) element.GetValue(ScaleUpProperty);
        public static void SetScaleUp(FrameworkElement element, double value) => element.SetValue(ScaleUpProperty, value);
    }
}