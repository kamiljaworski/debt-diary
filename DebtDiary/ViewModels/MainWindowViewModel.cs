using DebtDiary.Core;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System;

namespace DebtDiary
{
    /// <summary>
    /// Main Window View Model
    /// The only View Model that needs a reference to the Window
    /// because of implementing Window Controlling Commands
    /// </summary>
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged public EventHandler

        // Doing nothing lambda expression to avoid compiler warning
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };
        #endregion

        #region Private Members

        /// <summary>
        /// A window that this View Model controls
        /// </summary>
        private Window _window = null;

        /// <summary>
        /// The window resizer helper class that keeps the window size correct
        /// </summary>
        private WindowResizer _windowResizer;

        /// <summary>
        /// The margin around the window
        /// </summary>
        private int _outerMargin = 10;

        /// <summary>
        /// The last known dock position
        /// </summary>
        private WindowDockPosition _dockPosition = WindowDockPosition.Undocked;

        #endregion

        #region Public Properties

        /// <summary>
        /// Minimum width of the window
        /// </summary>
        public int MinimumWidth { get; set; } = 1280;

        /// <summary>
        /// Minimum height of the window
        /// </summary>
        public int MinimumHeight { get; set; } = 720;

        /// <summary>
        /// Height of the title bar
        /// </summary>
        public int TitleBarHeight { get; set; } = 35;

        /// <summary>
        /// Outer margin of the window
        /// </summary>
        public int OuterMargin
        {
            get => _window.WindowState == WindowState.Maximized ? _outerMargin : 0;
            set => _outerMargin = value;
        }

        /// <summary>
        /// The margin around the window
        /// </summary>
        public Thickness OuterMarginThickness => new Thickness(OuterMargin);

        #endregion

        #region Public Commands

        /// <summary>
        /// Command used to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// Command used to maximize/restore the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// Command used to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }
        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="window">Window</param>
        public MainWindowViewModel(Window window)
        {
            _window = window;

            // Create commands
            MinimizeCommand = new RelayCommand(() => _window.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => _window.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => _window.Close());

            // Fix window resize issue
            _windowResizer = new WindowResizer(_window);

            // Listen out for dock changes
            _windowResizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                _dockPosition = dock;

                // Fire off resize events
                WindowResized();
            };
        }


        #endregion

        #region Private Helpers 

        /// <summary>
        /// If the window resizes to a special position (docked or maximized)
        /// this will update all required property change events to set the borders and radius values
        /// </summary>
        private void WindowResized()
        {
            // Fire off events for all properties that are affected by a resize
            OnPropertyChanged(nameof(OuterMargin));
            OnPropertyChanged(nameof(OuterMarginThickness));
        }

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
