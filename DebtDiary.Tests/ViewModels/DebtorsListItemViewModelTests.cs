using DebtDiary.Core;
using Moq;
using NUnit.Framework;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class DebtorsListItemViewModelTests
    {
        private Debtor debtor = null;

        // Mocks
        private Mock<IApplicationViewModel> _mockApplicationVM = null;
        private Mock<IDebtorsListViewModel> _mockDebtorsListVM = null;

        // Stubs
        private Mock<IApplicationViewModel> _stubApplicationVM = null;
        private Mock<IDebtorsListViewModel> _stubDebtorsListVM = null;

        [SetUp]
        public void Setup()
        {
            debtor = new Debtor { Id = 1, FirstName = "John", LastName = "Cena", AvatarColor = Color.MediumAquamarine };
            _mockApplicationVM = new Mock<IApplicationViewModel>();
            _mockDebtorsListVM = new Mock<IDebtorsListViewModel>();
            _stubApplicationVM = new Mock<IApplicationViewModel>();
            _stubDebtorsListVM = new Mock<IDebtorsListViewModel>();
        }

        [Test]
        public void TestOpenDebtorSubpageChangesCurrentSubpage()
        {
            DebtorsListItemViewModel debtorsListItem = new DebtorsListItemViewModel(debtor, _mockApplicationVM.Object, _stubDebtorsListVM.Object);
            ApplicationSubpage currentSubpage = ApplicationSubpage.SummarySubpage;
            _mockApplicationVM.Setup(x => x.ChangeCurrentSubpageAsync(It.IsAny<ApplicationSubpage>())).Callback<ApplicationSubpage>(a => currentSubpage = a);

            debtorsListItem.OpenDebtorSubpage.Execute(null);

            Assert.AreEqual(ApplicationSubpage.DebtorInfoSubpage, currentSubpage);
        }

        [Test]
        public void TestOpenDebtorSubpageChangesSelectedDebtor()
        {
            DebtorsListItemViewModel debtorsListItem = new DebtorsListItemViewModel(debtor, _mockApplicationVM.Object, _stubDebtorsListVM.Object);
            _mockApplicationVM.SetupProperty(m => m.SelectedDebtor);

            debtorsListItem.OpenDebtorSubpage.Execute(null);

            Assert.AreSame(debtor, _mockApplicationVM.Object.SelectedDebtor);
        }

        [Test]
        public void TestOpenDebtorSubpageCallsDebtorsListUpdateMethod()
        {
            DebtorsListItemViewModel debtorsListItem = new DebtorsListItemViewModel(debtor, _stubApplicationVM.Object, _mockDebtorsListVM.Object);

            debtorsListItem.OpenDebtorSubpage.Execute(null);

            _mockDebtorsListVM.Verify(x => x.Update(), Times.Once());
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
            DebtorsListItemViewModel debtorsListItem = new DebtorsListItemViewModel(debtor, _stubApplicationVM.Object, _stubDebtorsListVM.Object);

            var formattedDebt = debtorsListItem.FormattedDebt;

            Assert.AreEqual(FormattingHelpers.GetFormattedCurrency(0.0m), formattedDebt);
        }
    }

}
