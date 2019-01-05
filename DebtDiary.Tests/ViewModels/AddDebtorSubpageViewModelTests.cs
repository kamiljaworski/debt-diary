using DebtDiary.Core;
using DebtDiary.DataProvider;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class AddDebtorSubpageViewModelTests
    {
        [Test]
        public void TestAddDebtorCommandAddsNewDebtorToUsersDebtorsListWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "Testowy";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            bool result = user.Debtors.FirstOrDefault(x => x.FirstName == "Test" && x.LastName == "Testowy" && x.Gender == Gender.Male) != null;
            Assert.True(result);
        }

        [Test]
        public void TestAddDebtorCommandDoesNotAddNewDebtorToUsersDebtorsListWhenThereIsNoInternetConnection()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "Testowy";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            bool result = user.Debtors.FirstOrDefault(x => x.FirstName == "Test" && x.LastName == "Testowy" && x.Gender == Gender.Male) == null;
            Assert.True(result);
        }

        [Test]
        public void TestAddDebtorCommandUpdatesDebtorsListInDiaryPageViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> mockDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, mockDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "Testowy";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            mockDiaryPageVM.Verify(x => x.UpdateDebtorsList(), Times.Once());
        }

        [Test]
        public void TestAddDebtorCommandOpensDialogWindowWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "Testowy";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.NewDebtorAdded), Times.Once());
        }

        [Test]
        public void TestAddDebtorCommandSetsNewDebtorAsSelectedInApplicationViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "Testowy";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);
            mockApplicationVM.SetupProperty(x => x.SelectedDebtor);
            mockApplicationVM.Object.SelectedDebtor = null;

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            bool result = mockApplicationVM.Object.SelectedDebtor != null;
            Assert.True(result);
        }

        [Test]
        public void TestAddDebtorCommandChangesCurrentSubpageToDebtorInfoSubpageWhenDataIsValid()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "Testowy";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            mockApplicationVM.Verify(x => x.ChangeCurrentSubpageAsync(ApplicationSubpage.DebtorInfoSubpage), Times.Once());
        }

        [Test]
        public void TestAddDebtorCommandOpensDialogWindowWhenThereIsNoInternetConnection()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "Testowy";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.NoInternetConnection), Times.Once());
        }

        [Test]
        public void TestAddDebtorCommandInformsUserAboutEmptyFirstName()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "";
            addDebtorSubpageVM.LastName = "Testowy";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.EmptyFirstName, addDebtorSubpageVM.FirstNameMessage);
        }

        [Test]
        public void TestAddDebtorCommandInformsUserAboutEmptyLastName()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.EmptyLastName, addDebtorSubpageVM.LastNameMessage);
        }

        [Test]
        public void TestAddDebtorCommandInformsUserAboutUnselectedGender()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "Testowy";
            addDebtorSubpageVM.Gender = Gender.None;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.UnselectedGender, addDebtorSubpageVM.GenderMessage);
        }

        [Test]
        public void TestAddDebtorCommandInformsUserAboutIncorrectFirstName()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "1231231";
            addDebtorSubpageVM.LastName = "Testowy";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectFirstName, addDebtorSubpageVM.FirstNameMessage);
        }

        [Test]
        public void TestAddDebtorCommandInformsUserAboutIncorrectLastName()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "1231231";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectLastName, addDebtorSubpageVM.LastNameMessage);
        }

        [Test]
        public void TestAddDebtorCommandInformsUserAboutDebtorDuplicate()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            User user = new User { Debtors = new List<Debtor>() };
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(user);
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "Testowy";
            addDebtorSubpageVM.Gender = Gender.Male;
            stubDataAccess.Setup(x => x.UserAddedThisDebtor(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)addDebtorSubpageVM.AddDebtorCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.DebtorExist, addDebtorSubpageVM.LastNameMessage);
        }

        [Test]
        public void TestPreviousColorCommandChangesColorToCorrectOne()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.AvatarColor = Color.Magenta;

            addDebtorSubpageVM.PreviousColorCommand.Execute(null);

            Assert.AreEqual(Color.MediumSeaGreen, addDebtorSubpageVM.AvatarColor);
        }

        [Test]
        public void TestNextColorCommandChangesColorToCorrectOne()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.AvatarColor = Color.Magenta;

            addDebtorSubpageVM.NextColorCommand.Execute(null);

            Assert.AreEqual(Color.PaleVioletRed, addDebtorSubpageVM.AvatarColor);
        }

        [Test]
        public void TestInitialsReturnsCorrectInitials()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            var addDebtorSubpageVM = new AddDebtorSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            addDebtorSubpageVM.FirstName = "Test";
            addDebtorSubpageVM.LastName = "Testowy";

            string initials = addDebtorSubpageVM.Initials;

            Assert.AreEqual("TT", initials);
        }


    }
}
