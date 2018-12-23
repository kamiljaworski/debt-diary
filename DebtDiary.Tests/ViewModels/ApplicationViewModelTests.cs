using NUnit.Framework;
using System.Threading.Tasks;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class ApplicationViewModelTests
    {
        private ApplicationViewModel _applicationViewModel = null;

        [SetUp]
        public void Setup() => _applicationViewModel = new ApplicationViewModel();

        [Test]
        public async Task TestChangeCurrentPageAsync()
        {
            var page = ApplicationPage.DiaryPage;
            var result = await _applicationViewModel.ChangeCurrentPageAsync(page);

            Assert.That(result == true && _applicationViewModel.CurrentPage == page);
        }

        [Test]
        public async Task TestChangeCurrentSubpageAsync()
        {
            var subpage = ApplicationSubpage.AddDebtorSubpage;
            var result = await _applicationViewModel.ChangeCurrentSubpageAsync(subpage);

            Assert.That(result == true && _applicationViewModel.CurrentSubpage == subpage);
        }

        [Test]
        public async Task TestResetCurrentSubpage()
        {
            await _applicationViewModel.ChangeCurrentSubpageAsync(ApplicationSubpage.AddDebtorSubpage);
            _applicationViewModel.ResetCurrentSubpage();

            Assert.AreEqual(ApplicationSubpage.SummarySubpage, _applicationViewModel.CurrentSubpage);
        }
    }
}
