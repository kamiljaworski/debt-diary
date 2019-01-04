using System;
using System.Collections.Generic;
using System.Linq;

namespace DebtDiary.Core
{
    public static class ChartHelpers
    {
        /// <summary>
        /// Get chart points of aggregated operations grouped by every day started startDate
        /// </summary>
        public static IEnumerable<decimal> GetChartPoints(this IList<Operation> operations, DateTime defaultStartDate)
        {
            DateTime startDate = defaultStartDate;

            // Check if operations list have date earlier then starting date
            if(operations != null && operations.Count > 0)
            {
                DateTime firstOperationDate = operations.OrderBy(x => x.AdditionDate).FirstOrDefault().AdditionDate;
                if (firstOperationDate != null && firstOperationDate < startDate)
                    startDate = firstOperationDate;
            }

            // Make a list of all the points
            IList<KeyValuePair<DateTime, decimal>> points = new List<KeyValuePair<DateTime, decimal>>();

            // Add default point one day before addition date with value 0.0m
            points.Add(new KeyValuePair<DateTime, decimal>(startDate.Date - TimeSpan.FromDays(1), 0.0m));

            // Make a list of all debts grouped by date
            IList<IGrouping<DateTime, Operation>> debts = operations.GroupBy(x => x.AdditionDate.Date).OrderBy(x => x.Key).ToList();

            // Find debts added in debtor addition date
            IGrouping<DateTime, Operation> firstDay = debts.FirstOrDefault(x => x.Key == startDate.Date);

            // If there isnt't any debt, add first point with 0 value
            if (firstDay == null)
                points.Add(new KeyValuePair<DateTime, decimal>(startDate.Date, 0.0m));
            else
            {
                // If there are some debts, add first point with summed value of these debts
                points.Add(new KeyValuePair<DateTime, decimal>(firstDay.Key, firstDay.Aggregate(0.0m, (a, b) => a + b.Value)));
                debts.Remove(debts.First());
            }

            // Add all debts to the list
            while (debts.Count() > 0 && (points.Last().Key < debts?.First().Key))
            {
                DateTime nextDay = points.Last().Key + TimeSpan.FromDays(1);

                // If there isn't any debt added in this day, add new point with last points value
                if (nextDay < debts.First().Key)
                    points.Add(new KeyValuePair<DateTime, decimal>(nextDay, points.Last().Value));
                else
                {
                    // If there are some debts, add new point with summed last points value with summed value of these debts
                    points.Add(new KeyValuePair<DateTime, decimal>(debts.First().Key, debts.First().Aggregate(points.Last().Value, (a, b) => a + b.Value)));
                    debts.Remove(debts.First());
                }
            }

            // Add rest of days to the list with last points value
            while (points.Last().Key < DateTime.Now.Date)
            {
                DateTime nextDay = points.Last().Key + TimeSpan.FromDays(1);
                points.Add(new KeyValuePair<DateTime, decimal>(nextDay, points.Last().Value));
            }

            // Return only decimal values
            return points.Select(x => x.Value);
        }
    }
}
