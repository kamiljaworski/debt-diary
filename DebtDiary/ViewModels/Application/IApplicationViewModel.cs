using DebtDiary.Core;
using System;
using System.Threading.Tasks;

namespace DebtDiary
{
    public interface IApplicationViewModel
    {
        ApplicationPage CurrentPage { get; }
        ApplicationSubpage CurrentSubpage { get; }
        ApplicationSubpage NextSubpage { get; }

        TimeSpan FadeInDuration { get; }
        TimeSpan FadeOutDuration { get; }
        TimeSpan SubpageFadeInDuration { get; }
        TimeSpan SubpageFadeOutDuration { get; }

        bool IsPageChanging { get; }
        bool IsSubpageChanging { get; }

        Debtor SelectedDebtor { get; set; }

        /// <summary>
        /// Reset current subpage to <see cref="ApplicationSubpage.SummarySubpage"/>
        /// </summary>
        void ResetCurrentSubpage();

        /// <summary>
        /// Change asynchronously current subpage in the application
        /// </summary>
        /// <returns><see cref="true"/> when the task is done</returns>
        Task<bool> ChangeCurrentSubpageAsync(ApplicationSubpage subpage);

        /// <summary>
        /// Change asynchronously current page in the application
        /// </summary>
        /// <returns><see cref="true"/> when the task is done</returns>
        Task<bool> ChangeCurrentPageAsync(ApplicationPage page);
    }
}
