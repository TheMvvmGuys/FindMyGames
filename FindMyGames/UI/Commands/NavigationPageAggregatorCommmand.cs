using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using TheMvvmGuys.FindMyGames.Extensions;

namespace TheMvvmGuys.FindMyGames.UI.Commands
{
    public class NavigationPageAggregatorCommand : ICommand
    {
        private bool _subbed;
        public NavigationPageAggregatorCommand(IEnumerable<Uri> uris)
        {
            Uris = uris;
        }

        public NavigationPageAggregatorCommand()
        {
            
        }
        public IEnumerable<Uri> Uris { get; set; }

        public IEnumerable<string> RelativeUris
        {
            get => Uris?.Select(u => u.ToString()) ?? Enumerable.Empty<string>();
            set { Uris = value.MakeRelativeUris(); CanExecuteChanged?.Invoke(this, EventArgs.Empty); }
        }

        public bool CanExecute(object parameter)
        {
            if (!(parameter is Frame f)) return false;
            SubscribeIfNotDoneAlready(f);
            return f.CanGoForward || f.CurrentSource.GetLastElement() != Uris.LastOrDefault()?.GetLastElement();
        }
        public void Execute(object parameter)
        {
            if (!(parameter is Frame f)) return;
            SubscribeIfNotDoneAlready(f);
            if (f.CanGoForward)
            {
                f.GoForward();
                return;
            }

            var uriContext = (IUriContext) f;
            if (Uris is IList<Uri> list) // Use the IndexOf if available because performance
            {
                var firstElement = list.First(x => f.CurrentSource.GetLastElement() == x.GetLastElement());
                var indexOf = list.IndexOf(firstElement);
                var secondElement = list[indexOf + 1];
                if (!secondElement.IsAbsoluteUri)
                {
                    Uri.TryCreate(uriContext.BaseUri, secondElement, out secondElement);
                }
                f.Navigate(secondElement); // Next element
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
                return; // Alright :)
            }
            var uri = Uris.SkipWhile(x => f.CurrentSource.GetLastElement() != x.GetLastElement())
                .ElementAt(1);
            if (!uri.IsAbsoluteUri)
            {
                Uri.TryCreate(uriContext.BaseUri, uri, out uri);
            }
            f.Navigate(uri); // Go next.
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SubscribeIfNotDoneAlready(Frame f)
        {
            if (_subbed) return;
            f.Navigated += (_, __) => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            _subbed = true;
        }

        public event EventHandler CanExecuteChanged;
    }
}