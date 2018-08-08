using System.Windows;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            DataContext = new DialogWindowViewModel(this);
            InitializeComponent();
        }
    }
}
