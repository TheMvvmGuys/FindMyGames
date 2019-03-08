using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace TheMvvmGuys.FindMyGames.DataBinding.Markup
{
    public class WindowBinding : Binding
    {
        public WindowBinding()
        {
            Initialize();
        }

        public WindowBinding(string path) : base(path)
        {
            Initialize();
        }

        private void Initialize()
        {
            RelativeSource = new FindAncestor(typeof(Window));        
        }
    }
}