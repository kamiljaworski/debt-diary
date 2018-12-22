using NUnit.Framework;
using System.Collections.Generic;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class IEnumerableExtensionsTests
    {
        [Test]
        public void TestForEach()
        {
            IList<int> originalEnumeration = new List<int> { 1, 2, 3, 4 };
            IList<int> expectedEnumeration = new List<int> { 1, 4, 9, 16 };
            IList<int> actualEnumeration = new List<int>();
            originalEnumeration.ForEach(i => actualEnumeration.Add(i * i));

            Assert.AreEqual(expectedEnumeration, actualEnumeration);
        }
    }
}
