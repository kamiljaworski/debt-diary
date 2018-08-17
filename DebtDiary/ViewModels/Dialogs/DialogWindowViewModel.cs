using DebtDiary.Core;
using System.Windows.Input;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Dialog Window View Model
    /// </summary>
    public class DialogWindowViewModel : MainWindowViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="window">Window you want to bind this ViewModel with</param>
        public DialogWindowViewModel(Window window) : base(window)
        {
            MinimumWidth = Width = 400;
            MinimumHeight = Height = 280;
        }
    }
}
