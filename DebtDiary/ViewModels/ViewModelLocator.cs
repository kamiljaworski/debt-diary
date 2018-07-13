namespace DebtDiary
{
    /// <summary>
    /// Locates View Models from IoC for use in xaml binding
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// ApplicationViewModel Property
        /// </summary>
        public ApplicationViewModel ApplicationViewModel { get => IocContainer.Get<ApplicationViewModel>(); }
    }
}
