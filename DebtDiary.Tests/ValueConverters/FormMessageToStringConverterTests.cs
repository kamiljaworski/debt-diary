using NUnit.Framework;
using System;
using System.Globalization;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class FormMessageToStringConverterTests
    {
        [TestCase(FormMessage.EmptyEmail, "Enter e-mail address")]
        [TestCase(FormMessage.None, "")]
        [TestCase("asdsadsad", "")]
        [TestCase("", "")]
        [TestCase(null, "")]
        public void TestConvertWithEnglishLanguage(object value, string expectedValue)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            CultureInfo.CurrentUICulture = new CultureInfo("en-US", false);
            FormMessageToStringConverter converter = new FormMessageToStringConverter();
            string result = converter.Convert(value, null, null, null) as string;

            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void TestConvertBack()
        {
            FormMessageToStringConverter converter = new FormMessageToStringConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
