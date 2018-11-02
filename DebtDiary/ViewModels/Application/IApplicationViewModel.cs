﻿using DebtDiary.Core;
using System;
using System.Threading.Tasks;

namespace DebtDiary
{
    public interface IApplicationViewModel
    {
        ApplicationPage CurrentPage { get; }
        ApplicationSubpage CurrentSubpage { get; }
        TimeSpan FadeInDuration { get; }
        TimeSpan FadeOutDuration { get; }
        TimeSpan SubpageFadeInDuration { get; }
        TimeSpan SubpageFadeOutDuration { get; }

        bool IsPageChanging { get; }
        bool IsSubpageChanging { get; }

        Debtor SelectedDebtor { get; set; }


        /// <summary>
        /// Change current page in the application
        /// </summary>
        void ChangeCurrentPage(ApplicationPage page);

        /// <summary>
        /// Change current subpage in the application
        /// </summary>
        void ChangeCurrentSubpage(ApplicationSubpage subpage);

        /// <summary>
        /// Change asynchronously current subpage in the application
        /// </summary>
        /// <returns><see cref="true"/> when the task is done</returns>
        Task<bool> ChangeCurrentSubpageAsync(ApplicationSubpage subpage);

        /// <summary>
        /// Change asynchronously current page in the application
        /// </summary>
        /// <returns><see cref="true"/> when the task is done</returns>
        Task<bool> ChangeCurrentPageAsync(ApplicationSubpage subpage);
    }
}
