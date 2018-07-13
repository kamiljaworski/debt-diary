using Ninject;
using System.ComponentModel;

namespace DebtDiary
{
    /// <summary>
    /// Locates View Models from IoC for use in xaml binding
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Singleton instance of the ViewModelLocator
        /// </summary>
        //public static ViewModelLocator Instance { get; private set; } = new ViewModelLocator();

        /// <summary>
        /// ApplicationViewModel Property
        /// </summary>
        public ApplicationViewModel ApplicationViewModel
        {
            get => IocContainer.Get<ApplicationViewModel>();
        }
    }
}
