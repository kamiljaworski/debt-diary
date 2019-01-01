using System.Security;
using DebtDiary.Core;
using DebtDiary.DataProvider;
using Moq;
using NUnit.Framework;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class LoginPageViewModelTests
    {
        [Test]
        public void TestLoginCommandCallsLoginUserInClientDataStoreWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IClientDataStore> mockClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var loginPageVM = new LoginPageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacadeVM.Object, mockClientDataStore.Object, stubDataAccess.Object);
            loginPageVM.Username = "test";
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            SecureString ss = new SecureString();
            ss.AppendChar('t');
            stubPassword.Setup(x => x.Password).Returns(ss);
            User user = new User();
            stubDataAccess.Setup(x => x.UserExist(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            stubDataAccess.Setup(x => x.TryGetUser(It.IsAny<string>(), It.IsAny<string>(), out user)).Returns(true);

            ((RelayParameterizedCommand)loginPageVM.LoginCommand).ExecuteAndAwait(stubPassword.Object);

            mockClientDataStore.Verify(x => x.LoginUser(It.IsAny<User>()), Times.Once());
        }

        [Test]
        public void TestLoginCommandUpdatesDebtorsListInDiaryPageViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> mockDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var loginPageVM = new LoginPageViewModel(stubApplicationVM.Object, mockDiaryPageVM.Object, stubDialogFacadeVM.Object, stubClientDataStore.Object, stubDataAccess.Object);
            loginPageVM.Username = "test";
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            SecureString ss = new SecureString();
            ss.AppendChar('t');
            stubPassword.Setup(x => x.Password).Returns(ss);
            User user = new User();
            stubDataAccess.Setup(x => x.UserExist(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            stubDataAccess.Setup(x => x.TryGetUser(It.IsAny<string>(), It.IsAny<string>(), out user)).Returns(true);

            ((RelayParameterizedCommand)loginPageVM.LoginCommand).ExecuteAndAwait(stubPassword.Object);

            mockDiaryPageVM.Verify(x => x.UpdateDebtorsList(), Times.Once());
        }

        [Test]
        public void TestLoginCommandUpdatesUsersDataInDiaryPageViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> mockDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var loginPageVM = new LoginPageViewModel(stubApplicationVM.Object, mockDiaryPageVM.Object, stubDialogFacadeVM.Object, stubClientDataStore.Object, stubDataAccess.Object);
            loginPageVM.Username = "test";
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            SecureString ss = new SecureString();
            ss.AppendChar('t');
            stubPassword.Setup(x => x.Password).Returns(ss);
            User user = new User();
            stubDataAccess.Setup(x => x.UserExist(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            stubDataAccess.Setup(x => x.TryGetUser(It.IsAny<string>(), It.IsAny<string>(), out user)).Returns(true);

            ((RelayParameterizedCommand)loginPageVM.LoginCommand).ExecuteAndAwait(stubPassword.Object);

            mockDiaryPageVM.Verify(x => x.UpdateUsersData(), Times.Once());
        }

        [Test]
        public void TestLoginCommandResetsCurrentSubpageInApplicationViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var loginPageVM = new LoginPageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacadeVM.Object, stubClientDataStore.Object, stubDataAccess.Object);
            loginPageVM.Username = "test";
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            SecureString ss = new SecureString();
            ss.AppendChar('t');
            stubPassword.Setup(x => x.Password).Returns(ss);
            User user = new User();
            stubDataAccess.Setup(x => x.UserExist(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            stubDataAccess.Setup(x => x.TryGetUser(It.IsAny<string>(), It.IsAny<string>(), out user)).Returns(true);

            ((RelayParameterizedCommand)loginPageVM.LoginCommand).ExecuteAndAwait(stubPassword.Object);

            mockApplicationVM.Verify(x => x.ResetCurrentSubpage(), Times.Once());
        }

        [Test]
        public void TestLoginCommandChangesCurrentPageToDiaryPageInApplicationViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var loginPageVM = new LoginPageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacadeVM.Object, stubClientDataStore.Object, stubDataAccess.Object);
            loginPageVM.Username = "test";
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            SecureString ss = new SecureString();
            ss.AppendChar('t');
            stubPassword.Setup(x => x.Password).Returns(ss);
            User user = new User();
            stubDataAccess.Setup(x => x.UserExist(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            stubDataAccess.Setup(x => x.TryGetUser(It.IsAny<string>(), It.IsAny<string>(), out user)).Returns(true);

            ((RelayParameterizedCommand)loginPageVM.LoginCommand).ExecuteAndAwait(stubPassword.Object);

            mockApplicationVM.Verify(x => x.ChangeCurrentPageAsync(ApplicationPage.DiaryPage), Times.Once());
        }

        [TestCase("")]
        [TestCase(null)]
        public void TestLoginCommandInformsUserAboutEmptyUsernameWhenUsernameIsNullOrEmpty(string username)
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var loginPageVM = new LoginPageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacadeVM.Object, stubClientDataStore.Object, stubDataAccess.Object);
            loginPageVM.Username = username;
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            SecureString ss = new SecureString();
            ss.AppendChar('t');
            stubPassword.Setup(x => x.Password).Returns(ss);

            ((RelayParameterizedCommand)loginPageVM.LoginCommand).ExecuteAndAwait(stubPassword.Object);

            Assert.AreEqual(FormMessage.EmptyUsername, loginPageVM.UsernameMessage);
        }

        [Test]
        public void TestLoginCommandInformsUserAboutEmptyPasswordWhenPasswordIsEmpty()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var loginPageVM = new LoginPageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacadeVM.Object, stubClientDataStore.Object, stubDataAccess.Object);
            loginPageVM.Username = "test";
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            SecureString ss = new SecureString();
            stubPassword.Setup(x => x.Password).Returns(ss);

            ((RelayParameterizedCommand)loginPageVM.LoginCommand).ExecuteAndAwait(stubPassword.Object);

            Assert.AreEqual(FormMessage.EmptyPassword, loginPageVM.PasswordMessage);
        }

        [Test]
        public void TestLoginCommandInformsUserAboutIncorrectUsernameOrPasswordWhenUserDoesNotExistInDatabase()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var loginPageVM = new LoginPageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacadeVM.Object, stubClientDataStore.Object, stubDataAccess.Object);
            loginPageVM.Username = "test";
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            SecureString ss = new SecureString();
            ss.AppendChar('t');
            stubPassword.Setup(x => x.Password).Returns(ss);
            stubDataAccess.Setup(x => x.UserExist(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            ((RelayParameterizedCommand)loginPageVM.LoginCommand).ExecuteAndAwait(stubPassword.Object);

            bool isUsernameIncorrect = loginPageVM.UsernameMessage == FormMessage.IncorrectUsername;
            bool isPasswordIncorrect = loginPageVM.PasswordMessage == FormMessage.IncorrectPassword;
            Assert.True(isUsernameIncorrect && isPasswordIncorrect);
        }

        [Test]
        public void TestCreateAccountCommandChangesCurrentPageToRegisterPage()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var loginPageVM = new LoginPageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacadeVM.Object, stubClientDataStore.Object, stubDataAccess.Object);

            loginPageVM.CreateAccountCommand.Execute(null);

            mockApplicationVM.Verify(x => x.ChangeCurrentPageAsync(ApplicationPage.RegisterPage), Times.Once());
        }
    }
}
