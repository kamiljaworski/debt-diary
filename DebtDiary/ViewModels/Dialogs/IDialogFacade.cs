namespace DebtDiary
{
    public interface IDialogFacade
    {
        /// <summary>
        /// Opens a new dialog window
        /// </summary>
        /// <param name="dialogMessage"><see cref="DialogMessage"/> you want to show</param>
        void OpenDialog(DialogMessage dialogMessage);

        bool IsDialogOpened { get; }

        /// <summary>
        /// Dialog Message of an actual dialog window
        /// </summary>
        DialogMessage DialogMessage { get; }
    }
}
