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
        public void TestLogoutResetSelectedDebtor()
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
        public void TestLogoutLogTheUserOutInClientDataStore()
        {
            Mock<IApplicationViewModel> stubApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> mockClientDataStore = new Mock<IClientDataStore>();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(stubApplicationViewModel.Object, mockClientDataStore.Object);

            diaryPageViewModel.LogoutCommand.Execute(null);

            mockClientDataStore.Verify(x => x.LogoutUser(), Times.Once());
        }

        [Test]
        public void TestLogoutChangeCurrentPageToLoginPageInApplicationViewModel()
        {
            Mock<IApplicationViewModel> stubApplicationViewModel = new Mock<IApplicationViewModel>();
            Mock<IClientDataStore> stubClientDataStore = new Mock<IClientDataStore>();
            DiaryPageViewModel diaryPageViewModel = new DiaryPageViewModel(stubApplicationViewModel.Object, stubClientDataStore.Object);
            ApplicationPage currentPage = ApplicationPage.DiaryPage;
            stubApplicationViewModel.Setup(x => x.ChangeCurrentPageAsync(It.IsAny<ApplicationPage>())).Callback<ApplicationPage>(a => currentPage = a);

            diaryPageViewModel.LogoutCommand.Execute(null);

            Assert.AreEqual(ApplicationPage.LoginPage, currentPage);
        }

        // TODO: Add other tests to cover whole class

        // Prepare fake user
        public DiaryPageViewModelTests()
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
