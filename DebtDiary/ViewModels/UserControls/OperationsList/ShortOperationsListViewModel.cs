using System.Collections.ObjectModel;

namespace DebtDiary
{
    public class ShortOperationsListViewModel
    {
        public ObservableCollection<DebtorsListItemViewModel> Operations { get; set; } = new ObservableCollection<DebtorsListItemViewModel>();

    }
}
