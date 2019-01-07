using DebtDiary.Core;
using DebtDiary.DataProvider;
using Moq;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class DebtorInfoSubpageViewModelTests
    {
        #region Constructor tests

        [TestCase(Gender.Male, StatisticPanelMessage.DebtMale)]
        [TestCase(Gender.Female, StatisticPanelMessage.DebtFemale)]
        [TestCase(Gender.None, StatisticPanelMessage.DebtFemale)]
        public void TestConstructorCreatesCorrectDebtStatisticPanel(Gender gender, StatisticPanelMessage expectedMessage)
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            debtor.Gender = gender;
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);

            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            bool result = debtorInfoSubpageVM.Debt.Message == expectedMessage && debtorInfoSubpageVM.Debt.Value == "900,00 zł";
            Assert.True(result);
        }

        [Test]
        public void TestConstructorCreatesCorrectAdditionDateStatisticPanel()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);

            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            Assert.AreEqual(debtor.AdditionDate.ToShortDateString(), debtorInfoSubpageVM.AdditionDate.Value);
        }

        [Test]
        public void TestConstructorCreatesCorrectNumberOfOperationsStatisticPanel()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);

            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            Assert.AreEqual("4", debtorInfoSubpageVM.NumberOfOperations.Value);
        }

        [Test]
        public void TestConstructorCreatesCorrectLastOperationStatisticPanel()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);

            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            Assert.AreEqual("500,00 zł", debtorInfoSubpageVM.LastOperation.Value);
        }

        [Test]
        public void TestConstructorCreatesCorrectNumberOfChartPoints()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);

            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            Assert.AreEqual(12, debtorInfoSubpageVM.SeriesCollection.First().Values.Count);
        }

        [Test]
        public void TestConstructorCreatesCorrectNumberOfOperationsListItems()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);

            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            Assert.AreEqual(4, debtorInfoSubpageVM.OperationsList.Operations.Count);
        }
        #endregion

        #region AddLoanCommand tests

        [Test]
        public void TestAddLoanCommandAddsDebtorsLoanToDebtorsOperationsListWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            bool result = debtor.Operations.FirstOrDefault(x => x.Value == 100m && x.Description == "test operation"
                          && x.AdditionDate.Date == (DateTime.Now - TimeSpan.FromDays(2)).Date) != null;
            Assert.True(result);
        }

        [Test]
        public void TestAddLoanCommandAddsUsersLoanToDebtorsOperationsListWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            debtorInfoSubpageVM.LoanOperationType = OperationType.UsersLoan;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            bool result = debtor.Operations.FirstOrDefault(x => x.Value == -100m && x.Description == "test operation"
                          && x.AdditionDate.Date == (DateTime.Now - TimeSpan.FromDays(2)).Date) != null;
            Assert.True(result);
        }

        [Test]
        public void TestAddLoanCommandDoesNotAddOperationToDebtorsOperationsListWhenThereIsNoInternetConnection()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            bool result = debtor.Operations.FirstOrDefault(x => x.Value == 100m && x.Description == "test operation" && x.AdditionDate == (DateTime.Now - TimeSpan.FromDays(2)).Date) == null;
            Assert.True(result);
        }

        [Test]
        public void TestAddLoanCommandOpensDialogWindowWhenThereIsNoInternetConnection()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.NoInternetConnection), Times.Once());
        }

        [Test]
        public void TestAddLoanCommandResetsEnteredDataWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            bool result = debtorInfoSubpageVM.LoanValue == string.Empty && debtorInfoSubpageVM.LoanDescription == string.Empty && debtorInfoSubpageVM.LoanDate.Date == DateTime.Now.Date;
            Assert.True(result);
        }

        [Test]
        public void TestAddLoanCommandUpdatesDebtStatisticPanelWhenDataIsValid()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            Assert.AreEqual("1 000,00 zł", debtorInfoSubpageVM.Debt.Value);
        }

        [Test]
        public void TestAddLoanCommandUpdatesNumberOfOperationsStatisticPanelWhenDataIsValid()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            Assert.AreEqual("5", debtorInfoSubpageVM.NumberOfOperations.Value);
        }

        [Test]
        public void TestAddLoanCommandUpdatesLastOperationStatisticPanelWhenDataIsValid()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            Assert.AreEqual("100,00 zł", debtorInfoSubpageVM.LastOperation.Value);
        }

        [Test]
        public void TestAddLoanCommandUpdatesOperationsListWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            Assert.AreEqual(5, debtorInfoSubpageVM.OperationsList.Operations.Count);
        }

        [Test]
        public void TestAddLoanCommandUpdatesDebtorsListInDiaryPageViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> mockDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, mockDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            mockDiaryPageVM.Verify(x => x.UpdateDebtorsList(), Times.Once());
        }

        [Test]
        public void TestAddLoanCommandInformsUserAboutEmptyValue()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.EmptyValue, debtorInfoSubpageVM.LoanValueMessage);
        }

        [Test]
        public void TestAddLoanCommandInformsUserAboutEmptyDescription()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.EmptyDescription, debtorInfoSubpageVM.LoanDescriptionMessage);
        }

        [Test]
        public void TestAddLoanCommandInformsUserAboutIncorrectDateWhenItIsTooOld()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(366);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectDate, debtorInfoSubpageVM.LoanDateMessage);
        }

        [Test]
        public void TestAddLoanCommandInformsUserAboutIncorrectDateWhenItIsInTheFuture()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now + TimeSpan.FromDays(1);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectDate, debtorInfoSubpageVM.LoanDateMessage);
        }

        [Test]
        public void TestAddLoanCommandInformsUserAboutIncorrectValue()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "asd";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectValue, debtorInfoSubpageVM.LoanValueMessage);
        }

        [Test]
        public void TestAddLoanCommandInformsUserAboutIncorrectDescription()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "100";
            debtorInfoSubpageVM.LoanDescription = "123^%&*ADS";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectDescription, debtorInfoSubpageVM.LoanDescriptionMessage);
        }

        [Test]
        public void TestAddLoanCommandInformsUserAboutNegativeNumber()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.LoanValue = "-100";
            debtorInfoSubpageVM.LoanDescription = "test operation";
            debtorInfoSubpageVM.LoanDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddLoanCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.NegativeNumber, debtorInfoSubpageVM.LoanValueMessage);
        }
        #endregion

        #region AddRepaymentCommand tests

        [Test]
        public void TestAddRepaymentCommandAddsDebtorsRepaymentToDebtorsOperationsListWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            bool result = debtor.Operations.FirstOrDefault(x => x.Value == -100m && x.Description == "test operation"
                          && x.AdditionDate.Date == (DateTime.Now - TimeSpan.FromDays(2)).Date) != null;
            Assert.True(result);
        }

        [Test]
        public void TestAddRepaymentCommandAddsUsersRepaymentToDebtorsOperationsListWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now - TimeSpan.FromDays(2);
            debtorInfoSubpageVM.RepaymentOperationType = OperationType.UsersRepayment;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            bool result = debtor.Operations.FirstOrDefault(x => x.Value == 100m && x.Description == "test operation"
                          && x.AdditionDate.Date == (DateTime.Now - TimeSpan.FromDays(2)).Date) != null;
            Assert.True(result);
        }

        [Test]
        public void TestAddRepaymentCommandDoesNotAddOperationToDebtorsOperationsListWhenThereIsNoInternetConnection()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            bool result = debtor.Operations.FirstOrDefault(x => x.Value == -100m && x.Description == "test operation" && x.AdditionDate == (DateTime.Now - TimeSpan.FromDays(2)).Date) == null;
            Assert.True(result);
        }

        [Test]
        public void TestAddRepaymentCommandOpensDialogWindowWhenThereIsNoInternetConnection()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> mockDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, mockDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(false);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            mockDialogFacade.Verify(x => x.OpenDialog(DialogMessage.NoInternetConnection), Times.Once());
        }

        [Test]
        public void TestAddRepaymentCommandResetsEnteredDataWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            bool result = debtorInfoSubpageVM.RepaymentValue == string.Empty && debtorInfoSubpageVM.RepaymentDescription == string.Empty
                          && debtorInfoSubpageVM.RepaymentDate.Date == DateTime.Now.Date;
            Assert.True(result);
        }

        [Test]
        public void TestAddRepaymentCommandUpdatesDebtStatisticPanelWhenDataIsValid()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            Assert.AreEqual("800,00 zł", debtorInfoSubpageVM.Debt.Value);
        }

        [Test]
        public void TestAddRepaymentCommandUpdatesNumberOfOperationsStatisticPanelWhenDataIsValid()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now - TimeSpan.FromDays(2);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            Assert.AreEqual("5", debtorInfoSubpageVM.NumberOfOperations.Value);
        }

        [Test]
        public void TestAddRepaymentCommandUpdatesLastOperationStatisticPanelWhenDataIsValid()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            Assert.AreEqual("-100,00 zł", debtorInfoSubpageVM.LastOperation.Value);
        }

        [Test]
        public void TestAddRepaymentCommandUpdatesOperationsListWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            Assert.AreEqual(5, debtorInfoSubpageVM.OperationsList.Operations.Count);
        }

        [Test]
        public void TestAddRepaymentCommandUpdatesDebtorsListInDiaryPageViewModelWhenDataIsValid()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> mockDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, mockDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            mockDiaryPageVM.Verify(x => x.UpdateDebtorsList(), Times.Once());
        }

        [Test]
        public void TestAddRepaymentCommandInformsUserAboutEmptyValue()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.EmptyValue, debtorInfoSubpageVM.RepaymentValueMessage);
        }

        [Test]
        public void TestAddRepaymentCommandInformsUserAboutEmptyDescription()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now;
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.EmptyDescription, debtorInfoSubpageVM.RepaymentDescriptionMessage);
        }

        [Test]
        public void TestAddRepaymentCommandInformsUserAboutIncorrectDateWhenItIsTooOld()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now - TimeSpan.FromDays(366);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectDate, debtorInfoSubpageVM.RepaymentDateMessage);
        }

        [Test]
        public void TestAddRepaymentCommandInformsUserAboutIncorrectDateWhenItIsInTheFuture()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now + TimeSpan.FromDays(1);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectDate, debtorInfoSubpageVM.RepaymentDateMessage);
        }

        [Test]
        public void TestAddRepaymentCommandInformsUserAboutIncorrectValue()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "asd";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now + TimeSpan.FromDays(1);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectValue, debtorInfoSubpageVM.RepaymentValueMessage);
        }

        [Test]
        public void TestAddRepaymentCommandInformsUserAboutIncorrectDescription()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "100";
            debtorInfoSubpageVM.RepaymentDescription = "123(^TASYD^";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now + TimeSpan.FromDays(1);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.IncorrectDescription, debtorInfoSubpageVM.RepaymentDescriptionMessage);
        }

        [Test]
        public void TestAddRepaymentCommandInformsUserAboutNegativeNumber()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            stubApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);
            debtorInfoSubpageVM.RepaymentValue = "-100";
            debtorInfoSubpageVM.RepaymentDescription = "test operation";
            debtorInfoSubpageVM.RepaymentDate = DateTime.Now + TimeSpan.FromDays(1);
            stubDataAccess.Setup(x => x.TrySaveChanges()).Returns(true);

            ((RelayParameterizedCommand)debtorInfoSubpageVM.AddRepaymentCommand).ExecuteAndAwait(null);

            Assert.AreEqual(FormMessage.NegativeNumber, debtorInfoSubpageVM.RepaymentValueMessage);
        }
        #endregion

        #region Other tests

        [Test]
        public void TestUpdateChangesDoesNotThrowExceptionWhenThereIsNoSelectedDebtor()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();

            Assert.DoesNotThrow(() => new DebtorInfoSubpageViewModel(stubApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object,
                                                                     stubClientDataStore.Object, stubDataAccess.Object));
        }

        [Test]
        public void TestEditDebtorCommandChangesCurrentSubpageInApplicationViewModel()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            debtorInfoSubpageVM.EditDebtorCommand.Execute(null);

            mockApplicationVM.Verify(x => x.ChangeCurrentSubpageAsync(ApplicationSubpage.EditDebtorSubpage));
        }

        [Test]
        public void TestDeleteDebtorCommandChangesCurrentSubpageInApplicationViewModel()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            debtorInfoSubpageVM.DeleteDebtorCommand.Execute(null);

            mockApplicationVM.Verify(x => x.ChangeCurrentSubpageAsync(ApplicationSubpage.DeleteDebtorSubpage));
        }

        [Test]
        public void TestCurrencyFormatterReturnsCorrectFormattedValue()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDiaryPageViewModel> stubDiaryPageVM = new Mock<IDiaryPageViewModel>();
            Mock<IDialogFacade> stubDialogFacade = new Mock<IDialogFacade>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            Mock<IDataAccess> stubDataAccess = new Mock<IDataAccess>();
            Debtor debtor = GetDebtor();
            mockApplicationVM.Setup(x => x.SelectedDebtor).Returns(debtor);
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(debtor.User);
            var debtorInfoSubpageVM = new DebtorInfoSubpageViewModel(mockApplicationVM.Object, stubDiaryPageVM.Object, stubDialogFacade.Object, stubClientDataStore.Object, stubDataAccess.Object);

            var result = debtorInfoSubpageVM.CurrencyFormatter(21.37);

            Assert.AreEqual("21,37 zł", result);
        }
        #endregion

        #region Private helpers

        private Debtor GetDebtor()
        {
            Debtor d = new Debtor
            {
                Id = 1,
                FirstName = "Annette",
                LastName = "Montz",
                Gender = Gender.Female,
                AdditionDate = DateTime.Now - TimeSpan.FromDays(10)
            };

            Operation op1 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(10), Value = 100.0m, Debtor = d };
            Operation op2 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(9), Value = 200.0m, Debtor = d };
            Operation op3 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(2), Value = 100.0m, Debtor = d };
            Operation op4 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(1), Value = 500.0m, Debtor = d };

            d.Operations = new System.Collections.Generic.List<Operation> { op1, op2, op3, op4 };
            d.User = new User
            {
                FirstName = "John",
                LastName = "Cena",
                RegisterDate = DateTime.Now - TimeSpan.FromDays(10),
                Gender = Gender.Male
            };

            return d;
        }
        #endregion
    }
}
