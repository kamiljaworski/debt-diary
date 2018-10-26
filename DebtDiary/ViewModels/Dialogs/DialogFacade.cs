using System.ComponentModel;

namespace DebtDiary
{
    public class DialogFacade : IDialogFacade, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public DialogMessage DialogMessage { get; private set; }
        public bool IsDialogOpened { get; private set; } = false;

        public void OpenDialog(DialogMessage dialogMessage)
        {
            IsDialogOpened = true;
            DialogMessage = dialogMessage;
            DialogWindow win = new DialogWindow();
            win.ShowDialog();
            IsDialogOpened = false;
        }
    }
}
