namespace DebtDiary
{
    class DialogFacade : IDialogFacade
    {
        private DialogMessage _dialogMessage = DialogMessage.None;


        public DialogMessage DialogMessage => _dialogMessage;

        public void OpenDialog(DialogMessage dialogMessage)
        {
            _dialogMessage = dialogMessage;
            DialogWindow win = new DialogWindow();
            win.ShowDialog();
        }
    }
}
