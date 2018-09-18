using DebtDiary.Core;

namespace DebtDiary
{
    public class ShortOperationsListItemViewModel : BaseViewModel
    {
        public decimal Value { get; set; }
        public string Description { get; set; }

        public ShortOperationsListItemViewModel()
        {

        }

        public ShortOperationsListItemViewModel(Operation operation)
        {
            Value = operation.Value;
            Description = operation.Description;
        }
    }
}
