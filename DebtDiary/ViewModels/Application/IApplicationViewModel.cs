using System;

namespace DebtDiary
{
    /// <summary>
    /// Interface for an ApplicationViewModel class
    /// </summary>
    public interface IApplicationViewModel
    {
        /// <summary>
        /// Current page in the application
        /// </summary>
        ApplicationPage CurrentPage { get; }

        /// <summary>
        /// Page fade in animation duration
        /// </summary>
        TimeSpan FadeInDuration { get; set; }

        /// <summary>
        /// Page fade out animation duration
        /// </summary>
        TimeSpan FadeOutDuration { get; set; }

        /// <summary>
        /// Changes current page in the application
        /// </summary>
        /// <param name="page"><see cref="ApplicationPage"/> you want to change to</param>
        void ChangeCurrentPage(ApplicationPage page);
    }
}
