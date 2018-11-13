using DebtDiary.Core;
using DebtDiary.DataProvider;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DebtDiary
{
    /// <summary>
    /// Base ViewModel abstract class
    /// </summary>
    public abstract class BaseViewModel : IViewModel
    {
        /// <summary>
        /// Implementation of an INotfiPropertyChanged interface
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name) => PropertyChanged(this, new PropertyChangedEventArgs(name));

        /// <summary>
        /// Runs a command if the updating flag is not set.
        /// If the flag is true (indicating the function is already running) then the action is not run.
        /// If the flag is false (indicating no running function) then the action is run.
        /// Once the action is finished if it was run, then the flag is reset to false
        /// </summary>
        /// <param name="updatingFlag">The boolean property flag defining if the command is already running</param>
        /// <param name="action">The action to run if the command is not already running</param>
        /// <returns></returns>
        protected async Task RunCommandAsync(Expression<Func<bool>> updatingFlag, Func<Task> action)
        {
            // Check if the flag property is true (meaning the function is already running)
            if (updatingFlag.GetPropertyValue())
                return;

            // Set the property flag to true to indicate we are running
            updatingFlag.SetPropertyValue(true);

            try
            {
                // Run the passed in action
                await action();
            }
            finally
            {
                // Set the property flag back to false now it's finished
                updatingFlag.SetPropertyValue(false);
            }
        }

        protected async Task RunCommandAsync(Func<Task> action)
        {
            await action();
        }
    }
}
