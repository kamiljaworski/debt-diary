using NUnit.Framework;
using System;
using System.Windows;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class OpacityToBolleanConverterTest
    {
        [TestCase(1.0, true)]
        [TestCase(0.900001, true)]
        [TestCase(0.9, false)]
        [TestCase("as", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void TestConvert(object value, bool expectedResult)
        {
            OpacityToBolleanConverter converter = new OpacityToBolleanConverter();
            bool result = (bool)converter.Convert(value, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestConvertBack()
        {
            OpacityToBolleanConverter converter = new OpacityToBolleanConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
