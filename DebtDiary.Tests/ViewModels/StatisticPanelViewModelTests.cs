using DebtDiary.Core;
using NUnit.Framework;
using System;

namespace DebtDiary.Tests.ViewModels
{
    [TestFixture]
    public class StatisticPanelViewModelTests
    {
        [Test]
        public void TestConstructorWithValidParameters()
        {
            var message = StatisticPanelMessage.DebtMale;
            var value = "-100,00";
            var gender = Gender.Female;

            var statisticPanel = new StatisticPanelViewModel(message, value, gender);

            Assert.True(statisticPanel.Message == message && statisticPanel.Value == value && statisticPanel.UsersGender == gender);
        }

        [Test]
        public void TestPassingNullThroughConstructorThrowsExcepton()
        {
            Assert.Throws<ArgumentNullException>(() => new StatisticPanelViewModel(StatisticPanelMessage.DebtMale, null, Gender.Female));
        }

    }
}
