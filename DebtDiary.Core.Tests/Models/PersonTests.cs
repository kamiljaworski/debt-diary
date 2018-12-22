using NUnit.Framework;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class PersonTests
    {
        [TestCase("Test", "Test", "TT")]
        [TestCase("Test", "Test", "TT")]
        [TestCase("", "", "  ")]
        [TestCase(null, null, "  ")]
        public void TestInitials(string firstName, string lastName, string expectedValue)
        {
            Person person = new Debtor { FirstName = firstName, LastName = lastName };

            Assert.AreEqual(expectedValue, person.Initials);
        }

        [TestCase("Test", "Test", "Test Test")]
        [TestCase("", "", " ")]
        [TestCase(null, null, " ")]
        public void TestFullName(string firstName, string lastName, string expectedValue)
        {
            Person person = new Debtor { FirstName = firstName, LastName = lastName };

            Assert.AreEqual(expectedValue, person.FullName);
        }

        [Test]
        public void TestEditPerson()
        {
            Person person = new Debtor();
            string firstName = "Test";
            string lastName = "Test";
            Gender gender = Gender.Female;
            Color avatarColor = Color.MediumAquamarine;
            person.EditPerson(firstName, lastName, gender, avatarColor);

            Assert.IsTrue(person.FirstName == firstName && person.LastName == lastName && person.Gender == gender && person.AvatarColor == avatarColor);
        }
    }
}
