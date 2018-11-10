using DebtDiary.Core;
using System.Windows.Controls;

namespace DebtDiary
{
    /// <summary>
    /// Interaction logic for SummarySubpage.xaml
    /// </summary>
    public partial class SummarySubpage : Page
    {
        public SummarySubpage()
        {
            IClientDataStore clientDataStore =  IocContainer.Get<IClientDataStore>();
            DataContext = new SummarySubpageViewModel(clientDataStore);
            InitializeComponent();
        }
    }
}
