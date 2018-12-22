using NUnit.Framework;
using System;
using System.Windows.Media;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class StatisticPanelMessageToBrushConverterTests
    {
        [TestCase(StatisticPanelMessage.DebtMale, "-100", "#FFE2131D")]
        [TestCase(StatisticPanelMessage.DebtFemale, "", "#FF8BC34A")]
        [TestCase(StatisticPanelMessage.SumOfDebts, "as", "#FF8BC34A")]
        [TestCase(StatisticPanelMessage.DebtMale, null, "#FF8BC34A")]
        [TestCase(StatisticPanelMessage.NumberOfOperations, null, "#FF00BCD4")]
        [TestCase(StatisticPanelMessage.NumberOfOperations, "-100", "#FF00BCD4")]
        [TestCase(StatisticPanelMessage.LastOperation, null, "#FFE91E63")]
        [TestCase(StatisticPanelMessage.AdditionDate, null, "#FFFF9800")]
        [TestCase(StatisticPanelMessage.NumberOfDebtors, null, "#FFFF9800")]
        [TestCase(null, null, "#FF008000")]
        [TestCase("", null, "#FF008000")]
        [TestCase("NumberOfDebtors", null, "#FF008000")]
        public void TestConvertWithArray(object value1, object value2, string hexColor)
        {
            StatisticPanelMessageToBrushConverter converter = new StatisticPanelMessageToBrushConverter();
            Color result = (Color)converter.Convert(new object[] { value1, value2 }, null, null, null);

            Assert.AreEqual(hexColor, result.ToString());
        }

        [TestCase(null, "#FF008000")]
        [TestCase("", "#FF008000")]
        [TestCase("asdsa", "#FF008000")]
        [TestCase(StatisticPanelMessage.DebtMale, "#FF008000")]
        public void TestConvertWithoutAnArray(object value, string hexColor)
        {
            StatisticPanelMessageToBrushConverter converter = new StatisticPanelMessageToBrushConverter();
            Color result = (Color)converter.Convert(null, null, null, null);

            Assert.AreEqual(hexColor, result.ToString());
        }

        [Test]
        public void TestConvertBack()
        {
            StatisticPanelMessageToBrushConverter converter = new StatisticPanelMessageToBrushConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
