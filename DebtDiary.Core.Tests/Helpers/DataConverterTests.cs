using NUnit.Framework;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class DataConverterTests
    {
        [TestCase("999.99", 999.99)]
        [TestCase("-999.99", -999.99)]
        [TestCase("999,99", 999.99)]
        [TestCase("-999,99", -999.99)]
        public void TestToDecimalWithValidDataCommaAndDot(string number, decimal expectedValue)
        {
            bool result = DataConverter.ToDecimal(number, out decimal actualValue);

            Assert.IsTrue(result);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("asdsad", 0.0)]
        [TestCase("-999.99d", 0.0)]
        [TestCase("", 0.0)]
        [TestCase(null, 0.0)]
        public void TestToDecimalWithInvalidData(string number, decimal expectedValue)
        {
            bool result = DataConverter.ToDecimal(number, out decimal actualValue);

            Assert.IsFalse(result);
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
