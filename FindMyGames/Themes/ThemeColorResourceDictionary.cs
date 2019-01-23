using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Markup;

namespace TheMvvmGuys.FindMyGames.Themes
{
    [ContentProperty(nameof(Themes))]
    public class ThemeColorResourceDictionary : ResourceDictionary
    {
        private int _selectedIndex;
        private ThemeColorEntryCollection _themes;

        public ThemeColorEntryCollection Themes
        {
            get => _themes;
            set { _themes = value; UpdateColors(true); }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set { _selectedIndex = value; UpdateColors(); }
        }

        public ThemeColorEntry SelectedItem
        {
            get => Themes[_selectedIndex];
            set => SelectedIndex = Themes.IndexOf(value);
        }
        private void UpdateColors(bool isFirst = false)
        {
            if (Themes is null || Themes.Count == 0)
                throw new InvalidOperationException("No themes have been provided");
            if (SelectedIndex < 0 || SelectedIndex >= Themes.Count)
                throw new IndexOutOfRangeException("The requested index for the theme is out of range.");
            if (isFirst)
            {
                // TODO: Settings for themes
            }
            var item = Themes[SelectedIndex];
            Source = item.Uri;
        }
    }
}