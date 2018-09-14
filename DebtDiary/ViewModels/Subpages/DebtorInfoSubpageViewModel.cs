namespace DebtDiary
{
    public class DebtorInfoSubpageViewModel : BaseViewModel
    {
        public string FullName { get; set; }

        public DebtorInfoSubpageViewModel()
        {
            IApplicationViewModel applicationViewModel = IocContainer.Get<IApplicationViewModel>();
            FullName = applicationViewModel.SelectedDebtor?.FullName;
        }
    }
}
