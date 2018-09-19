using DebtDiary.Core;
using System;

namespace DebtDiary
{
    public class ShortOperationsListItemViewModel : BaseViewModel
    {
        public decimal Value { get; set; }
        /// <summary>
        /// TODO: new helper class with currency formatting method
        /// </summary>
        public string FormattedValue => Helpers.GetFormattedCurrency(Value);
        public string Description { get; set; }
        public DateTime OperationDate { get; set; }
        public string FormattedOperationDate => OperationDate.ToShortDateString();


        public ShortOperationsListItemViewModel()
        {

        }

        public ShortOperationsListItemViewModel(Operation operation)
        {
            Value = operation.Value;
            Description = operation.Description;
            OperationDate = operation.AdditionDate;
        }
    }
}
