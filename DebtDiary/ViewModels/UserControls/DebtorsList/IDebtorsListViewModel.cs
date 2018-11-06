using DebtDiary.Core;
using System.Collections.Generic;

namespace DebtDiary
{
    public interface IDebtorsListViewModel
    {
        IList<DebtorsListItemViewModel> Debtors { get; set; }
        bool IsAnyDebtorAdded { get; }

        /// <summary>
        /// Update debtors list in this View Model
        /// </summary>
        void Update();

        /// <summary>
        /// Sort Debtors collection order by SortType
        /// </summary>
        void Sort(SortType sortType);
    }
}
