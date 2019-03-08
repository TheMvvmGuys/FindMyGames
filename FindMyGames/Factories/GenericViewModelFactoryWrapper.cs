using Jeuxjeux20.Mvvm;

namespace TheMvvmGuys.FindMyGames.Factories
{
    /// <summary>
    /// A wrapper to ensure the abstraction of the view model factory.
    /// </summary>
    /// <typeparam name="TViewModel">The view model type</typeparam>
    public class GenericViewModelFactoryWrapper<TViewModel> : IViewModelFactory<TViewModel> where TViewModel : IViewModel<object>
    {
        private readonly IViewModelFactory _factory;
        public GenericViewModelFactoryWrapper(IViewModelFactory factory)
        {
            _factory = factory;
        }
        public TViewModel Create(object model = null)
        {
            return _factory.Create<TViewModel>(model);
        }
    }

}