using DebtDiary.Core;
using System;

namespace DebtDiary
{
    public interface IApplicationViewModel
    {
        ApplicationPage CurrentPage { get; }
        ApplicationSubpage CurrentSubpage { get; }
        Debtor SelectedDebtor { get; set; }
        TimeSpan FadeInDuration { get; set; }
        TimeSpan FadeOutDuration { get; set; }

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
