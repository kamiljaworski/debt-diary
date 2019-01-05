using DebtDiary.Core;
using DebtDiary.DataProvider;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class EditDebtorSubpageViewModelTests
    {
        [Test]
        public void TestEditDebtorCommandEditsPersonWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "Emilia";
            editDebtorVM.LastName = "Clarke";
            editDebtorVM.Gender = Gender.Female;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            bool result = debtor.FirstName == "Emilia" && debtor.LastName == "Clarke" && debtor.Gender == Gender.Female && debtor.AvatarColor == Color.Coral;
            Assert.True(result);
        }

        [Test]
        public void TestEditDebtorCommandDoesNotEditPersonWhenThereIsNoInternetConnection()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "Emilia";
            editDebtorVM.LastName = "Clarke";
            editDebtorVM.Gender = Gender.Female;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            bool result = debtor.FirstName == "Kit" && debtor.LastName == "Harington" && debtor.Gender == Gender.Male && debtor.AvatarColor == Color.Brown;
            Assert.True(result);
        }


        [Test]
        public void TestEditDebtorCommandOpensDialogWindowWhenThereIsNoInternetConnection()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "Emilia";
            editDebtorVM.LastName = "Clarke";
            editDebtorVM.Gender = Gender.Female;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.NoInternetConnection), Times.Once());
        }

        [Test]
        public void TestEditDebtorCommandUpdatesDebtorsListInDiaryPageViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> mockDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(stubApplicationVM.Object, mockDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "Emilia";
            editDebtorVM.LastName = "Clarke";
            editDebtorVM.Gender = Gender.Female;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            mockDiaryPageVM.Verify(x => x.UpdateDebtorsList(), Times.Once());
        }

        [Test]
        public void TestEditDebtorCommandOpensDialogWindowWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "Emilia";
            editDebtorVM.LastName = "Clarke";
            editDebtorVM.Gender = Gender.Female;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.DebtorEdited), Times.Once());
        }

        [Test]
        public void TestEditDebtorCommandChangesCurrentSubpageInApplicationViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "Emilia";
            editDebtorVM.LastName = "Clarke";
            editDebtorVM.Gender = Gender.Female;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            mockApplicationVM.Verify(x => x.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage), Times.Once());
        }

        [Test]
        public void TestEditDebtorCommandInformsAboutEmptyFirstName()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "";
            editDebtorVM.LastName = "Clarke";
            editDebtorVM.Gender = Gender.Female;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.EmptyFirstName, editDebtorVM.FirstNameMessage);
        }

        [Test]
        public void TestEditDebtorCommandInformsAboutEmptyLastName()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "Emilia";
            editDebtorVM.LastName = "";
            editDebtorVM.Gender = Gender.Female;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.EmptyLastName, editDebtorVM.LastNameMessage);
        }

        [Test]
        public void TestEditDebtorCommandInformsAboutUnselectedGender()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "Emilia";
            editDebtorVM.LastName = "Clarke";
            editDebtorVM.Gender = Gender.None;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.UnselectedGender, editDebtorVM.GenderMessage);
        }

        [Test]
        public void TestEditDebtorCommandInformsAboutIncorrectFirstName()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "123";
            editDebtorVM.LastName = "Clarke";
            editDebtorVM.Gender = Gender.Female;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectFirstName, editDebtorVM.FirstNameMessage);
        }

        [Test]
        public void TestEditDebtorCommandInformsAboutIncorrectLastName()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "Emilia";
            editDebtorVM.LastName = "123";
            editDebtorVM.Gender = Gender.Female;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectLastName, editDebtorVM.LastNameMessage);
        }

        [Test]
        public void TestEditDebtorCommandInformsAboutDebtorsDuplicate()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            Debtor debtor2 = new Debtor { Id = 2, FirstName = "Emilia", LastName = "Clarke" };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor, debtor2 } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "Emilia";
            editDebtorVM.LastName = "Clarke";
            editDebtorVM.Gender = Gender.Female;
            editDebtorVM.AvatarColor = Color.Coral;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)editDebtorVM.EditDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.DebtorExist, editDebtorVM.LastNameMessage);
        }

        [Test]
        public void TestPreviousColorCommandChangesColorToCorrectOne()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.AvatarColor = Color.Magenta;

            editDebtorVM.PreviousColorCommand.Execute(null);

            Assert.AreEqual(Color.MediumSeaGreen, editDebtorVM.AvatarColor);
        }

        [Test]
        public void TestNextColorCommandChangesColorToCorrectOne()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.AvatarColor = Color.Magenta;

            editDebtorVM.NextColorCommand.Execute(null);

            Assert.AreEqual(Color.PaleVioletRed, editDebtorVM.AvatarColor);
        }

        [Test]
        public void TestInitialsReturnsCorrectInitials()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            editDebtorVM.FirstName = "Test";
            editDebtorVM.LastName = "Testowy";

            string initials = editDebtorVM.Initials;

            Assert.AreEqual("TT", initials);
        }

        [Test]
        public void TestGoBackCommandChangesCurrentSubpageInApplicationViewModel()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = new Debtor { Id = 1, FirstName = "Kit", LastName = "Harington", Gender = Gender.Male, AvatarColor = Color.Brown };
            User user = new User { Password = "EF797C8118F02DFB649607DD5D3F8C7623048C9C063D532CC95C5ED7A898A64F", Debtors = new List<Debtor> { debtor } };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            var editDebtorVM = new EditDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            editDebtorVM.GoBackCommand.Execute(null);

            mockApplicationVM.Verify(x => x.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage), Times.Once());
        }
    }
}
