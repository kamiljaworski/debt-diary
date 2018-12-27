using NUnit.Framework;
using System;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class StatisticPanelMessageValueConverterTests
    {
        [TestCase(StatisticPanelMessage.DebtFemale, "-100.00", "100.00")]
        [TestCase(StatisticPanelMessage.DebtFemale, "-999.00", "999.00")]
        [TestCase(StatisticPanelMessage.DebtMale, "-99999999999.00", "99999999999.00")]
        [TestCase(StatisticPanelMessage.DebtMale, "99999999999.00", "99999999999.00")]
        [TestCase(StatisticPanelMessage.DebtMale, "", null)]
        [TestCase(StatisticPanelMessage.DebtMale, null, null)]
        [TestCase("DebtMale", "", null)]
        [TestCase("cz", "", null)]
        [TestCase("", "", null)]
        [TestCase(null, "", null)]
        [TestCase(null, null, null)]
        public void TestConvertWithArrayOfTwoObjects(object value1, object value2, string expectedResult)
        {
            StatisticPanelMessageValueConverter converter = new StatisticPanelMessageValueConverter();
            var result = (string)converter.Convert(new object[] { value1, value2 }, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(null, null)]
        [TestCase(new object[] { StatisticPanelMessage.DebtMale }, null)]
        public void TestConvertWithInvalidArray(object[] values, string expectedResult)
        {
            StatisticPanelMessageValueConverter converter = new StatisticPanelMessageValueConverter();
            var result = (string)converter.Convert(values, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }


        [Test]
        public void TestConvertBack()
        {
            StatisticPanelMessageValueConverter converter = new StatisticPanelMessageValueConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
