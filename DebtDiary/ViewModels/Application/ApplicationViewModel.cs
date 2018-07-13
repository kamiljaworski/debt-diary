using DebtDiary.Core;

namespace DebtDiary
{
    /// <summary>
    /// ApplicationViewModel class storing aplication state data
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// CurrentPage in the application
        /// </summary>
        public ApplicationPage? CurrentPage { get; private set; } = null;

        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;
        }

        //public string Test { get; set; } = "test";
        #endregion

        public ApplicationViewModel()
        {
            if (CurrentPage == null)
                CurrentPage = ApplicationPage.LoginPage;
        }
    }
}
