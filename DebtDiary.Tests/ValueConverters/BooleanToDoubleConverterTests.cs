using NUnit.Framework;
using System;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class BooleanToDoubleConverterTests
    {
        [TestCase(true, 5.0d)]
        [TestCase(false, 0.0d)]
        [TestCase(null, 0.0d)]
        [TestCase("aa", 0.0d)]
        public void TestConvert(object value, double expectedResult)
        {
            BooleanToDoubleConverter converter = new BooleanToDoubleConverter();
            double result = (double)converter.Convert(value, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestConvertBack()
        {
            BooleanToDoubleConverter converter = new BooleanToDoubleConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
