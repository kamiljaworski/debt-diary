using DebtDiary.Core;

namespace DebtDiary.ViewModels
{
    /// <summary>
    /// ApplicationViewModel interface
    /// </summary>
    public interface IApplicationViewModel
    {
        /// <summary>
        /// CurrentPage in the application
        /// </summary>
        ApplicationPage CurrentPage { get; set; }
    }
}
