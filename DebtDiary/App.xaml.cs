using Ninject;
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

            // Bind IoC
            IKernel kernel = new StandardKernel();
            kernel.Bind<ApplicationViewModel>().To<ApplicationViewModel>();

            // Setting the current window
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}
