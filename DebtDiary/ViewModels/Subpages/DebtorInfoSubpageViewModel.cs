using DebtDiary.Core;

namespace DebtDiary
{
    public class DebtorInfoSubpageViewModel : BaseViewModel, IDebtorInfoSubpageViewModel
    {
        private Debtor _selectedDebtor = null;

        public string FullName { get; private set; }
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
