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
            IDialogFacade dialogFacade = IocContainer.Get<IDialogFacade>();
            DataContext = new DialogWindowViewModel(this, dialogFacade);
            InitializeComponent();
        }
    }
}
