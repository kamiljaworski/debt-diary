using System;
using System.Security;
using DebtDiary.Core;
using DebtDiary.DataProvider;
using Moq;
using NUnit.Framework;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class RegisterPageViewModelTests
    {
        [Test]
        public void TestSignUpCommandOpensDialogWindowWhenThereIsNoInternetConnection()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> mockDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, mockDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowski";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(false);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            mockDialogFacadeVM.Verify(x => x.OpenDialog(DialogMessage.NoInternetConnection), Times.Once());
        }

        [Test]
        public void TestSignUpCommandOpensDialogWindowWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> mockDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, mockDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowski";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            mockDialogFacadeVM.Verify(x => x.OpenDialog(DialogMessage.AccountCreated), Times.Once());
        }

        [Test]
        public void TestSignUpCommandClearsPasswordsInTheViewWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> mockPasswords = new Mock<IHaveTwoPasswords>();
            mockPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            mockPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowski";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(mockPasswords.Object);

            mockPasswords.Verify(x => x.ClearPassword(), Times.Once());
        }

        [Test]
        public void TestSignUpCommandChangesCurentPageToLoginPageWhenDataIsValid()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(mockApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowski";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            mockApplicationVM.Verify(x => x.ChangeCurrentPageAsync(ApplicationPage.LoginPage), Times.Once());
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutEmptyFirstName()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.LastName = "Testowski";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.EmptyFirstName, registerPageVM.FirstNameMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutEmptyLastName()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.EmptyLastName, registerPageVM.LastNameMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutEmptyUsername()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowski";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.EmptyUsername, registerPageVM.UsernameMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutEmptyEmail()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowski";
            registerPageVM.Username = "test123";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.EmptyEmail, registerPageVM.EmailMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutEmptyPassword()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(new SecureString());
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowski";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.EmptyPassword, registerPageVM.PasswordMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutEmptyRepeatedPassword()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(new SecureString());
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowski";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.EmptyRepeatedPassword, registerPageVM.RepeatedPasswordMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutUnselectedGender()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowski";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.UnselectedGender, registerPageVM.GenderMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutIncorrectFirstName()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "123";
            registerPageVM.LastName = "Testowski";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.IncorrectFirstName, registerPageVM.FirstNameMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutIncorrectLastName()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "123";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.IncorrectLastName, registerPageVM.LastNameMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutIncorrectUsername()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowy";
            registerPageVM.Username = "test123!@#(*U@!";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.IncorrectUsername, registerPageVM.UsernameMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutIncorrectEmail()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowy";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.IncorrectEmail, registerPageVM.EmailMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutTooShortPassword()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("1234567"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("1234567"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowy";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.PasswordTooShort, registerPageVM.PasswordMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutDifferentPasswords()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("1234567"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowy";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.DifferentPasswords, registerPageVM.PasswordMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutTakenUsername()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowy";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(true);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.TakenUsername, registerPageVM.UsernameMessage);
        }

        [Test]
        public void TestSignUpCommandInformsUserAboutTakenEmail()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(stubApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);
            Mock<IHaveTwoPasswords> stubPasswords = new Mock<IHaveTwoPasswords>();
            stubPasswords.Setup(x => x.Password).Returns(MakeSecurityStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(MakeSecurityStringFromString("12345678"));
            registerPageVM.FirstName = "Test";
            registerPageVM.LastName = "Testowy";
            registerPageVM.Username = "test123";
            registerPageVM.Email = "test123@gmail.com";
            registerPageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.IsUsernameTaken(It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.IsEmailTaken(It.IsAny<string>())).Returns(true);
            stubDataAccess.Setup(x => x.TryCreateUser(It.IsAny<User>())).Returns(true);

            ((RelayParameterizedCommand)registerPageVM.SignUpCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.TakenEmail, registerPageVM.EmailMessage);
        }

        [Test]
        public void TestLoginCommandChangesCurrentPageToLoginPage()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDialogFacade> stubDialogFacadeVM = new Mock<IDialogFacade>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var registerPageVM = new RegisterPageViewModel(mockApplicationVM.Object, stubDialogFacadeVM.Object, stubDataAccess.Object);

            registerPageVM.LoginCommand.Execute(null);

            mockApplicationVM.Verify(x => x.ChangeCurrentPageAsync(ApplicationPage.LoginPage), Times.Once());
        }

        private SecureString MakeSecurityStringFromString(string s)
        {
            SecureString ss = new SecureString();
            s.ForEach(x => ss.AppendChar(x));

            return ss;
        }
    }
}
