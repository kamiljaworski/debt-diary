using NUnit.Framework;
using System;
using System.Windows.Media;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class ColorToBrushConverterTests
    {
        [TestCase(Core.Color.Blue, "#FF0000FF")]
        [TestCase(Core.Color.Green, "#FF008000")]
        [TestCase(null, "#FF008000")]
        public void TestConvert(object color, string hexColor)
        {
            ColorToBrushConverter converter = new ColorToBrushConverter();
            object result = (SolidColorBrush)converter.Convert(color, null, null, null);

            Assert.AreEqual(hexColor, result.ToString());
        }

        [Test]
        public void TestConvertBack()
        {
            ColorToBrushConverter converter = new ColorToBrushConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
