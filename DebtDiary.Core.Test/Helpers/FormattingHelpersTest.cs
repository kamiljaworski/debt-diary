using NUnit.Framework;

namespace DebtDiary.Core.Test
{
    [TestFixture]
    public class FormattingHelpersTest
    {
        [TestCase("Jan", "Kowalski", "JK")]
        [TestCase("Morgan", "Freeman", "MF")]
        [TestCase("Łukasz", "Stanisławowski", "ŁS")]
        public void TestGetInitialsWithValidData(string firstName, string lastName, string expectedValue)
        {
            string actualValue = FormattingHelpers.GetInitials(firstName, lastName);
            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
