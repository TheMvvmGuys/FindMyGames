using System.Windows.Data;

namespace TheMvvmGuys.FindMyGames.Extensions.Xaml
{
    public class AppBinding : Binding
    {
        public AppBinding()
        {
            Source = App.CurrentAsApp;
        }

        public AppBinding(string path) : base(path)
        {
            Source = App.CurrentAsApp;
        }
    }
}