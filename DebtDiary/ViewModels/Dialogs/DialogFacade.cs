namespace DebtDiary
{
    class DialogFacade : IDialogFacade
    {
        public DialogMessage DialogMessage { get; private set; }

        public void OpenDialog(DialogMessage dialogMessage)
        {
            DialogMessage = dialogMessage;
            DialogWindow win = new DialogWindow();
            win.ShowDialog();
        }
    }
}
