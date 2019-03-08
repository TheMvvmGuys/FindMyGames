using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using TheMvvmGuys.FindMyGames.Utilities;
namespace TheMvvmGuys.FindMyGames.UI.Commands
{
    public class NavigationPageAggregatorCommand : ICommand
    {
        private bool _subbed;
        public NavigationPageAggregatorCommand(IEnumerable<Page> pages)
        {
            Pages = pages as IList<Page> ?? pages.ToList();
        }

        public NavigationPageAggregatorCommand()
        {

        }
        public IList<Page> Pages { get; set; }

        public bool CanExecute(object parameter)
        {
            if (!(parameter is Frame f)) return false;
            SubscribeIfNotDoneAlready(f);
            return f.CanGoForward || !Equals(f.Content, Pages.LastOrDefault());
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
            var currentPage = Pages.FirstOrDefault(x => Equals(f.Content, x));
            var indexOf = Pages.IndexOf(currentPage);
            var secondElement = Pages[indexOf + 1];
            f.Navigate(secondElement); // Next element
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