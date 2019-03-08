namespace TheMvvmGuys.FindMyGames.UI.Interactivity.Modals
{
    public interface IModalInteraction : IInteractionBase
    {
        ModalDisplay ModalDisplay { get; set; } // The data templates will cover them.
    }
}