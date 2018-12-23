using System;
using System.Windows.Input;

namespace DebtDiary.Core
{
    public class RelayCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// Action that RelayCommand have to run
        /// </summary>
        private Action _action = null;
        #endregion

        #region Default Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="action">Action to run</param>
        public RelayCommand(Action action)
        {
            if (action == null)
                throw new ArgumentNullException();

            _action = action;
        }
        #endregion

        #region ICommand Public EventHandler

        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region ICommand Methods

        /// <summary>
        /// RelayCommand can always be Executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes a commands Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter) => _action();
        #endregion
    }
}
