using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using Jeuxjeux20.Mvvm;
using Ninject;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using TheMvvmGuys.FindMyGames.Extensions;
using TheMvvmGuys.FindMyGames.Factories;
using TheMvvmGuys.FindMyGames.Providers;
using TheMvvmGuys.FindMyGames.UI.Commands;
using TheMvvmGuys.FindMyGames.UI.Themes;
using TheMvvmGuys.FindMyGames.ViewModels;
using TheMvvmGuys.FindMyGames.Views;
using TheMvvmGuys.FindMyGames.Views.FirstStartup;

namespace TheMvvmGuys.FindMyGames
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static App CurrentAsApp => Current as App;
        private const string ProfileFileName = "OptimizationProfile.profile";


        public App()
        {
            // Multi core JIT 
            ProfileOptimization.SetProfileRoot(AppDomain.CurrentDomain.BaseDirectory);
            ProfileOptimization.StartProfile(ProfileFileName);
        }

        public ThemeColorResourceDictionary ThemeColorsDictionary
            => (ThemeColorResourceDictionary)Resources.MergedDictionaries.FirstOrDefault(r =>
               r is ThemeColorResourceDictionary);

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _container.Get<ISettings>().SaveSettings();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _container = new StandardKernel(new CoreModule(), new AppModule());
            var settings = _container.Get<ISettings>();
            ThemeColorsDictionary.SelectedItem = settings.GetEntry(ThemeColorsDictionary);
            if (true) // TODO: detect
            {
                _container.Get<FirstStartupWindow>().Show();
            }
            else
            {
                // TODO
                _container.Get<MainWindow>().Show();
            }
            base.OnStartup(e);
        }

        public MainWindow GetMainWindow() => _container.Get<MainWindow>(); // well :p

        private IKernel _container;

        /// <summary>
        /// Module to inject components related to view models.
        /// </summary>
        public class CoreModule : NinjectModule
        {
            public override void Load()
            {
                // Bind view models
                Bind<MainWindowViewModel>().ToSelf();
                Bind<WelcomePageViewModel>().ToSelf();
                Bind<IViewModel<Settings>, SettingsViewModel, ISettings>().To<SettingsViewModel>();
                // The next statement wraps the service locator-ish into
                // an abstract factory to make it available as a generic injection.
                // 
                // IViewModelFactory  |  IViewModelFactory<T>
                // myFactory.Get<T>() |  myFactory.Get()
                // Bad!               |  Good, strongly typed (myFactory is IViewModelFactory<SomeViewModel> for example.
                Bind<IViewModelFactory>().ToFactory().WhenInjectedInto(typeof(GenericViewModelFactoryWrapper<>));
                Bind(typeof(IViewModelFactory<>)).To(typeof(GenericViewModelFactoryWrapper<>));
                // Default bindings
                Bind<IThemeCollection>().To<NullThemeCollection>(); // Default themes
                Bind<Settings>().ToSelf().InSingletonScope(); // Default settings
            }
        }
        /// <summary>
        /// Module to inject components related to the desktop app properties.
        /// </summary>
        public class AppModule : NinjectModule
        {
            public override void Load()
            {
                // Settings model
                Rebind<Settings>().ToProvider<SettingsProvider>().InSingletonScope();
                Rebind<IThemeCollection>().ToMethod(_ => CurrentAsApp.ThemeColorsDictionary); // + Themes
                // The main window :o
                Bind<MainWindow>().ToSelf();
                // Pages for the startup window.
                Bind<Page>().To<WelcomePage>().WhenInjectedInto<FirstStartupWindow>();
                Bind<Page>().To<PathChooserPage>().WhenInjectedInto<FirstStartupWindow>();
                Bind<Page>().To<SummaryPage>().WhenInjectedInto<FirstStartupWindow>();
            }
        }
    }
}
