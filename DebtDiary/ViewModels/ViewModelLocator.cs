namespace DebtDiary
{
    public class ViewModelLocator
    {
        public IApplicationViewModel ApplicationViewModel { get => IocContainer.Get<IApplicationViewModel>(); }
        public IDialogFacade DialogFacade { get => IocContainer.Get<IDialogFacade>(); }
    }
}
