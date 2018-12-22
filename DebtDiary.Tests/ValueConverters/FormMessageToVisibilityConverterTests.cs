using NUnit.Framework;
using System;
using System.Windows;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class FormMessageToVisibilityConverterTests
    {
        [TestCase(FormMessage.None, Visibility.Hidden)]
        [TestCase(FormMessage.DebtorExist, Visibility.Visible)]
        [TestCase("aaaa", Visibility.Hidden)]
        [TestCase("", Visibility.Hidden)]
        [TestCase(null, Visibility.Hidden)]
        public void TestConvert(object value, Visibility expectedResult)
        {
            FormMessageToVisibilityConverter converter = new FormMessageToVisibilityConverter();
            Visibility result = (Visibility)converter.Convert(value, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestConvertBack()
        {
            FormMessageToVisibilityConverter converter = new FormMessageToVisibilityConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
