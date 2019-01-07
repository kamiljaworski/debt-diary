using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class ChartHelpersTests
    {
        [Test]
        public void TestGetChartPointsWithNoOperationsAndTodayDate()
        {
            IList<Operation> operations = new List<Operation>();
            DateTime today = DateTime.Today.Date;
            IEnumerable<decimal> expectedEnumeration = new List<decimal> { 0.0m, 0.0m };
            IEnumerable<decimal> actualEnumeration = operations.GetChartPoints(today);

            Assert.AreEqual(expectedEnumeration, actualEnumeration);
        }


        [Test]
        public void TestGetChartPointsWithSomeOperationsAndOperationOnLastDay()
        {
            DateTime startDate = DateTime.Today - TimeSpan.FromDays(7);
            IList<Operation> operations = new List<Operation>
            {
                new Operation { AdditionDate = startDate, Value = 100.0m },
                new Operation { AdditionDate = startDate, Value = 200.0m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(1), Value = -50.0m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(2), Value = -100.0m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(2), Value = -500.50m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(5), Value = 600.0m },
                new Operation { AdditionDate = DateTime.Today, Value = 100.0m }
            };

            IEnumerable<decimal> expectedEnumeration = new List<decimal> { 0.0m, 300.0m, 250.0m, -350.50m, -350.50m, -350.50m, 249.50m, 249.50m, 349.50m };
            IEnumerable<decimal> actualEnumeration = operations.GetChartPoints(startDate);

            Assert.AreEqual(expectedEnumeration, actualEnumeration);
        }

        [Test]
        public void TestGetChartPointsWithSomeOperationsAndLastOperationAddedThreeDaysAgo()
        {
            DateTime startDate = DateTime.Today - TimeSpan.FromDays(10);
            IList<Operation> operations = new List<Operation>
            {
                new Operation { AdditionDate = startDate, Value = 100.0m },
                new Operation { AdditionDate = startDate, Value = 200.0m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(1), Value = -50.0m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(2), Value = -100.0m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(2), Value = -500.50m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(5), Value = 600.0m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(7), Value = 100.0m }
            };

            IEnumerable<decimal> expectedEnumeration = new List<decimal> { 0.0m, 300.0m, 250.0m, -350.50m, -350.50m, -350.50m, 249.50m, 249.50m, 349.50m, 349.50m, 349.50m, 349.50m };
            IEnumerable<decimal> actualEnumeration = operations.GetChartPoints(startDate);

            Assert.AreEqual(expectedEnumeration, actualEnumeration);
        }

        [Test]
        public void TestGetChartPointsWithFirstOperationOlderThanStartDate()
        {
            DateTime startDate = DateTime.Today - TimeSpan.FromDays(10);
            IList<Operation> operations = new List<Operation>
            {
                new Operation { AdditionDate = startDate - TimeSpan.FromDays(2), Value = 100.0m },
                new Operation { AdditionDate = startDate, Value = 200.0m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(1), Value = -50.0m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(2), Value = -100.0m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(2), Value = -500.50m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(5), Value = 600.0m },
                new Operation { AdditionDate = startDate + TimeSpan.FromDays(7), Value = 100.0m }
            };

            IEnumerable<decimal> expectedEnumeration = new List<decimal> { 0.0m, 100.0m, 100.0m, 300.0m, 250.0m, -350.50m, -350.50m, -350.50m, 249.50m, 249.50m, 349.50m, 349.50m, 349.50m, 349.50m };
            IEnumerable<decimal> actualEnumeration = operations.GetChartPoints(startDate);

            Assert.AreEqual(expectedEnumeration, actualEnumeration);
        }
    }
}
