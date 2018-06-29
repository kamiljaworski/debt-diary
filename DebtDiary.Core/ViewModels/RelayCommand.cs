using System;
using System.Windows.Input;

namespace DebtDiary.Core
{
    /// <summary>
    /// Basic command that runs an action
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// Action that RelayCommand have to run
        /// </summary>
        private Action _action = null;
        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="action">Action to run</param>
        public RelayCommand(Action action) => _action = action;
        #endregion

        #region ICommand Public EventHandler

        // Doing nothing lambda expression to avoid compiler warning
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region ICommand Methods

        /// <summary>
        /// RelayCommand can always be Executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes a commands Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            _action();
        }
        #endregion
    }
}
