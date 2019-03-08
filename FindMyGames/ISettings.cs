using System.ComponentModel;
using System.Linq;
using TheMvvmGuys.FindMyGames.UI.Themes;

namespace TheMvvmGuys.FindMyGames
{
    public interface ISettings : INotifyPropertyChanged
    {
        Theme Theme { get; set; }
        bool IsFirstRun { get; set; }
        void SaveSettings();
    }

    internal static class SettingsExtensions
    {
        public static ThemeColorEntry GetEntry(this ISettings settings, IThemeCollection themes) 
            => themes.FirstOrDefault(t => t.Theme == settings.Theme);
    }
}