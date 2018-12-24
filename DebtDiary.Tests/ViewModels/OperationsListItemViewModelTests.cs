using DebtDiary.Core;
using NUnit.Framework;
using System;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class OperationsListItemViewModelTests
    {
        [Test]
        public void TestOneParameterConstructor()
        {
            var operation = new Operation { AdditionDate = DateTime.Now, Description = "test", Id = 1, OperationType = OperationType.DebtorsLoan, Value = 100.99m,
                                            Debtor = new Debtor { FirstName = "Test", LastName = "Test" } };

            var operationListItem = new OperationsListItemViewModel(operation);

            Assert.True(operationListItem.Value == operation.Value && operationListItem.OperationDate == operation.AdditionDate &&
                        operationListItem.Description == operation.Description && operationListItem.FullName == operation.Debtor.FullName);
        }

        [Test]
        public void TestParameterlessConstructor()
        {
            var operationListItem = new OperationsListItemViewModel();

            Assert.True(operationListItem.Value == 0.0m && operationListItem.OperationDate == DateTime.Now.Date &&
                        operationListItem.Description == string.Empty && operationListItem.FullName == string.Empty);
        }

        [Test]
        public void TestFormattingProperties()
        {
            var operationListItem = new OperationsListItemViewModel();

            var date = new DateTime(2018, 12, 24);
            operationListItem.OperationDate = date;

            Assert.True(operationListItem.FormattedOperationDate == date.ToShortDateString() && operationListItem.FormattedValue == FormattingHelpers.GetFormattedCurrency(0.0m));
        }
    }
}
