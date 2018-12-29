using DebtDiary.Core;
using Moq;
using NUnit.Framework;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class DebtorsListItemViewModelTests
    {
        private Debtor _debtor = null;

        [SetUp]
        public void Setup()
        {
            _debtor = new Debtor { Id = 1, FirstName = "John", LastName = "Cena", AvatarColor = Color.MediumAquamarine };
        }

        [Test]
        public void TestOpenDebtorSubpageChangesCurrentSubpage()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDebtorsListViewModel> stubDebtorsListVM = new Mock<IDebtorsListViewModel>();
            DebtorsListItemViewModel debtorsListItem = new DebtorsListItemViewModel(_debtor, mockApplicationVM.Object, stubDebtorsListVM.Object);
            ApplicationSubpage currentSubpage = ApplicationSubpage.SummarySubpage;
            mockApplicationVM.Setup(x => x.ChangeCurrentSubpageAsync(It.IsAny<ApplicationSubpage>())).Callback<ApplicationSubpage>(a => currentSubpage = a);

            debtorsListItem.OpenDebtorSubpage.Execute(null);

            Assert.AreEqual(ApplicationSubpage.DebtorInfoSubpage, currentSubpage);
        }

        [Test]
        public void TestOpenDebtorSubpageChangesSelectedDebtor()
        {
            Mock<IApplicationViewModel> mockApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDebtorsListViewModel> stubDebtorsListVM = new Mock<IDebtorsListViewModel>();
            DebtorsListItemViewModel debtorsListItem = new DebtorsListItemViewModel(_debtor, mockApplicationVM.Object, stubDebtorsListVM.Object);
            mockApplicationVM.SetupProperty(m => m.SelectedDebtor);

            debtorsListItem.OpenDebtorSubpage.Execute(null);

            Assert.AreSame(_debtor, mockApplicationVM.Object.SelectedDebtor);
        }

        [Test]
        public void TestOpenDebtorSubpageCallsDebtorsListUpdateMethod()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDebtorsListViewModel> mockDebtorsListVM = new Mock<IDebtorsListViewModel>();
            DebtorsListItemViewModel debtorsListItem = new DebtorsListItemViewModel(_debtor, stubApplicationVM.Object, mockDebtorsListVM.Object);

            debtorsListItem.OpenDebtorSubpage.Execute(null);

            mockDebtorsListVM.Verify(x => x.Update(), Times.Once());
        }

        [Test]
        public void TestOpenDebtorSubpageDoesNotThrowExcpetionWhenInDesignMode()
        {
            DebtorsListItemViewModel debtorsListItem = new DebtorsListItemViewModel();

            Assert.DoesNotThrow(() => debtorsListItem.OpenDebtorSubpage.Execute(null));
        }

        [Test]
        public void TestFormattedDebt()
        {
            Mock<IApplicationViewModel> stubApplicationVM = new Mock<IApplicationViewModel>();
            Mock<IDebtorsListViewModel> stubDebtorsListVM = new Mock<IDebtorsListViewModel>();
            DebtorsListItemViewModel debtorsListItem = new DebtorsListItemViewModel(_debtor, stubApplicationVM.Object, stubDebtorsListVM.Object);

            var formattedDebt = debtorsListItem.FormattedDebt;

            Assert.AreEqual(FormattingHelpers.GetFormattedCurrency(0.0m), formattedDebt);
        }
    }

}
