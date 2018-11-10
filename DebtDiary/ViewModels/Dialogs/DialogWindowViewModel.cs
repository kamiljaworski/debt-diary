using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Dialog Window View Model
    /// </summary>
    public class DialogWindowViewModel : MainWindowViewModel
    {

        public DialogMessage DialogMessage { get; private set; }

        public DialogWindowViewModel(Window window, IDialogFacade dialogFacade) : base(window)
        {
            DialogMessage = dialogFacade.DialogMessage;
            MinimumWidth = Width = 400;
            MinimumHeight = Height = 280;
        }
    }
}
