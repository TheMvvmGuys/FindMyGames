namespace TheMvvmGuys.FindMyGames.UI.Interactivity
{
    public interface IInteractionContainer
    {
        T GetInteraction<T>();
    }
}