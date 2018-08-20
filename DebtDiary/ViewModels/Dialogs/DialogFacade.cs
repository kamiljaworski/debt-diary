namespace DebtDiary
{
    class DialogFacade : IDialogFacade
    {
        public void OpenDialog()
        {
            DialogWindow win = new DialogWindow();
            win.ShowDialog();
        }
    }
}
