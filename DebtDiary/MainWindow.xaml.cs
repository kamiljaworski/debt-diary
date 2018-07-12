using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Setting Data Context
            DataContext = new MainWindowViewModel(this);
            InitializeComponent();
        }
    }
}
