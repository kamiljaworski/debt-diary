using DebtDiary.Core;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Windows.Media;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class StatisticPanelMessageToStringConverterTests
    {
        [TestCase(StatisticPanelMessage.DebtFemale, "-100", Gender.Female, "Jesteś jej winna")]
        [TestCase(StatisticPanelMessage.DebtFemale, "100", Gender.Female, "Jest ci winna")]
        [TestCase(StatisticPanelMessage.DebtFemale, "-100", Gender.Male, "Jesteś jej winny")]
        [TestCase(StatisticPanelMessage.DebtFemale, "100", Gender.Male, "Jest ci winna")]
        [TestCase(StatisticPanelMessage.DebtMale, "-100", Gender.Male, "Jesteś mu winny")]
        [TestCase(StatisticPanelMessage.DebtMale, "100", Gender.Male, "Jest ci winny")]
        [TestCase(StatisticPanelMessage.DebtMale, "-100", Gender.Female, "Jesteś mu winna")]
        [TestCase(StatisticPanelMessage.DebtMale, "100", Gender.Female, "Jest ci winny")]
        [TestCase("DebtMale", "100", Gender.Female, "")]
        [TestCase("", "100", Gender.Female, "")]
        [TestCase(null, "100", Gender.Female, "")]
        [TestCase(StatisticPanelMessage.DebtMale, "sadsa", Gender.Female, "Jest ci winny")]
        [TestCase(StatisticPanelMessage.DebtMale, "", Gender.Female, "")]
        [TestCase(StatisticPanelMessage.DebtMale, null, Gender.Female, "")]
        [TestCase(StatisticPanelMessage.DebtMale, "100", Gender.None, "Jest ci winny")]
        [TestCase(StatisticPanelMessage.DebtMale, "100", "Female", "Jest ci winny")]
        [TestCase(StatisticPanelMessage.DebtMale, "100", "sd", "Jest ci winny")]
        [TestCase(StatisticPanelMessage.DebtMale, "100", "", "Jest ci winny")]
        [TestCase(StatisticPanelMessage.DebtMale, "100", null, "Jest ci winny")]
        [TestCase(null, null, null, "")]
        public void TestConvertWithPolishLanguageAndArrayOfThreeObjects(object value1, object value2, object value3, string expectedResult)
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            CultureInfo.CurrentUICulture = new CultureInfo("pl-PL", false);
            StatisticPanelMessageToStringConverter converter = new StatisticPanelMessageToStringConverter();
            var result = (string)converter.Convert(new object[] { value1, value2, value3 }, null, null, null);

            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestCase(null, "")]
        [TestCase(new object[] { StatisticPanelMessage.DebtFemale, "-100" }, "")]
        [TestCase(new object[] { StatisticPanelMessage.DebtFemale }, "")]
        public void TestConvertWithPolishLanguageAndInvalidArray(object[] value, string expectedResult)
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            CultureInfo.CurrentUICulture = new CultureInfo("pl-PL", false);
            StatisticPanelMessageToStringConverter converter = new StatisticPanelMessageToStringConverter();
            var result = (string)converter.Convert(value, null, null, null);

            Assert.AreEqual(expectedResult, result.ToString());
        }

        [Test]
        public void TestConvertBack()
        {
            StatisticPanelMessageToStringConverter converter = new StatisticPanelMessageToStringConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
