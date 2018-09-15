using DebtDiary.Core;
using System;

namespace DebtDiary
{
    public interface IApplicationViewModel
    {
        ApplicationPage CurrentPage { get; }
        ApplicationSubpage CurrentSubpage { get; }
        TimeSpan FadeInDuration { get; set; }
        TimeSpan FadeOutDuration { get; set; }

        Debtor SelectedDebtor { get; set; }

        /// <summary>
        /// Changes current page in the application
        /// </summary>
        void ChangeCurrentPage(ApplicationPage page);

        /// <summary>
        /// Changes current subpage in the application
        /// </summary>
        void ChangeCurrentSubpage(ApplicationSubpage subpage);
    }
}
