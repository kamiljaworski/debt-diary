using NUnit.Framework;
using System.Globalization;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class FormattingHelpersTests
    {
        [TestCase("Jan", "Kowalski", "JK")]
        [TestCase("Morgan", "Freeman", "MF")]
        [TestCase("Łukasz", "Stanisławowski", "ŁS")]
        public void TestGetInitialsWithValidData(string firstName, string lastName, string expectedValue)
        {
            string actualValue = FormattingHelpers.GetInitials(firstName, lastName);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(null, null, "  ")]
        [TestCase("", "", "  ")]
        [TestCase(null, "", "  ")]
        [TestCase("", null, "  ")]
        [TestCase("", "TEST", " T")]
        [TestCase("TEST", "", "T ")]
        [TestCase(null, "TEST", " T")]
        [TestCase("TEST", null, "T ")]
        public void TestGetInitialsWithInvalidData(string firstName, string lastName, string expectedValue)
        {
            string actualValue = FormattingHelpers.GetInitials(firstName, lastName);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(2531.78, "2,531.78 $")]
        [TestCase(-9999.99, "-9,999.99 $")]
        public void TestGetFormattedCurrencyWithUSCulture(decimal number, string expectedValue)
        {
            CultureInfo.CurrentCulture = new CultureInfo("en-US", false);
            string actualValue = FormattingHelpers.GetFormattedCurrency(number);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(2531.78, "2 531,78 zł")]
        [TestCase(-9999.99, "-9 999,99 zł")]
        public void TestGetFormattedCurrencyWithPLCulture(decimal number, string expectedValue)
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            string actualValue = FormattingHelpers.GetFormattedCurrency(number);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(2531.78, "2 531,78 zł")]
        [TestCase(-9999.99, "-9 999,99 zł")]
        public void TestGetFormattedCurrencyWithDoubleParameterAndPLCulture(double number, string expectedValue)
        {
            CultureInfo.CurrentCulture = new CultureInfo("pl-PL", false);
            string actualValue = FormattingHelpers.GetFormattedCurrency(number);
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
