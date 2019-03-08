using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TheMvvmGuys.FindMyGames.UI.Themes;
// ReSharper disable ValueParameterNotUsed

namespace TheMvvmGuys.FindMyGames
{
    public abstract class DoNothing
    {
        protected void Nothing() { }
        protected T Nothing<T>(T value = default) => value;
    }
    public class NullThemeCollection : DoNothing, IThemeCollection
    {
        private readonly IEnumerable<ThemeColorEntry> _emptyCollection = Enumerable.Empty<ThemeColorEntry>();
        public IEnumerator<ThemeColorEntry> GetEnumerator() => _emptyCollection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _emptyCollection).GetEnumerator();
        public void Add(ThemeColorEntry _) => Nothing();
        public void Clear() => Nothing();
        public bool Contains(ThemeColorEntry item) => Nothing<bool>();
        public void CopyTo(ThemeColorEntry[] array, int arrayIndex) => Nothing();
        public bool Remove(ThemeColorEntry item) => Nothing<bool>();
        public int Count => Nothing<int>();
        public bool IsReadOnly => Nothing(true);
        public int IndexOf(ThemeColorEntry item) => Nothing<int>();
        public void Insert(int index, ThemeColorEntry item) => Nothing();
        public void RemoveAt(int index) => Nothing();
        public ThemeColorEntry this[int index]
        {
            get => Nothing<ThemeColorEntry>();
            set => Nothing();
        }
        public int SelectedIndex
        {
            get => Nothing<int>();
            set => Nothing();
        }
        public ThemeColorEntry SelectedItem
        {
            get => Nothing<ThemeColorEntry>();
            set => Nothing();
        }
    }
}