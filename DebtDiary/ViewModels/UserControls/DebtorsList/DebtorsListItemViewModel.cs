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
        private Debtor _debtor = null;

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Initials { get; set; }
        public decimal Debt { get; set; }
        public Color AvatarColor { get; set; }
        public bool IsSelected { get; set; }

        public ICommand OpenDebtorSubpage { get; set; }

        public string FormattedDebt => Helpers.GetFormattedCurrency(Debt);

        public DebtorsListItemViewModel()
        {
            OpenDebtorSubpage = new RelayCommand(() => OpenDebtorSubpageAsync());
        }

        private async void OpenDebtorSubpageAsync()
        {
            IocContainer.Get<IApplicationViewModel>().IsSubpageChanging = true;


            // Set current selected debtor in the application
            IApplicationViewModel applicationViewModel = IocContainer.Get<IApplicationViewModel>();
            // Reset selected buttons in the diary page side menu
            IocContainer.Get<IDiaryPageViewModel>().ResetSelectedButtons();
            // Change subpage
            //await Task.Run(() => applicationViewModel.ChangeCurrentSubpage(ApplicationSubpage.DebtorInfoSubpage));
            //applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage);

            applicationViewModel.ChangeCurrentSubpage(ApplicationSubpage.DebtorInfoSubpage);






            IocContainer.Get<IApplicationViewModel>().SelectedDebtor = _debtor;
            IocContainer.Get<IDebtorInfoSubpageViewModel>().UpdateChanges();

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="debtor"><see cref="Debtor"/> you want to make <see cref="DebtorsListItemViewModel"/> from</param>
        public DebtorsListItemViewModel(Debtor debtor) : this()
        {
            _debtor = debtor;
            Id = debtor.Id;
            FullName = debtor.FullName;
            Initials = debtor.Initials;
            Debt = debtor.Debt;
            AvatarColor = debtor.AvatarColor;
        }
    }
}
