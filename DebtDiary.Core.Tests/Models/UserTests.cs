using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void TestOperationsWithNoOperations()
        {
            User user = new User();
            List<Operation> operations = new List<Operation>();

            Assert.AreEqual(operations, user.Operations);
        }

        [Test]
        public void TestOperationsWithSomeOperations()
        {
            // Prepare first debtors operations list
            Operation operation1 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(10), Value = 100.0m };
            Operation operation2 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(8), Value = 200.0m };
            Operation operation3 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(7), Value = -50.0m };
            Operation operation4 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(5), Value = -20.0m };
            Operation operation5 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(3), Value = 300.0m };

            // Add these operations to debtor1
            Debtor debtor1 = new Debtor();
            debtor1.Operations.Add(operation1);
            debtor1.Operations.Add(operation2);
            debtor1.Operations.Add(operation3);
            debtor1.Operations.Add(operation4);
            debtor1.Operations.Add(operation5);

            // Prepare second debtors operations list
            Operation operation6 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(9), Value = 5000.0m };
            Operation operation7 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(6), Value = 20.0m };
            Operation operation8 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(4), Value = 10.0m };
            Operation operation9 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(2), Value = -200.0m };
            Operation operation10 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(1), Value = 100.0m };

            // Add these operations to debtor2
            Debtor debtor2 = new Debtor();
            debtor2.Operations.Add(operation6);
            debtor2.Operations.Add(operation7);
            debtor2.Operations.Add(operation8);
            debtor2.Operations.Add(operation9);
            debtor2.Operations.Add(operation10);

            // Add these debtors to user Debtors list
            User user = new User();
            user.Debtors.Add(debtor1);
            user.Debtors.Add(debtor2);

            // Prepare expected operations list (ordered by AdditionDate descending)
            List<Operation> expectedOperations = new List<Operation>
            {
                operation10, operation9, operation5, operation8, operation4, operation7, operation3, operation2, operation6, operation1
            };

            Assert.AreEqual(expectedOperations, user.Operations);
        }

        [Test]
        public void TestGetChartPointsWithNoOperations()
        {
            User user = new User { RegisterDate = DateTime.Today };
            IEnumerable<decimal> expectedEnumeration = new List<decimal> { 0.0m, 0.0m };

            Assert.AreEqual(expectedEnumeration, user.GetChartPoints());
        }

        [Test]
        public void TestGetChartPointsWithSomeOperations()
        {
            // Prepare first debtors operations list
            Operation operation1 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(10), Value = 100.0m };
            Operation operation2 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(8), Value = 200.0m };
            Operation operation3 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(7), Value = -50.0m };
            Operation operation4 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(5), Value = -20.0m };
            Operation operation5 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(3), Value = 300.0m };

            // Add these operations to debtor1
            Debtor debtor1 = new Debtor();
            debtor1.Operations.Add(operation1);
            debtor1.Operations.Add(operation2);
            debtor1.Operations.Add(operation3);
            debtor1.Operations.Add(operation4);
            debtor1.Operations.Add(operation5);

            // Prepare second debtors operations list
            Operation operation6 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(9), Value = 5000.0m };
            Operation operation7 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(6), Value = 20.0m };
            Operation operation8 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(4), Value = 10.0m };
            Operation operation9 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(2), Value = -200.0m };
            Operation operation10 = new Operation { AdditionDate = DateTime.Today - TimeSpan.FromDays(1), Value = 100.0m };

            // Add these operations to debtor2
            Debtor debtor2 = new Debtor();
            debtor2.Operations.Add(operation6);
            debtor2.Operations.Add(operation7);
            debtor2.Operations.Add(operation8);
            debtor2.Operations.Add(operation9);
            debtor2.Operations.Add(operation10);

            // Add these debtors to user Debtors list
            User user = new User { RegisterDate = DateTime.Today - TimeSpan.FromDays(10) };
            user.Debtors.Add(debtor1);
            user.Debtors.Add(debtor2);

            // Prepare expected list
            IEnumerable<decimal> expectedEnumeration = new List<decimal> { 0.0m, 100.0m, 5100.0m, 5300.0m, 5250.0m, 5270.0m, 5250.0m, 5260.0m, 5560.0m, 5360.0m, 5460.0m, 5460.0m };

            Assert.AreEqual(expectedEnumeration, user.GetChartPoints());
        }

        [TestCase("OLD_PASSWORD", "NEW_PASSWORD", "NEW_PASSWORD")]
        [TestCase("OLD_PASSWORD", "", "OLD_PASSWORD")]
        [TestCase("OLD_PASSWORD", null, "OLD_PASSWORD")]
        public void TestChangePassword(string oldPassword, string newPassword, string expectedPassword)
        {
            User user = new User { Password = oldPassword };
            user.ChangePassword(newPassword);

            Assert.AreEqual(expectedPassword, user.Password);
        }
    }
}
