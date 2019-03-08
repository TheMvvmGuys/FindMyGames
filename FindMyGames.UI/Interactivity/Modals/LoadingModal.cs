namespace TheMvvmGuys.FindMyGames.UI.Interactivity.Modals
{
    public class LoadingModal : ModalDisplay
    {
        private string _text;

        public string Text
        {
            get => _text;
            set => SetProperty(value, out _text);
        }
    }
}