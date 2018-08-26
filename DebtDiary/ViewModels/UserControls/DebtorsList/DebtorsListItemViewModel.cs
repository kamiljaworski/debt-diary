using DebtDiary.Core;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// View model for each of debtors list item
    /// </summary>
    public class DebtorsListItemViewModel : BaseViewModel
    {
        public string FullName { get; set; }

        public string Initials { get; set; }

        public decimal Debt { get; set; }

        public AvatarColor AvatarColor { get; set; }

        /// <summary>
        /// Debt showed as local currency pattern
        /// </summary>
        public string FormattedDebt
        {
            get
            {
                var numberFormat = (NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat.Clone();
                numberFormat.CurrencyNegativePattern = 8;
                numberFormat.CurrencyPositivePattern = 3;

                return Debt.ToString("C", numberFormat);
            }
        }

        public DebtorsListItemViewModel()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="debtor"><see cref="Debtor"/> you want to make <see cref="DebtorsListItemViewModel"/> from</param>
        public DebtorsListItemViewModel(Debtor debtor)
        {
            FullName = debtor.FullName;
            Initials = debtor.Initials;
            Debt = debtor.Debt;
            AvatarColor = debtor.AvatarColor;
        }
    }
}
