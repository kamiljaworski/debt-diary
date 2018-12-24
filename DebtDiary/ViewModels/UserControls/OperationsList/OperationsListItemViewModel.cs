using DebtDiary.Core;
using System;

namespace DebtDiary
{
    public class OperationsListItemViewModel : ShortOperationsListItemViewModel
    {
        public string FullName { get; set; } = string.Empty;

        public OperationsListItemViewModel() : base() { }

        public OperationsListItemViewModel(Operation operation) : base(operation)
        {
            FullName = operation.Debtor.FullName;
        }
    }
}
