using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Markup;
using TheMvvmGuys.FindMyGames.UI.Annotations;

namespace TheMvvmGuys.FindMyGames.UI.Themes
{
    [ContentProperty(nameof(Themes))]
    public class ThemeColorResourceDictionary : ResourceDictionary, INotifyPropertyChanged, IThemeCollection
    {
        private int _selectedIndex;
        private ThemeColorEntryCollection _themes = new ThemeColorEntryCollection();

        public ThemeColorEntryCollection Themes
        {
            get => _themes;
            set { _themes = value; UpdateColors(true); }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set { _selectedIndex = value; UpdateColors(); OnPropertyChanged(); OnPropertyChanged(nameof(SelectedItem)); }
        }

        public ThemeColorEntry SelectedItem
        {
            get => Themes[_selectedIndex];
            set { SelectedIndex = Themes.IndexOf(value); OnPropertyChanged(); OnPropertyChanged(nameof(SelectedItem)); }
        }

        private void UpdateColors(bool isFirst = false)
        {
            if (Themes is null || Themes.Count == 0)
                throw new InvalidOperationException("No themes have been provided");
            if (SelectedIndex < 0 || SelectedIndex >= Themes.Count)
                throw new IndexOutOfRangeException("The requested index for the theme is out of range.");
            var item = Themes[SelectedIndex];
            Source = item.Uri;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        public void Add(ThemeColorEntry item)
        {
            _themes.Add(item);
        }

        public bool Contains(ThemeColorEntry item)
        {
            return _themes.Contains(item);
        }

        public void CopyTo(ThemeColorEntry[] array, int arrayIndex)
        {
            _themes.CopyTo(array, arrayIndex);
        }

        public bool Remove(ThemeColorEntry item)
        {
            return _themes.Remove(item);
        }

        public int IndexOf(ThemeColorEntry item)
        {
            return _themes.IndexOf(item);
        }

        public void Insert(int index, ThemeColorEntry item)
        {
            _themes.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _themes.RemoveAt(index);
        }

        public ThemeColorEntry this[int index]
        {
            get => _themes[index];
            set => _themes[index] = value;
        }

        IEnumerator<ThemeColorEntry> IEnumerable<ThemeColorEntry>.GetEnumerator()
        {
            return Themes.GetEnumerator();
        }
    }
}