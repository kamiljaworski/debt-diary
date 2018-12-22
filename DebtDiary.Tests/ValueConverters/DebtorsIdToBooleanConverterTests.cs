using NUnit.Framework;
using System;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class DebtorsIdToBooleanConverterTests
    {
        [TestCase(1, 1, true)]
        [TestCase(2, 2, true)]
        [TestCase(2, 1, false)]
        [TestCase(1, 2, false)]
        [TestCase("aa", 2, false)]
        [TestCase(null, 2, false)]
        [TestCase(2, null, false)]
        [TestCase(2, "aa", false)]
        public void TestConvertWithTwoElementsArray(object ob1, object ob2, bool expectedResult)
        {
            DebtorsIdToBooleanConverter converter = new DebtorsIdToBooleanConverter();
            bool result = (bool)converter.Convert(new object[] { ob1, ob2 }, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(1, false)]
        [TestCase("a", false)]
        [TestCase(null, false)]
        public void TestConvertWithOneElement(object ob, bool expectedResult)
        {
            DebtorsIdToBooleanConverter converter = new DebtorsIdToBooleanConverter();
            bool result = (bool)converter.Convert(new object[] { ob }, null, null, null);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestConvertBack()
        {
            DebtorsIdToBooleanConverter converter = new DebtorsIdToBooleanConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
