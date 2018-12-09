using NUnit.Framework;
using System;
using System.Globalization;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class ColorToStringConverterTest
    {
        [TestCase(Core.Color.Black, "Black")]
        [TestCase(Core.Color.MediumAquamarine, "Medium aquamarine")]
        [TestCase(Core.Color.PaleVioletRed, "Pale violet red")]
        [TestCase(Core.Color.Tomato, "Tomato")]
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

        [TestCase(Core.Color.Black, "Czarny")]
        [TestCase(Core.Color.MediumAquamarine, "Średnia akwamaryna")]
        [TestCase(Core.Color.PaleVioletRed, "Bladofioletowa czerwień")]
        [TestCase(Core.Color.Tomato, "Pomidorowy")]
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
