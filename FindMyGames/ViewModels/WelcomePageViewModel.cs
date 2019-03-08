using Jeuxjeux20.Mvvm;

namespace TheMvvmGuys.FindMyGames.ViewModels
{
    public class WelcomePageViewModel : PropertyChangedObject
    {
        public SettingsViewModel Settings { get; }

        public WelcomePageViewModel(SettingsViewModel settings)
        {
            Settings = settings;
        }
    }
}