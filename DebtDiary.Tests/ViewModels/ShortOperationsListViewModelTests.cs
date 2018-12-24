using DebtDiary.Core;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class ShortOperationsListViewModelTests
    {
        [Test]
        public void TestOperationsContainAllItemsPassedThroughConstructor()
        {
            IEnumerable<Operation> operations = new List<Operation>
            {
                new Operation { AdditionDate = new System.DateTime(2018, 12, 11), Description="test1", Value=100.0m},
                new Operation { AdditionDate = new System.DateTime(2018, 12, 12), Description="test2", Value=-150.0m },
                new Operation { AdditionDate = new System.DateTime(2018, 12, 13), Description="test3", Value=-250.0m },
                new Operation { AdditionDate = new System.DateTime(2018, 12, 15), Description="test6", Value=550.0m}
            };
            var operationsListViewModel = new ShortOperationsListViewModel(operations);

            Assert.True(operationsListViewModel.IsAnyOperationAdded == true && operationsListViewModel.Operations.Count == operations.Count());
        }

        [Test]
        public void TestParameterlessConstructorMakesEmptyOperationsList()
        {
            var operationsListViewModel = new ShortOperationsListViewModel();

            Assert.True(operationsListViewModel.IsAnyOperationAdded == false && operationsListViewModel.Operations.Count == 0);
        }
    }
}
