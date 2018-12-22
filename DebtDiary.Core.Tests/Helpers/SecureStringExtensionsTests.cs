using NUnit.Framework;
using System.Security;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class SecureStringExtensionsTests
    {
        [TestCase("something", false)]
        [TestCase("a", false)]
        [TestCase("", true)]
        [TestCase(null, true)]
        public void TestIsNullOrEmpty(string password, bool expectedValue)
        {
            SecureString secureString = new SecureString();
            if(password != null)
                foreach (char c in password)
                    secureString.AppendChar(c);

            bool actualValue = secureString.IsNullOrEmpty();
            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestCase("test", "9F86D081884C7D659A2FEAA0C55AD015A3BF4F1B2B0B822CD15D6C15B0F00A08")]
        [TestCase("something", "3FC9B689459D738F8C88A3A48AA9E33542016B7A4052E001AAA536FCA74813CB")]
        [TestCase("", "")]
        public void TestGetEncryptedPassword(string password, string hashedPassword)
        {
            SecureString secureString = new SecureString();
            if (password != null)
                foreach (char c in password)
                    secureString.AppendChar(c);

            Assert.AreEqual(hashedPassword, secureString.GetEncryptedPassword());
        }
    }
}
