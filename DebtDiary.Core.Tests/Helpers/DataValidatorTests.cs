using NUnit.Framework;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class DataValidatorTests
    {
        [TestCase("test@gmail.com", true)]
        [TestCase("test123@gmail.com", true)]
        [TestCase("john.fox541@gmail.com", true)]
        [TestCase("john_fox541@gmail.com", true)]
        [TestCase("test__@buziaczek.pl", true)]
        [TestCase("test", false)]
        [TestCase("test@a.a", false)]
        [TestCase("test@gmail", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        public void TestIsEmailCorrect(string email, bool expectedValue)
        {
            bool actualValue = DataValidator.IsEmailCorrect(email);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("test", true)]
        [TestCase("test123", true)]
        [TestCase("test_123", true)]
        [TestCase("TE$T", false)]
        [TestCase("$%^&*(", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        public void TestIsUsernameCorrect(string username, bool expectedValue)
        {
            bool actualValue = DataValidator.IsUsernameCorrect(username);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("John", true)]
        [TestCase("john", true)]
        [TestCase("John Paul", true)]
        [TestCase("John P.", true)]
        [TestCase("John paul", true)]
        [TestCase("Bachleda-Curuś", true)]
        [TestCase("O'Connor", true)]
        [TestCase("john123", false)]
        [TestCase("$%^&*(", false)]
        [TestCase("123", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        public void TestIsNameCorrect(string name, bool expectedValue)
        {
            bool actualValue = DataValidator.IsNameCorrect(name);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("10000", true)]
        [TestCase("10000.11", true)]
        [TestCase("-999.99", true)]
        [TestCase("10000,11", false)]
        [TestCase("10 000,11", false)]
        [TestCase("10 000.11", false)]
        [TestCase("10 000.11111", false)]
        [TestCase("10000.11111", false)]
        [TestCase("asdasd", false)]
        [TestCase("%#@#%", false)]
        [TestCase("asd1", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        public void TestIsDecimalNumberCorrect(string number, bool expectedValue)
        {
            bool actualValue = DataValidator.IsDecimalNumberCorrect(number);
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("lend", true)]
        [TestCase("lend 10zł", true)]
        [TestCase("lend 10", true)]
        [TestCase("lend 10,50 $", true)]
        [TestCase("lend 10.50 $", true)]
        [TestCase("10$ loan - for sth", true)]
        [TestCase("asdasd", true)]
        [TestCase("%#@#%", false)]
        [TestCase(null, false)]
        [TestCase("", false)]
        public void TestIsOperationDescriptionCorrect(string description, bool expectedValue)
        {
            bool actualValue = DataValidator.IsOperationDescriptionCorrect(description);
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
