using DebtDiary.Core;
using System;

namespace DebtDiary
{
    public class ShortOperationsListItemViewModel : BaseViewModel
    {
        public int Id { get; private set; }
        public decimal Value { get; set; } = 0.0m;
        public string FormattedValue => FormattingHelpers.GetFormattedCurrency(Value);
        public string Description { get; set; } = string.Empty;
        public DateTime OperationDate { get; set; } = DateTime.Now.Date;
        public string FormattedOperationDate => OperationDate.ToShortDateString();

        public ShortOperationsListItemViewModel() { }

        public ShortOperationsListItemViewModel(Operation operation)
        {
            Id = operation.Id;
            Value = operation.Value;
            Description = operation.Description.ToLowerFirstLetter();
            OperationDate = operation.AdditionDate;
        }
    }
}
