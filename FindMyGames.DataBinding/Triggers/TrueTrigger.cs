using System.Windows;
using Microsoft.Xaml.Behaviors;
using DataTrigger = Microsoft.Xaml.Behaviors.Core.DataTrigger;

namespace TheMvvmGuys.FindMyGames.DataBinding.Triggers
{
    public class TrueTrigger : DataTrigger
    {
        public TrueTrigger()
        {
            Binding = true;
        }
    }
}