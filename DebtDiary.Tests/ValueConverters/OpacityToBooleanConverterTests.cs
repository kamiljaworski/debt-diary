using NUnit.Framework;
using System;
using System.Windows;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class OpacityToBooleanConverterTests
    {
        [TestCase(1.0, true)]
        [TestCase(0.900001, true)]
        [TestCase(0.9, false)]
        [TestCase("as", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void TestConvert(object value, bool expectedResult)
        {
            OpacityToBooleanConverter converter = new OpacityToBooleanConverter();
            bool result = (bool)converter.Convert(value, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestConvertBack()
        {
            OpacityToBooleanConverter converter = new OpacityToBooleanConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
