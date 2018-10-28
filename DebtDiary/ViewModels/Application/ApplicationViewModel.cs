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
        public TimeSpan FadeInDuration { get; set; } = TimeSpan.FromSeconds(0.8);
        public TimeSpan FadeOutDuration { get; set; } = TimeSpan.FromSeconds(0.6);

        public Debtor SelectedDebtor
        {
            get => _selectedDebtor;
            set
            {
                _selectedDebtor = value;
                
            }
        }

        public bool IsPageChanging { get;  set; } = false;
        public bool IsSubpageChanging { get;  set; } = false;
        #endregion

        #region Public Methods

        /// <summary>
        /// Changes current page in the application
        /// </summary>
        public void ChangeCurrentPage(ApplicationPage page) => ChangeCurrentPageAsync(page);

        /// <summary>
        /// Changes current subpage in the application
        /// </summary>
       public void ChangeCurrentSubpage(ApplicationSubpage subpage) => ChangeCurrentSubpageAsync(subpage);

        public void ChangeCurrentSubpageAndDoAction(ApplicationSubpage subpage, Action action) => ChangeCurrentSubpageAndDoActionAsync(subpage, action);
        #endregion

        #region Private Methods

        /// <summary>
        /// Async version of ChangeCurrentPage interface method
        /// </summary>
        private async void ChangeCurrentPageAsync(ApplicationPage page)
        {
            // Await for page fade out animation
            await Task.Delay(FadeOutDuration);

            // Change the page
            CurrentPage = page;
        }

        /// <summary>
        /// Async version of ChangeCurrentPage interface method
        /// </summary>
        public async void ChangeCurrentSubpageAsync(ApplicationSubpage subpage)
        {
            IsSubpageChanging = true;
            await Task.Delay(FadeOutDuration);
            IsSubpageChanging = false;

            // Change the page
            CurrentSubpage = subpage;
        }


        private async void ChangeCurrentSubpageAndDoActionAsync(ApplicationSubpage subpage, Action action)
        {
            IsSubpageChanging = true;
            // Await for page fade out animation
            await Task.Delay(FadeOutDuration);
            IsSubpageChanging = false;


            // Change the page
            CurrentSubpage = subpage;
            action();
        }
        #endregion
    }
}
