using NUnit.Framework;
using System;
using System.Globalization;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class ColorToStringConverterTests
    {
        [TestCase(Core.Color.Black, "black")]
        [TestCase(Core.Color.MediumAquamarine, "medium aquamarine")]
        [TestCase(Core.Color.PaleVioletRed, "pale violet red")]
        [TestCase(Core.Color.Tomato, "tomato")]
        [TestCase(null, "")]
        [TestCase("Black", "")]
        public void TestConvertWithEnglishLanguage(object color, string expectedColor)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            CultureInfo.CurrentUICulture = new CultureInfo("en-US", false);
            ColorToStringConverter converter = new ColorToStringConverter();
            string result = converter.Convert(color, null, null, null) as string;

            Assert.AreEqual(expectedColor, result);
        }

        [TestCase(Core.Color.Black, "czarny")]
        [TestCase(Core.Color.MediumAquamarine, "akwamaryna")]
        [TestCase(Core.Color.PaleVioletRed, "wyblakły fioletowo-czerwony")]
        [TestCase(Core.Color.Tomato, "pomidorowy")]
        [TestCase(null, "")]
        [TestCase("Black", "")]
        public void TestConvertWithPolishLanguage(object color, string expectedColor)
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            CultureInfo.CurrentUICulture = new CultureInfo("pl-PL", false);
            ColorToStringConverter converter = new ColorToStringConverter();
            string result = converter.Convert(color, null, null, null) as string;

            Assert.AreEqual(expectedColor, result);
        }

        [Test]
        public void TestConvertBack()
        {
            ColorToStringConverter converter = new ColorToStringConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
