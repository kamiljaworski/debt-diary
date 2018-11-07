using DebtDiary.Core;
using DebtDiary.DataProvider;
using Ninject;

namespace DebtDiary
{
    /// <summary>
    /// The IoC Container for application
    /// </summary>
    public static class IocContainer
    {
        /// <summary>
        /// Kernel of this IoC Container
        /// </summary>
        public static IKernel Kernel { get; private set; } = new StandardKernel();

        /// <summary>
        /// Setup of the IoC Container
        /// Binding all dependencies
        /// Must be called as soon as the application starts
        /// </summary>
        public static void Setup()
        {
            Kernel.Bind<IApplicationViewModel>().To<ApplicationViewModel>().InSingletonScope();
            Kernel.Bind<IDataAccess>().To<DataAccess>().InSingletonScope();
            Kernel.Bind<IDialogFacade>().To<DialogFacade>().InSingletonScope();
            Kernel.Bind<IClientDataStore>().To<ClientDataStore>().InSingletonScope();
            Kernel.Bind<IDiaryPageViewModel>().To<DiaryPageViewModel>().InSingletonScope()
                .WithConstructorArgument("applicationViewModel", Get<IApplicationViewModel>())
                .WithConstructorArgument("clientDataStore", Get<IClientDataStore>());
        }

        /// <summary>
        /// Get the specified dependency object
        /// </summary>
        /// <typeparam name="T">The type to get</typeparam>
        /// <returns></returns>
        public static T Get<T>()
        {
            return Kernel.Get<T>();
        }
    }
}
