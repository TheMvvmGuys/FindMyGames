using System.ComponentModel;
using System.Runtime.CompilerServices;
using TheMvvmGuys.FindMyGames.UI.Annotations;

namespace TheMvvmGuys.FindMyGames.UI.Interactivity.Modals
{
    public abstract class ModalDisplay
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        internal virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetProperty<T>(T value, out T field, [CallerMemberName] string name = null)
        {
            field = value;
            OnPropertyChanged(name);
        }
    }
}