using TheMvvmGuys.FindMyGames.UI.Themes;

namespace TheMvvmGuys.FindMyGames
{
    public class Settings
    {
        public bool IsFirstRun { get; set; }
        public Theme Theme { get; set; } = Theme.Dark;
    }
}
