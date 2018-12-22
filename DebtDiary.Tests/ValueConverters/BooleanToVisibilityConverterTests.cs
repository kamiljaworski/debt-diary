using NUnit.Framework;
using System;
using System.Windows;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class BooleanToVisibilityConverterTests
    {
        [TestCase(true, null, Visibility.Hidden)]
        [TestCase(false, null, Visibility.Visible)]
        [TestCase(null, null, Visibility.Hidden)]
        [TestCase("aa", null, Visibility.Hidden)]
        [TestCase(false, "ButtonText", Visibility.Visible)]
        [TestCase(true, "ButtonText", Visibility.Hidden)]
        [TestCase(true, "SpinningText", Visibility.Visible)]
        [TestCase(false, "SpinningText", Visibility.Hidden)]
        public void TestConvert(object value, object parameter, Visibility expectedResult)
        {
            BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();
            Visibility result = (Visibility)converter.Convert(value, null, parameter, null);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestConvertBack()
        {
            BooleanToVisibilityConverter converter = new BooleanToVisibilityConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
