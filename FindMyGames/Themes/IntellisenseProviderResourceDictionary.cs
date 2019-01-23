using System;
using System.ComponentModel;
using System.Windows;

namespace TheMvvmGuys.FindMyGames.Themes
{
    public sealed class IntellisenseProviderResourceDictionary : ResourceDictionary
    {
        private Uri _source;

        public new Uri Source
        {
            get
            {
                var defaultUri = new Uri("pack://application:,,,/FindMyGames;component/Themes/Resources/Empty.xaml", UriKind.Absolute);
#if DEBUG
                if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
#else
                if (false)
#endif
                {
                    return defaultUri;
                }
                else
                {
                    return _source;
                }
            }
            set => _source = value;
        }
    }
}