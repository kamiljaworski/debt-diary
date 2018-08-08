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
        public IApplicationViewModel ApplicationViewModel { get => IocContainer.Get<IApplicationViewModel>(); }
    }
}
