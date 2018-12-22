using NUnit.Framework;
using System;
using System.Windows.Media;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class DecimalToBrushConverterTests
    {
        [TestCase(100.0, "Operation", "#FF008000")]
        [TestCase(-100.0, "Operation", "#FFFF0000")]
        [TestCase(0.0, "Operation", "#FF000000")]
        [TestCase(100.0, null, "#FF90EE90")]
        [TestCase(-100.0, "", "#FFFF7F50")]
        [TestCase(0.0, "asdsad", "#FFFAFAD2")]
        public void TestConvertWithCorrectNumbers(decimal value, object parameter, string hexColor)
        {
            DecimalToBrushConverter converter = new DecimalToBrushConverter();
            SolidColorBrush result = (SolidColorBrush)converter.Convert(value, null, parameter, null);

            Assert.AreEqual(hexColor, result.ToString());
        }

        [TestCase("string", null, "#FF90EE90")]
        [TestCase(null, null, "#FF90EE90")]
        public void TestConvertWithIncorrectNumbers(object value, object parameter, string hexColor)
        {
            DecimalToBrushConverter converter = new DecimalToBrushConverter();
            SolidColorBrush result = (SolidColorBrush)converter.Convert(value, null, parameter, null);

            Assert.AreEqual(hexColor, result.ToString());
        }

        [Test]
        public void TestConvertBack()
        {
            DecimalToBrushConverter converter = new DecimalToBrushConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
