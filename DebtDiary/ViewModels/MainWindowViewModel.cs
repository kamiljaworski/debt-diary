using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebtDiary
{
    /// <summary>
    /// Main Window View Model
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged implementation
        // Doing nothing lambda expression to avoid compiler warning
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        #endregion

        #region Public Properties
        // Minimum width the window can get +4 because of dropshadow and 2pt margin
        public int MinimumWidth { get; set; } = 1284;

        // Minimum height the window can get +4 because of dropshadow and 2pt margin
        public int MinimumHeight { get; set; } = 724;

        public int TitleBarHeight { get; set; } = 35;
        #endregion

    }
}
