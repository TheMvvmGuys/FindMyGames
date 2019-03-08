using System.Collections.Generic;

namespace TheMvvmGuys.FindMyGames.UI.Themes
{
    public interface IThemeCollection : IList<ThemeColorEntry>
    {
        int SelectedIndex { get; set; }
        ThemeColorEntry SelectedItem { get; set; }
    }
}