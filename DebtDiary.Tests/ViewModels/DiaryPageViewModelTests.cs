using DebtDiary.Core;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class DiaryPageViewModelTests
    {
        // Fake user
        private User _user = null;

        [Test]
        public void TestLogoutCommandResetsSelectedDebtor()
        {
            Mock<IApplicationViewModel> mockClientDataStore = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(mockClientDataStore.Object, stubClientDataStore.Object);
            mockClientDataStore.SetupProperty(x => x.SelectedDebtor);
            mockClientDataStore.Object.SelectedDebtor = _user.Debtors.First();

            diaryPageViewModel.LogoutCommand.Execute(null);

            Assert.AreEqual(null, mockClientDataStore.Object.SelectedDebtor);
        }

        [Test]
        public void TestLogoutCommandLogsTheUserOutInClientDataStore()
        {
            Mock<IApplicationViewModel> stubApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> mockClientDataStore = new Mock<IClientDataStore>();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(stubApplicationViewModel.Object, mockClientDataStore.Object);

            diaryPageViewModel.LogoutCommand.Execute(null);

            mockClientDataStore.Verify(x => x.LogoutUser(), Times.Once());
        }

        [Test]
        public void TestLogoutCommandChangesCurrentPageToLoginPageInApplicationViewModel()
        {
            Mock<IApplicationViewModel> stubApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(stubApplicationViewModel.Object, stubClientDataStore.Object);
            ApplicationPage currentPage = ApplicationPage.DiaryPage;
            stubApplicationViewModel.Setup(x => x.ChangeCurrentPageAsync(It.IsAny<ApplicationPage>())).Callback<ApplicationPage>(a => currentPage = a);

            diaryPageViewModel.LogoutCommand.Execute(null);

            Assert.AreEqual(ApplicationPage.LoginPage, currentPage);
        }

        [Test]
        public void TestSortCommandWithCurrentDescendingSortTypeSortsDebtorsListInAscendingSortType()
        {
            Mock<IApplicationViewModel> stubApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(stubApplicationViewModel.Object, stubClientDataStore.Object);
            diaryPageViewModel.SortType = SortType.Descending;

            diaryPageViewModel.SortCommand.Execute(null);

            bool result = diaryPageViewModel.DebtorsList.Debtors.First().Debt <= diaryPageViewModel.DebtorsList.Debtors.Last().Debt;
            Assert.True(result);
        }

        [Test]
        public void TestSortCommandWithCurrentAscendingSortTypeSortsDebtorsListInDescendingSortType()
        {
            Mock<IApplicationViewModel> stubApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(stubApplicationViewModel.Object, stubClientDataStore.Object);
            diaryPageViewModel.SortType = SortType.Ascending;

            diaryPageViewModel.SortCommand.Execute(null);

            bool result = diaryPageViewModel.DebtorsList.Debtors.First().Debt >= diaryPageViewModel.DebtorsList.Debtors.Last().Debt;
            Assert.True(result);
        }

        [Test]
        public void TestSummaryCommandResetsSelectedDebtor()
        {
            Mock<IApplicationViewModel> mockApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            mockApplicationViewModel.SetupProperty(x => x.SelectedDebtor);
            mockApplicationViewModel.Object.SelectedDebtor = _user.Debtors.First();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(mockApplicationViewModel.Object, stubClientDataStore.Object);

            diaryPageViewModel.SummaryCommand.Execute(null);

            bool result = mockApplicationViewModel.Object.SelectedDebtor == null;
            Assert.True(result);
        }

        [Test]
        public void TestSummaryCommandChangesCurrentApplicationSubpageToSummarySubpage()
        {
            Mock<IApplicationViewModel> mockApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(mockApplicationViewModel.Object, stubClientDataStore.Object);

            diaryPageViewModel.SummaryCommand.Execute(null);

            mockApplicationViewModel.Verify(x => x.ChangeCurrentSubpageAsync(ApplicationSubpage.SummarySubpage), Times.Once());
        }

        [Test]
        public void TestMyAccountCommandResetsSelectedDebtor()
        {
            Mock<IApplicationViewModel> mockApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            mockApplicationViewModel.SetupProperty(x => x.SelectedDebtor);
            mockApplicationViewModel.Object.SelectedDebtor = _user.Debtors.First();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(mockApplicationViewModel.Object, stubClientDataStore.Object);

            diaryPageViewModel.MyAccountCommand.Execute(null);

            bool result = mockApplicationViewModel.Object.SelectedDebtor == null;
            Assert.True(result);
        }

        [Test]
        public void TestMyAccountCommandChangesCurrentApplicationSubpageToSummarySubpage()
        {
            Mock<IApplicationViewModel> mockApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(mockApplicationViewModel.Object, stubClientDataStore.Object);

            diaryPageViewModel.MyAccountCommand.Execute(null);

            mockApplicationViewModel.Verify(x => x.ChangeCurrentSubpageAsync(ApplicationSubpage.MyAccountSubpage), Times.Once());
        }

        [Test]
        public void TestAddDebtorCommandResetsSelectedDebtor()
        {
            Mock<IApplicationViewModel> mockApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            mockApplicationViewModel.SetupProperty(x => x.SelectedDebtor);
            mockApplicationViewModel.Object.SelectedDebtor = _user.Debtors.First();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(mockApplicationViewModel.Object, stubClientDataStore.Object);

            diaryPageViewModel.AddDebtorCommand.Execute(null);

            bool result = mockApplicationViewModel.Object.SelectedDebtor == null;
            Assert.True(result);
        }

        [Test]
        public void TestAddDebtorCommandChangesCurrentApplicationSubpageToSummarySubpage()
        {
            Mock<IApplicationViewModel> mockApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(mockApplicationViewModel.Object, stubClientDataStore.Object);

            diaryPageViewModel.AddDebtorCommand.Execute(null);

            mockApplicationViewModel.Verify(x => x.ChangeCurrentSubpageAsync(ApplicationSubpage.AddDebtorSubpage), Times.Once());
        }

        [Test]
        public void TestUpdateDebtorsListCallsUpdateMethodInDebtorsListViewModel()
        {
            Mock<IApplicationViewModel> stubApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(stubApplicationViewModel.Object, stubClientDataStore.Object);

            stubClientDataStore.Setup(x => x.LoggedUser).Returns(_user);
            diaryPageViewModel.UpdateDebtorsList();

            bool result = diaryPageViewModel.DebtorsList.Debtors.Count == _user.Debtors.Count;
            Assert.True(result);
        }

        [Test]
        public void TestUpdateUsersDataDoesNotThrowExceptionMadeByParameterlessConstructor()
        {
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel();

            Assert.DoesNotThrow(() => diaryPageViewModel.UpdateUsersData());
        }

        public DiaryPageViewModelTests()
        {
            // Prepare fake user
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
