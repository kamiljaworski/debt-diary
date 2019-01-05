using System.Collections.Generic;
using System.Security;
using DebtDiary.Core;
using DebtDiary.DataProvider;
using Moq;
using NUnit.Framework;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class DeleteDebtorSubpageViewModelTests
    {
        [Test]
        public void TestDeleteDebtorCommandRemovesDebtorFromUsersDebtorsListWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            stubPassword.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)deleteDebtorVM.DeleteDebtorCommand).ExecuteAndAwait(stubPassword.Object);

            bool result = user.Debtors.Contains(debtor) == false;
            Assert.True(result);
        }

        [Test]
        public void TestDeleteDebtorCommandDoesNotRemoveDebtorFromUsersDebtorsListWhenThereIsNoInternetConnection()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            stubPassword.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)deleteDebtorVM.DeleteDebtorCommand).ExecuteAndAwait(stubPassword.Object);

            bool result = user.Debtors.Contains(debtor) == true;
            Assert.True(result);
        }

        [Test]
        public void TestDeleteDebtorCommandDoesNotRemoveDebtorFromUsersDebtorsListWhenThereIsPassedWrongObjectAsExecuteParameter()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)deleteDebtorVM.DeleteDebtorCommand).ExecuteAndAwait(null);

            bool result = user.Debtors.Contains(debtor) == true;
            Assert.True(result);
        }

        [Test]
        public void TestDeleteDebtorCommandOpensDialogWindowWhenThereIsNoInternetConnection()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            stubPassword.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)deleteDebtorVM.DeleteDebtorCommand).ExecuteAndAwait(stubPassword.Object);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.NoInternetConnection), Times.Once());
        }

        [Test]
        public void TestDeleteDebtorCommandUpdatesDebtorsListInDiaryPageViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> mockDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(stubApplicationVM.Object, mockDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            stubPassword.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)deleteDebtorVM.DeleteDebtorCommand).ExecuteAndAwait(stubPassword.Object);

            mockDiaryPageVM.Verify(x => x.UpdateDebtorsList(), Times.Once());
        }

        [Test]
        public void TestDeleteDebtorCommandOpensDialogWindowWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            stubPassword.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)deleteDebtorVM.DeleteDebtorCommand).ExecuteAndAwait(stubPassword.Object);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.DebtorDeleted), Times.Once());
        }

        [Test]
        public void TestDeleteDebtorCommandChangesCurrentSubpageToSummarySubpageInApplicationViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            stubPassword.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)deleteDebtorVM.DeleteDebtorCommand).ExecuteAndAwait(stubPassword.Object);

            mockApplicationVM.Verify(x => x.ChangeCurrentSubpageAsync(ApplicationSubpage.SummarySubpage), Times.Once());
        }

        [Test]
        public void TestDeleteDebtorCommandInformsUserAboutEmptyPassword()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            stubPassword.Setup(x => x.Password).Returns(new SecureString());
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)deleteDebtorVM.DeleteDebtorCommand).ExecuteAndAwait(stubPassword.Object);

            Assert.AreEqual(FormMessage.EmptyPassword, deleteDebtorVM.PasswordMessage);
        }

        [Test]
        public void TestDeleteDebtorCommandInformsUserAboutIncorrectPassword()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64D", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            Mock<IHavePassword> stubPassword = new Mock<IHavePassword>();
            stubPassword.Setup(x => x.Password).Returns(GetSecureStringFromString("12345678"));
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)deleteDebtorVM.DeleteDebtorCommand).ExecuteAndAwait(stubPassword.Object);

            Assert.AreEqual(FormMessage.IncorrectPassword, deleteDebtorVM.PasswordMessage);
        }

        [Test]
        public void TestGoBackCommandChangesCurrectSubpageToDebtorInfoSubpageInApplicationViewModel()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64D", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            deleteDebtorVM.GoBackCommand.Execute(null);

            mockApplicationVM.Verify(x => x.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage), Times.Once());
        }

        [Test]
        public void TestFullNameReturnsCorrectFullName()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64D", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            string result = deleteDebtorVM.FullName;

            Assert.AreEqual("John Cena", result);
        }

        [Test]
        public void TestInitialsRetursCorrectInitials()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { FirstName = "John", LastName = "Cena" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64D", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var deleteDebtorVM = new DeleteDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            string result = deleteDebtorVM.Initials;

            Assert.AreEqual("JC", result);
        }

        private SecureString GetSecureStringFromString(string s)
        {
            SecureString ss = new SecureString();
            s.ForEach(c => ss.AppendChar(c));

            return ss;
        }
    }
}
