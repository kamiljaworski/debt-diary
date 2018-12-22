using DebtDiary.Core;
using NUnit.Framework;
using System;
using System.Windows;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class TextToBooleanConverterTests
    {
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("asdasd", true)]
        public void TestConvert(object value, bool expectedResult)
        {
            TextToBooleanConverter converter = new TextToBooleanConverter();
            bool result = (bool)converter.Convert(value, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestConvertBack()
        {
            TextToBooleanConverter converter = new TextToBooleanConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
