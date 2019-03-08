using System;
using System.Threading.Tasks;
using Jeuxjeux20.Mvvm;
using Ninject;
using TheMvvmGuys.FindMyGames.Factories;
using TheMvvmGuys.FindMyGames.UI.Interactivity.Modals;

namespace TheMvvmGuys.FindMyGames.ViewModels
{
    public class MainWindowViewModel : PropertyChangedObject
    {
        private ISettings _settings;

        public MainWindowViewModel()
        {
            
        }
        public MainWindowViewModel(ISettings settings)
        {
            _settings = settings;
        }
    }
}