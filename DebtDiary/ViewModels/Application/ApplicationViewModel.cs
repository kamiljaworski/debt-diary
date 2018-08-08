using System;
using System.Threading.Tasks;

namespace DebtDiary
{
    /// <summary>
    /// ApplicationViewModel class storing aplication state data
    /// </summary>
    public class ApplicationViewModel : BaseViewModel, IApplicationViewModel
    {
        #region IApplicationViewModel implementation

        /// <summary>
        /// Current page in the application
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.LoginPage;

        /// <summary>
        /// Page fade in animation duration
        /// </summary>
        public TimeSpan FadeInDuration { get; set; } = TimeSpan.FromSeconds(0.8);

        /// <summary>
        /// Page fade out animation duration
        /// </summary>
        public TimeSpan FadeOutDuration { get; set; } = TimeSpan.FromSeconds(0.6);

        /// <summary>
        /// Changes current page in the application
        /// </summary>
        /// <param name="page"><see cref="ApplicationPage"/> you want to change to</param>
        public void ChangeCurrentPage(ApplicationPage page) => ChangeCurrentPageAsync(page);
        #endregion

        #region Private methods

        /// <summary>
        /// Async version of ChangeCurrentPage interface method
        /// </summary>
        /// <param name="page"><see cref="ApplicationPage"/> you want to change to</param>
        private async void ChangeCurrentPageAsync(ApplicationPage page)
        {
            // Await for page fade out animation
            await Task.Delay(FadeOutDuration);

            // Change the page
            CurrentPage = page;
        }
        #endregion
    }
}
