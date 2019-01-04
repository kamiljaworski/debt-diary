using DebtDiary.Core;
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
    public class SummarySubpageViewModelTests
    {
        private User _user;

        [Test]
        public void TestConstructorCreatesCorrectSumOfDebtsStatisticPanel()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);

            var summarySubpageVM = new SummarySubpageViewModel(stubClientDataStore.Object);

            Assert.AreEqual("500,00 zł", summarySubpageVM.SumOfDebts.Value);
        }

        [Test]
        public void TestConstructorCreatesCorrectNumberOfDebtorsStatisticPanel()
        {
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);

            var summarySubpageVM = new SummarySubpageViewModel(stubClientDataStore.Object);

            Assert.AreEqual("3", summarySubpageVM.NumberOfDebtors.Value);
        }

        [Test]
        public void TestConstructorCreatesCorrectNumberOfOperationsStatisticPanel()
        {
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);

            var summarySubpageVM = new SummarySubpageViewModel(stubClientDataStore.Object);

            Assert.AreEqual("9", summarySubpageVM.NumberOfOperations.Value);
        }

        [Test]
        public void TestConstructorCreatesCorrectLastOperationStatisticPanel()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);

            var summarySubpageVM = new SummarySubpageViewModel(stubClientDataStore.Object);

            Assert.AreEqual("500,00 zł", summarySubpageVM.LastOperation.Value);
        }

        [Test]
        public void TestConstructorCreatesCorrectNumberOfChartPoints()
        {
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);

            var summarySubpageVM = new SummarySubpageViewModel(stubClientDataStore.Object);

            Assert.AreEqual(12, summarySubpageVM.SeriesCollection.First().Values.Count);
        }

        [Test]
        public void TestConstructorCreatesCorrectNumberOfOperationsListItems()
        {
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);

            var summarySubpageVM = new SummarySubpageViewModel(stubClientDataStore.Object);

            Assert.AreEqual(9, summarySubpageVM.OperationsList.Operations.Count);
        }

        [Test]
        public void TestCurrencyFormatterReturnsCorrectFormattedValue()
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);
            var summarySubpageVM = new SummarySubpageViewModel(stubClientDataStore.Object);

            var result = summarySubpageVM.CurrencyFormatter(21.37);

            Assert.AreEqual("21,37 zł", result);
        }

        public SummarySubpageViewModelTests()
        {
            Debtor d1 = new Debtor { Id = 1, FirstName = "Annette", LastName = "Montz", };
            Debtor d2 = new Debtor { Id = 2, FirstName = "Tim",  LastName = "Giordano", };
            Debtor d3 = new Debtor { Id = 3, FirstName = "Antione", LastName = "Feliciano", };

            Operation op1 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(10), Value = 100.0m, Debtor = d1 };
            Operation op2 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(9), Value = 200.0m, Debtor = d1 };
            Operation op3 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(2), Value = 100.0m, Debtor = d1 };
            Operation op4 = new Operation { AdditionDate = DateTime.Now, Value = 500.0m, Debtor = d1 };
            Operation op5 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(8), Value = -100.0m, Debtor = d2 };
            Operation op6 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(7), Value = -900.0m, Debtor = d2 };
            Operation op7 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(3), Value = -100.0m, Debtor = d3 };
            Operation op8 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(2), Value = 900.0m, Debtor = d3 };
            Operation op9 = new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(1), Value = -200.0m, Debtor = d3 };

            d1.Operations = new System.Collections.Generic.List<Operation> { op1, op2, op3, op4 };
            d2.Operations = new System.Collections.Generic.List<Operation> { op5, op6 };
            d3.Operations = new System.Collections.Generic.List<Operation> { op7, op8, op9 };

            // Prepare fake user
            _user = new User
            {
                FirstName = "John",
                LastName = "Cena",
                Debtors = new System.Collections.Generic.List<Debtor> { d1, d2, d3 },
                Operations = { op1, op2, op3, op4, op5, op6, op7, op8, op9 },
                RegisterDate = DateTime.Now - TimeSpan.FromDays(10)
            };
        }
    }
}
