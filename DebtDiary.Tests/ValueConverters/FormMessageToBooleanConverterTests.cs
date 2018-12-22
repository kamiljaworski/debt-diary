using NUnit.Framework;
using System;
using System.Globalization;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class FormMessageToBooleanConverterTests
    {
        [TestCase(FormMessage.DebtorExist, true)]
        [TestCase(FormMessage.None, false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        [TestCase("asd", false)]
        public void TestConvert(object value, bool expectedValue)
        {
            FormMessageToBooleanConverter converter = new FormMessageToBooleanConverter();
            bool result = (bool)converter.Convert(value, null, null, null);

            Assert.AreEqual(expectedValue, result);
        }

        [Test]
        public void TestConvertBack()
        {
            FormMessageToBooleanConverter converter = new FormMessageToBooleanConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
