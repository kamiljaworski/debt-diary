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
        [TestCase(StatisticPanelMessage.DebtMale, "", "")]
        [TestCase(StatisticPanelMessage.DebtMale, null, "")]
        [TestCase("DebtMale", "", "")]
        [TestCase("cz", "", "")]
        [TestCase("", "", "")]
        [TestCase(null, "", "")]
        [TestCase(null, null, "")]
        public void TestConvertWithArrayOfTwoObjects(object value1, object value2, string expectedResult)
        {
            StatisticPanelMessageValueConverter converter = new StatisticPanelMessageValueConverter();
            var result = (string)converter.Convert(new object[] { value1, value2 }, null, null, null);

            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestCase(null, "")]
        [TestCase(new object[] { StatisticPanelMessage.DebtMale }, "")]
        public void TestConvertWithInvalidArray(object[] values, string expectedResult)
        {
            StatisticPanelMessageValueConverter converter = new StatisticPanelMessageValueConverter();
            var result = (string)converter.Convert(values, null, null, null);

            Assert.AreEqual(expectedResult, result.ToString());
        }


        [Test]
        public void TestConvertBack()
        {
            StatisticPanelMessageValueConverter converter = new StatisticPanelMessageValueConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
