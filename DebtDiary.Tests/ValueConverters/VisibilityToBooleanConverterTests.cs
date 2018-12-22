using DebtDiary.Core;
using NUnit.Framework;
using System;
using System.Windows;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class VisibilityToBooleanConverterTests
    {
        [TestCase(Visibility.Visible, true)]
        [TestCase(Visibility.Hidden, false)]
        [TestCase(Visibility.Collapsed, false)]
        [TestCase("Visible", false)]
        [TestCase("asd", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void TestConvert(object value, bool expectedResult)
        {
            VisibilityToBooleanConverter converter = new VisibilityToBooleanConverter();
            bool result = (bool)converter.Convert(value, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestConvertBack()
        {
            VisibilityToBooleanConverter converter = new VisibilityToBooleanConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
