using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Custom OnStartup method to load IoC
        /// </summary>
        /// <param name="e"></param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // Base Application method
            base.OnStartup(e);

            // Setup IoC Container
            IocContainer.Setup();

            // Setting the current window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
