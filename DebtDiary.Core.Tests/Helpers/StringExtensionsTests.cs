using NUnit.Framework;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("test", "test")]
        [TestCase("TEST", "tEST")]
        [TestCase("123", "123")]
        [TestCase("ŁŁŁ", "łŁŁ")]
        public void TestToLowerFirstLetterWithValidString(string text, string expectedValue)
        {
            string actualValue = text.ToLowerFirstLetter();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        public void TestToLowerFirstLetterWithInvalidString(string text, string expectedValue)
        {
            string actualValue = text.ToLowerFirstLetter();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("test", "Test")]
        [TestCase("TEST", "TEST")]
        [TestCase("123", "123")]
        [TestCase("łłł", "Łłł")]
        public void TestToUpperFirstLetterWithValidString(string text, string expectedValue)
        {
            string actualValue = text.ToUpperFirstLetter();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase(null, null)]
        [TestCase("", "")]
        public void TestToUpperFirstLetterWithInvalidString(string text, string expectedValue)
        {
            string actualValue = text.ToUpperFirstLetter();
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
