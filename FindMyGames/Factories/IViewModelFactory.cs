using Jeuxjeux20.Mvvm;

namespace TheMvvmGuys.FindMyGames.Factories
{
    public interface IViewModelFactory<out TViewModel> where TViewModel : IViewModel<object>
    {
        TViewModel Create(object model = null);
    }
    public interface IViewModelFactory
    {
        T Create<T>(object model = null);
    }
}