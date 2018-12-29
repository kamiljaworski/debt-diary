using DebtDiary.Core;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class DebtorsListViewModelTests
    {
        // Fake user
        private User _user = null;

        [Test]
        public void TestConstructorCreatesListOfAllUsersDebtors()
        {
            Mock<IApplicationViewModel> stubApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);

            DebtorsListViewModel debtorsList = new DebtorsListViewModel(stubApplicationViewModel.Object, stubClientDataStore.Object);

            Assert.True(debtorsList.IsAnyDebtorAdded == true && debtorsList.Debtors.Count == _user.Debtors.Count);
        }

        [Test]
        public void TestSortWithAscendingSortType()
        {
            Mock<IApplicationViewModel> stubApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);

            DebtorsListViewModel debtorsList = new DebtorsListViewModel(stubApplicationViewModel.Object, stubClientDataStore.Object);

            debtorsList.Sort(SortType.Ascending);

            Assert.True(debtorsList.Debtors.First().Debt <= debtorsList.Debtors.Last().Debt);
        }

        [Test]
        public void TestSortWithDescendingSortType()
        {
            Mock<IApplicationViewModel> stubApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);

            DebtorsListViewModel debtorsList = new DebtorsListViewModel(stubApplicationViewModel.Object, stubClientDataStore.Object);

            debtorsList.Sort(SortType.Descending);

            Assert.True(debtorsList.Debtors.First().Debt >= debtorsList.Debtors.Last().Debt);
        }

        [Test]
        public void TestUpdateDoesNotThrowExcpetionWhenInDesignMode()
        {
            DebtorsListViewModel debtorsList = new DebtorsListViewModel();

            Assert.DoesNotThrow(() => debtorsList.Update());
        }


        public DebtorsListViewModelTests()
        {
            _user = new User
            {
                FirstName = "John",
                LastName = "Cena",
                Debtors = new System.Collections.Generic.List<Debtor>
                {
                    new Debtor
                    {
                        Id = 1,
                        FirstName = "Annette",
                        LastName = "Montz",
                        Operations = new System.Collections.Generic.List<Operation>
                        {
                            new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(10), Value = 100.0m },
                            new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(9), Value = 200.0m },
                            new Operation {  AdditionDate = DateTime.Now - TimeSpan.FromDays(2), Value = 100.0m },
                            new Operation { AdditionDate = DateTime.Now, Value = 500.0m },
                        }
                    },

                    new Debtor
                    {
                        Id = 2,
                        FirstName = "Tim",
                        LastName = "Giordano",
                        Operations = new System.Collections.Generic.List<Operation>
                        {
                            new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(8), Value = -100.0m },
                            new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(7), Value = -900.0m },
                        }
                    },

                    new Debtor
                    {
                        Id = 3,
                        FirstName = "Antione",
                        LastName = "Feliciano",
                        Operations = new System.Collections.Generic.List<Operation>
                        {
                            new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(3), Value = -100.0m },
                            new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(2), Value = 900.0m },
                            new Operation { AdditionDate = DateTime.Now - TimeSpan.FromDays(1), Value = -200.0m },
                        }
                    },
                }
            };
        }
    }
}
