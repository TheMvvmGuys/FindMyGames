using System.Collections.Generic;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace TheMvvmGuys.FindMyGames.UI
{
    public static class InteractionAttached
    {
        public static readonly DependencyProperty BehaviorsProperty = DependencyProperty.RegisterAttached(
            "Behaviors", typeof(IEnumerable<Behavior>), typeof(InteractionAttached), new FrameworkPropertyMetadata(default(IEnumerable<Behavior>), new PropertyChangedCallback(ChangeTarget)), ValidateValueCallback);

        private static bool ValidateValueCallback(object value) => value is null || value is IEnumerable<Behavior>;
        private static void ChangeTarget(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var b = Microsoft.Xaml.Behaviors.Interaction.GetBehaviors(d);
            b.Clear();
            if (e.NewValue is null) return;
            foreach (var behavior in (IEnumerable<Behavior>)e.NewValue)
            {
                b.Add(behavior);
            }
        }

        public static IEnumerable<Behavior> GetBehaviors(DependencyObject dp)
        {
            return dp.GetValue(BehaviorsProperty) as IEnumerable<Behavior>;
        }

        public static void SetBehaviors(DependencyObject dp, IEnumerable<Behavior> value)
        {
            dp.SetValue(BehaviorsProperty, value);
        }
    }
}