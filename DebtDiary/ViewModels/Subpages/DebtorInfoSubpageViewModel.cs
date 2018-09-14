using DebtDiary.Core;

namespace DebtDiary
{
    public class DebtorInfoSubpageViewModel : BaseViewModel, IDebtorInfoSubpageViewModel
    {
        private Debtor _selectedDebtor = null;

        public string FullName { get; private set; }
        public decimal Debt { get; private set; } = 0m;
        public int OperationsNumber { get; private set; } = 0;
        public decimal LastOperation { get; private set; } = 0m;
        public Debtor SelectedDebtor
        {
            get => _selectedDebtor;
            set
            {
                _selectedDebtor = value;
                UpdateData();
            }
        }



        private void UpdateData()
        {
            if (_selectedDebtor == null)
                return;

            FullName = _selectedDebtor.FullName;
        }
    }
}
