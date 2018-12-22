using NUnit.Framework;
using System;
using System.Globalization;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class DialogMessageToStringConverterTests
    {
        [TestCase(DialogMessage.AccountCreated, null, "Your account has been created")]
        [TestCase(DialogMessage.AccountCreated, "Title", "Your account has been created")]
        [TestCase(DialogMessage.AccountCreated, "Subtitle", "You can log in now")]
        [TestCase(DialogMessage.None, "Subtitle", "")]
        [TestCase(DialogMessage.None, "asdsad", "")]
        [TestCase("Aa", "Subtitle", "")]
        [TestCase("Aa", null, "")]
        [TestCase(null, null, "")]
        public void TestConvertWithEnglishLanguage(object value, object parameter, string expectedValue)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            CultureInfo.CurrentUICulture = new CultureInfo("en-US", false);
            DialogMessageToStringConverter converter = new DialogMessageToStringConverter();
            string result = converter.Convert(value, null, parameter, null) as string;

            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void TestConvertBack()
        {
            DialogMessageToStringConverter converter = new DialogMessageToStringConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
