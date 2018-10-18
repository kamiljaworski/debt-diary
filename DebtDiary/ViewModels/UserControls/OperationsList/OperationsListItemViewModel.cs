using DebtDiary.Core;
using System;

namespace DebtDiary
{
    public class OperationsListItemViewModel : BaseViewModel
    {
        public string FullName { get; set; }
        public decimal Value { get; set; }
        public string FormattedValue => Helpers.GetFormattedCurrency(Value);
        public string Description { get; set; }
        public DateTime OperationDate { get; set; }
        public string FormattedOperationDate => OperationDate.ToShortDateString();


        public OperationsListItemViewModel()
        {
        }

        public OperationsListItemViewModel(Operation operation)
        {
            FullName = operation.Debtor.FullName;
            Value = operation.Value;
            Description = operation.Description.ToLowerFirstLetter();
            OperationDate = operation.AdditionDate;
        }
    }
}
