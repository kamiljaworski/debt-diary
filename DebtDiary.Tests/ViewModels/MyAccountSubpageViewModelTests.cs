using System;
using System.Security;
using DebtDiary.Core;
using DebtDiary.DataProvider;
using Moq;
using NUnit.Framework;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class MyAccountSubpageViewModelTests
    {
        #region EditProfileCommand tests
        [Test]
        public void TestEditProfileCommandEditsUserWhenDataIsValid()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            myAccountSubpageVM.FirstName = "Cynthia";
            myAccountSubpageVM.LastName = "Connor";
            myAccountSubpageVM.Gender = Gender.Female;
            myAccountSubpageVM.AvatarColor = Color.BlueViolet;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.EditProfileCommand).ExecuteAndAwait(null);

            bool result = user.FirstName == "Cynthia" && user.LastName == "Connor" && user.Gender == Gender.Female && user.AvatarColor == Color.BlueViolet;
            Assert.True(result);
        }

        [Test]
        public void TestEditProfileCommandDoesNotEditUserWhenThereIsNoInternetConnection()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            myAccountSubpageVM.FirstName = "Cynthia";
            myAccountSubpageVM.LastName = "Connor";
            myAccountSubpageVM.Gender = Gender.Female;
            myAccountSubpageVM.AvatarColor = Color.BlueViolet;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)myAccountSubpageVM.EditProfileCommand).ExecuteAndAwait(null);

            bool result = user.FirstName == "John" && user.LastName == "Cena" && user.Gender == Gender.Male && user.AvatarColor == Color.Coral;
            Assert.True(result);
        }

        [Test]
        public void TestEditProfileCommandOpensDialogWindowWhenThereIsNoInternetConnection()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            myAccountSubpageVM.FirstName = "Cynthia";
            myAccountSubpageVM.LastName = "Connor";
            myAccountSubpageVM.Gender = Gender.Female;
            myAccountSubpageVM.AvatarColor = Color.BlueViolet;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)myAccountSubpageVM.EditProfileCommand).ExecuteAndAwait(null);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.NoInternetConnection), Times.Once());
        }

        [Test]
        public void TestEditProfileCommandUpdatesUsersDataInDiaryPageViewModelWhenDataIsValid()
        {
            Mock<IDiaryPageViewModel> mockDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(mockDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            myAccountSubpageVM.FirstName = "Cynthia";
            myAccountSubpageVM.LastName = "Connor";
            myAccountSubpageVM.Gender = Gender.Female;
            myAccountSubpageVM.AvatarColor = Color.BlueViolet;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.EditProfileCommand).ExecuteAndAwait(null);

            mockDiaryPageVM.Verify(x => x.UpdateUsersData(), Times.Once());
        }

        [Test]
        public void TestEditProfileCommandOpensDialogWindowWhenDataIsValid()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            myAccountSubpageVM.FirstName = "Cynthia";
            myAccountSubpageVM.LastName = "Connor";
            myAccountSubpageVM.Gender = Gender.Female;
            myAccountSubpageVM.AvatarColor = Color.BlueViolet;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.EditProfileCommand).ExecuteAndAwait(null);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.ProfileUpdated), Times.Once());
        }

        [Test]
        public void TestEditProfileCommandInformsUserAboutEmptyFirstName()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            myAccountSubpageVM.FirstName = "";
            myAccountSubpageVM.LastName = "Connor";
            myAccountSubpageVM.Gender = Gender.Female;
            myAccountSubpageVM.AvatarColor = Color.BlueViolet;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.EditProfileCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.EmptyFirstName, myAccountSubpageVM.FirstNameMessage);
        }

        [Test]
        public void TestEditProfileCommandInformsUserAboutEmptyLastName()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            myAccountSubpageVM.FirstName = "Cynthia";
            myAccountSubpageVM.LastName = "";
            myAccountSubpageVM.Gender = Gender.Female;
            myAccountSubpageVM.AvatarColor = Color.BlueViolet;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.EditProfileCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.EmptyLastName, myAccountSubpageVM.LastNameMessage);
        }

        [Test]
        public void TestEditProfileCommandInformsUserAboutUnselectedGender()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            myAccountSubpageVM.FirstName = "Cynthia";
            myAccountSubpageVM.LastName = "";
            myAccountSubpageVM.Gender = Gender.None;
            myAccountSubpageVM.AvatarColor = Color.BlueViolet;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.EditProfileCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.UnselectedGender, myAccountSubpageVM.GenderMessage);
        }

        [Test]
        public void TestEditProfileCommandInformsUserAboutIncorrectFirstName()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            myAccountSubpageVM.FirstName = "123";
            myAccountSubpageVM.LastName = "Connor";
            myAccountSubpageVM.Gender = Gender.Female;
            myAccountSubpageVM.AvatarColor = Color.BlueViolet;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.EditProfileCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectFirstName, myAccountSubpageVM.FirstNameMessage);
        }

        [Test]
        public void TestEditProfileCommandInformsUserAboutIncorrectLastName()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            myAccountSubpageVM.FirstName = "Cynthia";
            myAccountSubpageVM.LastName = "123";
            myAccountSubpageVM.Gender = Gender.Female;
            myAccountSubpageVM.AvatarColor = Color.BlueViolet;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.EditProfileCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectLastName, myAccountSubpageVM.LastNameMessage);
        }
        #endregion

        #region ChangePasswordCommand tests
        [Test]
        public void TestChangePasswordCommandChangesPasswordWhenDataIsValid()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHaveThreePasswords> stubPasswords = new Mock<IHaveThreePasswords>();
            stubPasswords.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(GetSecureStringFromString("123456789"));
            stubPasswords.Setup(x => x.ThirdPassword).Returns(GetSecureStringFromString("123456789"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.ChangePasswordCommand).ExecuteAndAwait(stubPasswords.Object);

            bool result = user.Password == "15E2B0D3C33891EBB0F1EF609EC419420C20E320CE94C65FBC8C3312448EB225";
            Assert.True(result);
        }

        [Test]
        public void TestChangePasswordCommandDoesNotChangePasswordWhenThereIsNoInternetConnection()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHaveThreePasswords> stubPasswords = new Mock<IHaveThreePasswords>();
            stubPasswords.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(GetSecureStringFromString("123456789"));
            stubPasswords.Setup(x => x.ThirdPassword).Returns(GetSecureStringFromString("123456789"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)myAccountSubpageVM.ChangePasswordCommand).ExecuteAndAwait(stubPasswords.Object);

            bool result = user.Password == "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F";
            Assert.True(result);
        }

        [Test]
        public void TestChangePasswordCommandOpensDialogWindowWhenThereIsNoInternetConnection()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHaveThreePasswords> stubPasswords = new Mock<IHaveThreePasswords>();
            stubPasswords.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(GetSecureStringFromString("123456789"));
            stubPasswords.Setup(x => x.ThirdPassword).Returns(GetSecureStringFromString("123456789"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)myAccountSubpageVM.ChangePasswordCommand).ExecuteAndAwait(stubPasswords.Object);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.NoInternetConnection), Times.Once());
        }

        [Test]
        public void TestChangePasswordCommandClearsPasswordsInIHaveThreePasswordsObject()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHaveThreePasswords> mockPasswords = new Mock<IHaveThreePasswords>();
            mockPasswords.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            mockPasswords.Setup(x => x.SecondPassword).Returns(GetSecureStringFromString("123456789"));
            mockPasswords.Setup(x => x.ThirdPassword).Returns(GetSecureStringFromString("123456789"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.ChangePasswordCommand).ExecuteAndAwait(mockPasswords.Object);

            mockPasswords.Verify(x => x.ClearPassword(), Times.Once());
        }

        [Test]
        public void TestChangePasswordCommandOpensDialogWindowWhenDataIsValid()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHaveThreePasswords> stubPasswords = new Mock<IHaveThreePasswords>();
            stubPasswords.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(GetSecureStringFromString("123456789"));
            stubPasswords.Setup(x => x.ThirdPassword).Returns(GetSecureStringFromString("123456789"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.ChangePasswordCommand).ExecuteAndAwait(stubPasswords.Object);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.PasswordChanged), Times.Once());
        }

        [Test]
        public void TestChangePasswordCommandInformsUserAboutEmptyCurrentPassword()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHaveThreePasswords> stubPasswords = new Mock<IHaveThreePasswords>();
            stubPasswords.Setup(x => x.Password).Returns(GetSecureStringFromString(""));
            stubPasswords.Setup(x => x.SecondPassword).Returns(GetSecureStringFromString("123456789"));
            stubPasswords.Setup(x => x.ThirdPassword).Returns(GetSecureStringFromString("123456789"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.ChangePasswordCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.EmptyCurrentPassword, myAccountSubpageVM.CurrentPasswordMessage);
        }

        [Test]
        public void TestChangePasswordCommandInformsUserAboutEmptyNewPassword()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHaveThreePasswords> stubPasswords = new Mock<IHaveThreePasswords>();
            stubPasswords.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(GetSecureStringFromString(""));
            stubPasswords.Setup(x => x.ThirdPassword).Returns(GetSecureStringFromString("123456789"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.ChangePasswordCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.EmptyNewPassword, myAccountSubpageVM.NewPasswordMessage);
        }

        [Test]
        public void TestChangePasswordCommandInformsUserAboutEmptyRepeatedNewPassword()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHaveThreePasswords> stubPasswords = new Mock<IHaveThreePasswords>();
            stubPasswords.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(GetSecureStringFromString("123456789"));
            stubPasswords.Setup(x => x.ThirdPassword).Returns(GetSecureStringFromString(""));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.ChangePasswordCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.EmptyRepeatedNewPassword, myAccountSubpageVM.RepeatNewPasswordMessage);
        }

        [Test]
        public void TestChangePasswordCommandInformsUserAboutIncorrectCurrentPassword()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHaveThreePasswords> stubPasswords = new Mock<IHaveThreePasswords>();
            stubPasswords.Setup(x => x.Password).Returns(GetSecureStringFromString("123456789"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(GetSecureStringFromString("123456789"));
            stubPasswords.Setup(x => x.ThirdPassword).Returns(GetSecureStringFromString("123456789"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.ChangePasswordCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.IncorrectCurrentPassword, myAccountSubpageVM.CurrentPasswordMessage);
        }

        [Test]
        public void TestChangePasswordCommandInformsUserAboutTooShortPassword()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHaveThreePasswords> stubPasswords = new Mock<IHaveThreePasswords>();
            stubPasswords.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(GetSecureStringFromString("12345"));
            stubPasswords.Setup(x => x.ThirdPassword).Returns(GetSecureStringFromString("12345"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.ChangePasswordCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.PasswordTooShort, myAccountSubpageVM.NewPasswordMessage);
        }

        [Test]
        public void TestChangePasswordCommandInformsUserAbouDifferentPasswords()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHaveThreePasswords> stubPasswords = new Mock<IHaveThreePasswords>();
            stubPasswords.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubPasswords.Setup(x => x.SecondPassword).Returns(GetSecureStringFromString("12345678"));
            stubPasswords.Setup(x => x.ThirdPassword).Returns(GetSecureStringFromString("123456789"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)myAccountSubpageVM.ChangePasswordCommand).ExecuteAndAwait(stubPasswords.Object);

            Assert.AreEqual(FormMessage.DifferentPasswords, myAccountSubpageVM.NewPasswordMessage);
        }

        #endregion

        #region Other tests
        [Test]
        public void TestInitialsReturnsCorrectInitials()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            string result = myAccountSubpageVM.Initials;

            Assert.AreEqual("JC", result);
        }

        [Test]
        public void TestPreviousColorCommandChangesAvatarColorToCorrectOne()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            myAccountSubpageVM.PreviousColorCommand.Execute(null);

            Assert.AreEqual(Color.CornflowerBlue, myAccountSubpageVM.AvatarColor);
        }

        [Test]
        public void TestNextColorCommandChangesAvatarColorToCorrectOne()
        {
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = GetUser();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var myAccountSubpageVM = new MyAccountSubpageViewModel(stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            myAccountSubpageVM.NextColorCommand.Execute(null);

            Assert.AreEqual(Color.Chocolate, myAccountSubpageVM.AvatarColor);
        }


        #endregion

        #region Helpers
        private SecureString GetSecureStringFromString(string s)
        {
            SecureString ss = new SecureString();
            s.ForEach(c => ss.AppendChar(c));

            return ss;
        }

        private User GetUser()
        {
            return new User
            {
                FirstName = "John",
                LastName = "Cena",
                AvatarColor = Color.Coral,
                Username = "test",
                Email = "test@gmail.com",
                Gender = Gender.Male,
                Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F"
            };
        }
        #endregion
    }
}
