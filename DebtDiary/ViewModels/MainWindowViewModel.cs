using DebtDiary.Core;
using System.Windows.Input;
using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Main Window View Model
    /// The only View Model that needs a reference to the Window
    /// because of implementing Window Controlling Commands
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
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
        /// Actual width of the window
        /// </summary>
        public int Width { get; set; } = 1280;

        /// <summary>
        /// Actual height of the window
        /// </summary>
        public int Height { get; set; } = 720;

        /// <summary>
        /// Height of the title bar
        /// </summary>
        public int TitleBarHeight { get; set; } = 35;

        /// <summary>
        /// True if the window should be borderless because it is docked or maximized
        /// </summary>
        public bool Borderless => (_window.WindowState == WindowState.Maximized || _dockPosition != WindowDockPosition.Undocked);

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder => Borderless ? 0 : 3;


        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder);
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

            // Listen out for the window resizing
            _window.StateChanged += (sender, e) =>
            {
                // Fire off events for all properties that are affected by a resize
                WindowResized();
            };

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

            //ApplicationState.MainWindowViewModel = this;
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
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
        }
        #endregion
    }
}
