using DebtDiary.Core;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DebtDiary
{
    public class DebtorsListItemViewModel : BaseViewModel
    {
        #region Private members
        private IApplicationViewModel _applicationViewModel;
        private IDebtorsListViewModel _debtorsListViewModel;
        private Debtor _debtor;

        private bool isInDesignTime = false;
        #endregion

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Initials { get; set; }
        public decimal Debt { get; set; }
        public Color AvatarColor { get; set; }
        public bool IsSelected { get; set; }

        public ICommand OpenDebtorSubpage { get; set; }

        public string FormattedDebt => FormattingHelpers.GetFormattedCurrency(Debt);

        private async void OpenDebtorSubpageAsync()
        {
            if (isInDesignTime)
                return;

            _applicationViewModel.SelectedDebtor = _debtor;
            _debtorsListViewModel.Update();

            await _applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage);
        }

        public DebtorsListItemViewModel(Debtor debtor, IApplicationViewModel applicationViewModel, IDebtorsListViewModel debtorsListViewModel)
        {
            isInDesignTime = false;
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

        public DebtorsListItemViewModel()
        {
            isInDesignTime = true;
        }
    }
}
