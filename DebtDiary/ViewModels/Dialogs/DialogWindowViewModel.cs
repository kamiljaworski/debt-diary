using System.Windows;

namespace DebtDiary
{
    public class DialogWindowViewModel : MainWindowViewModel
    {
        public DialogMessage DialogMessage { get; private set; } = DialogMessage.None;

        public DialogWindowViewModel(Window window, IDialogFacade dialogFacade) : base(window)
        {
            DialogMessage = dialogFacade.DialogMessage;
            MinimumWidth = Width = 400;
            MinimumHeight = Height = 280;
        }
    }
}
