using System.ComponentModel;

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
    }
}
