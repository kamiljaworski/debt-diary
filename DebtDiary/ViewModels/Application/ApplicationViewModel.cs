using System;
using System.Threading.Tasks;

namespace DebtDiary
{
    /// <summary>
    /// ApplicationViewModel class storing aplication state data
    /// </summary>
    public class ApplicationViewModel : BaseViewModel, IApplicationViewModel
    {
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.LoginPage;

        public TimeSpan FadeInDuration { get; set; } = TimeSpan.FromSeconds(0.8);

        public TimeSpan FadeOutDuration { get; set; } = TimeSpan.FromSeconds(0.6);


        public void ChangeCurrentPage(ApplicationPage page) => ChangeCurrentPageAsync(page);


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
    }
}
