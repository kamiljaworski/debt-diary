using DebtDiary.Core;
using NUnit.Framework;
using System;
using System.Globalization;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class GenderStringsConverterTests
    {
        [TestCase(Gender.Female, "Debt", "Jest ci winna")]
        [TestCase(Gender.Male, "Debt", "Jest ci winny")]
        [TestCase(Gender.Male, "Deasdasdabt", "")]
        [TestCase(Gender.Male, "", "")]
        [TestCase(Gender.Male, null, "")]
        [TestCase("Male", "Debt", "")]
        [TestCase("", "Debt", "")]
        [TestCase(null, "Debt", "")]
        public void TestConvertWithPolishLanguage(object value, object parameter, string expectedColor)
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            CultureInfo.CurrentUICulture = new CultureInfo("pl-PL", false);
            GenderStringsConverter converter = new GenderStringsConverter();
            string result = converter.Convert(value, null, parameter, null) as string;

            Assert.AreEqual(expectedColor, result);
        }

        [Test]
        public void TestConvertBack()
        {
            GenderStringsConverter converter = new GenderStringsConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
