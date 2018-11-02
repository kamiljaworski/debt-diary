using System;
using System.Threading;
using System.Threading.Tasks;
using DebtDiary.Core;

namespace DebtDiary
{
    public class ApplicationViewModel : BaseViewModel, IApplicationViewModel
    {
        #region Private Members

        private Debtor _selectedDebtor = null;
        #endregion

        #region Public Properties

        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.LoginPage;
        public ApplicationSubpage CurrentSubpage { get; private set; } = ApplicationSubpage.SummarySubpage;
        public TimeSpan FadeInDuration { get; } = TimeSpan.FromSeconds(0.8);
        public TimeSpan FadeOutDuration { get; } = TimeSpan.FromSeconds(0.6);
        public TimeSpan SubpageFadeInDuration { get; } = TimeSpan.FromSeconds(0.4);
        public TimeSpan SubpageFadeOutDuration { get; } = TimeSpan.FromSeconds(0.3);

        public Debtor SelectedDebtor
        {
            get => _selectedDebtor;
            set
            {
                _selectedDebtor = value;

            }
        }

        public bool IsPageChanging { get; set; } = false;
        public bool IsSubpageChanging { get; set; } = false;
        #endregion

        #region Public Methods
        /// <summary>
        /// Change asynchronously current subpage in the application
        /// </summary>
        /// <returns><see cref="true"/> when the task is done</returns>
        public async Task<bool> ChangeCurrentSubpageAsync(ApplicationSubpage subpage)
        {
            IsSubpageChanging = true;
            await Task.Delay(SubpageFadeOutDuration);
            IsSubpageChanging = false;

            CurrentSubpage = subpage;
            return true;
        }

        public async Task<bool> ChangeCurrentPageAsync(ApplicationPage page)
        {
            IsPageChanging = true;
            await Task.Delay(FadeInDuration);
            IsPageChanging = false;

            CurrentPage = page;
            return true;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Async version of ChangeCurrentPage interface method
        /// </summary>
        private async void aChangeCurrentPageAsync(ApplicationPage page)
        {
            // Await for page fade out animation
            await Task.Delay(FadeOutDuration);

            // Change the page
            CurrentPage = page;
        }

        public void ResetCurrentSubpage() => CurrentSubpage = ApplicationSubpage.SummarySubpage;



        #endregion
    }
}
