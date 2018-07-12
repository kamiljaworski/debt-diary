using System.ComponentModel;

namespace DebtDiary
{
    /// <summary>
    /// View Model Interface
    /// </summary>
    public interface IViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        void OnPropertyChanged(string name);
    }
}
