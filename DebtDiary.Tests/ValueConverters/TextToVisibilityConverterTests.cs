using NUnit.Framework;
using System;
using System.Windows;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class TextToVisibilityConverterTests
    {
        [TestCase("", Visibility.Visible)]
        [TestCase(null, Visibility.Visible)]
        [TestCase("asdasd", Visibility.Hidden)]
        public void TestConvert(object value, Visibility expectedResult)
        {
            TextToVisibilityConverter converter = new TextToVisibilityConverter();
            var result = (Visibility)converter.Convert(value, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestConvertBack()
        {
            TextToVisibilityConverter converter = new TextToVisibilityConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
