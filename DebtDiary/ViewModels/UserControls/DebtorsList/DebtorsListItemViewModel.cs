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
        private IDebtorInfoSubpageViewModel _debtorInfoSubpageViewModel;
        private IDiaryPageViewModel _diaryPageViewModel;
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


            // Reset selected buttons in the diary page side menu
            _diaryPageViewModel.ResetSelectedButtons();
            bool isSubpageChanged = await _applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage);
            
            // Change subpage
            if (isSubpageChanged)
            {
                _applicationViewModel.SelectedDebtor = _debtor;
                _debtorInfoSubpageViewModel.UpdateChanges();
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="debtor"><see cref="Debtor"/> you want to make <see cref="DebtorsListItemViewModel"/> from</param>
        public DebtorsListItemViewModel(Debtor debtor, IApplicationViewModel applicationViewModel, IDebtorInfoSubpageViewModel debtorInfoSubpageViewModel, IDiaryPageViewModel diaryPageViewModel)
        {
            _debtor = debtor;
            _applicationViewModel = applicationViewModel;
            _debtorInfoSubpageViewModel = debtorInfoSubpageViewModel;
            _diaryPageViewModel = diaryPageViewModel;

            Id = _debtor.Id;
            FullName = _debtor.FullName;
            Initials = _debtor.Initials;
            Debt = _debtor.Debt;
            AvatarColor = _debtor.AvatarColor;

            // TODO: optimize arguments number
            OpenDebtorSubpage = new RelayCommand(() => OpenDebtorSubpageAsync());
        }

        // constructor for design time vm 
        public DebtorsListItemViewModel()
        {
            // TODO: change this
        }
    }
}
