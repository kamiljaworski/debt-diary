using DebtDiary.Core;
using System;
using System.Threading.Tasks;

namespace DebtDiary
{
    public interface IApplicationViewModel
    {
        ApplicationPage CurrentPage { get; }
        ApplicationSubpage CurrentSubpage { get; }
        TimeSpan FadeInDuration { get; set; }
        TimeSpan FadeOutDuration { get; set; }
        bool IsPageChanging { get; }
        bool IsSubpageChanging { get; set; }

        Debtor SelectedDebtor { get; set; }

        /// <summary>
        /// Changes current page in the application
        /// </summary>
        void ChangeCurrentPage(ApplicationPage page);

        void ChangeCurrentSubpage(ApplicationSubpage subpage);



        void ChangeCurrentSubpageAndDoAction(ApplicationSubpage subpage, Action action);
    }
}
