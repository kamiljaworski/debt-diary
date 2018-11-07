using DebtDiary.Core;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    /// <summary>
    /// View model for each of debtors list item
    /// </summary>
    public class DebtorsListItemViewModel : BaseViewModel
    {
        #region Private members
        private IApplicationViewModel _applicationViewModel;
        private IDebtorsListViewModel _debtorsListViewModel;
        private Debtor _debtor;
        #endregion

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Initials { get; set; }
        public decimal Debt { get; set; }
        public Color AvatarColor { get; set; }
        public bool IsSelected { get; set; }

        public ICommand OpenDebtorSubpage { get; set; }

        public string FormattedDebt => Helpers.GetFormattedCurrency(Debt);

        private async void OpenDebtorSubpageAsync()
        {
            _applicationViewModel.SelectedDebtor = _debtor;
            _debtorsListViewModel.Update();

            await _applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage);
        }

        public DebtorsListItemViewModel(Debtor debtor, IApplicationViewModel applicationViewModel, IDebtorsListViewModel debtorsListViewModel)
        {
            _debtor = debtor;
            _applicationViewModel = applicationViewModel;
            _debtorsListViewModel = debtorsListViewModel;

            Id = _debtor.Id;
            FullName = _debtor.FullName;
            Initials = _debtor.Initials;
            Debt = _debtor.Debt;
            AvatarColor = _debtor.AvatarColor;

            OpenDebtorSubpage = new RelayCommand(() => OpenDebtorSubpageAsync());
        }

        // constructor for design time vm 
        public DebtorsListItemViewModel()
        {
            // TODO: change this
        }
    }
}
