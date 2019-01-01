using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary.Core
{
    public class RelayParameterizedCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// Action that RelayParameterizedCommand have to run
        /// </summary>
        private Func<object, Task> _action = null;
        #endregion

        #region Default Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="action">Action to run</param>
        public RelayParameterizedCommand(Func<object, Task> action)
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
        public bool CanExecute(object parameter) => true;

        /// <summary>
        /// Executes a commands Action
        /// </summary>
        public void Execute(object parameter) => _action(parameter);
        #endregion

        /// <summary>
        /// Executes a command and await for the result
        /// </summary>
        public void ExecuteAndAwait(object parameter) => _action(parameter).GetAwaiter().GetResult();
    }
}
