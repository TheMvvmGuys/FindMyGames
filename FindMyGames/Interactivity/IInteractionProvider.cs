namespace TheMvvmGuys.FindMyGames.Interactivity
{
    public interface IInteractionProvider<out T> where T : IInteractionBase
    {
        T ProvideInteraction();
    }
}