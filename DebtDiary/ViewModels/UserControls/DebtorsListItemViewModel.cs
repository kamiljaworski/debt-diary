using DebtDiary.Core;

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

    }
}
