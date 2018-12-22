using NUnit.Framework;
using System;
using System.Windows;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class HorizontalAlignmentToTextAlignmentConverterTests
    {
        [TestCase(HorizontalAlignment.Center, TextAlignment.Center)]
        [TestCase(HorizontalAlignment.Left, TextAlignment.Left)]
        [TestCase(HorizontalAlignment.Right, TextAlignment.Right)]
        [TestCase(HorizontalAlignment.Stretch, TextAlignment.Justify)]
        [TestCase(null, TextAlignment.Center)]
        [TestCase("", TextAlignment.Center)]
        [TestCase("asdasd", TextAlignment.Center)]
        public void TestConvert(object value, TextAlignment expectedResult)
        {
            HorizontalAlignmentToTextAlignmentConverter converter = new HorizontalAlignmentToTextAlignmentConverter();
            TextAlignment result = (TextAlignment)converter.Convert(value, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestConvertBack()
        {
            HorizontalAlignmentToTextAlignmentConverter converter = new HorizontalAlignmentToTextAlignmentConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
