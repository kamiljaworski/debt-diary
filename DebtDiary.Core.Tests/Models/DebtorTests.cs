using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class DebtorTests
    {
        [Test]
        public void TestDebtWithNoOperations()
        {
            Debtor debtor = new Debtor();

            Assert.AreEqual(0.0m, debtor.Debt);
        }

        [Test]
        public void TestDebtWithSomeOperations()
        {
            Debtor debtor = new Debtor();
            debtor.Operations.Add(new Operation { Value = 100 });
            debtor.Operations.Add(new Operation { Value = 50 });
            debtor.Operations.Add(new Operation { Value = -20 });
            debtor.Operations.Add(new Operation { Value = 300 });

            Assert.AreEqual(430.0m, debtor.Debt);
        }

        [Test]
        public void TestGetChartPointsWithNoOperations()
        {
            Debtor debtor = new Debtor();
            User user = new User { RegisterDate = DateTime.Today };
            debtor.User = user;
            IEnumerable<decimal> expectedEnumeration = new List<decimal> { 0.0m, 0.0m };

            Assert.AreEqual(expectedEnumeration, debtor.GetChartPoints());
        }
    }
}
