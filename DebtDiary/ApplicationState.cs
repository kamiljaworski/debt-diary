using DebtDiary.Core;
using System.ComponentModel;

namespace DebtDiary
{
    /// <summary>
    /// Simple temporary class to navigate pages
    /// </summary>
    public static class ApplicationState
    {
        /// <summary>
        /// Reference to MainWindowViewModel
        /// </summary>
        public static MainWindowViewModel MainWindowViewModel { get; set; } = null;
    }
}
