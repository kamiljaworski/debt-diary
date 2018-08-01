using System;
using System.Threading.Tasks;

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

        /// <summary>
        /// Duration of page fade in animation
        /// </summary>
        public TimeSpan FadeInDuration { get; set; } = TimeSpan.FromSeconds(0.8);

        /// <summary>
        /// Duration of page fade out animation
        /// </summary>
        public TimeSpan FadeOutDuration { get; set; } = TimeSpan.FromSeconds(0.6);
        #endregion

        #region Public Methods

        /// <summary>
        /// Method used to navigate pages in the application
        /// </summary>
        /// <param name="page"></param>
        public async void GoToPageAsync(ApplicationPage page)
        {
            // Await for page fade out animation
            await Task.Delay(FadeOutDuration);

            // Change the page
            CurrentPage = page;
        }
        #endregion
    }
}
