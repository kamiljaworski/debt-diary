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
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.LoginPage;
        #endregion

        #region Public Methods

        /// <summary>
        /// Method used to navigate pages in the application
        /// </summary>
        /// <param name="page"></param>
        public void GoToPage(ApplicationPage page)
        {
            CurrentPage = page;
        }
        #endregion
    }
}
