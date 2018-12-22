using DebtDiary.Core;
using NUnit.Framework;
using System;
using System.Windows;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class OperationTypeToBooleanConverterTests
    {
        [TestCase(OperationType.DebtorsLoan, "DebtorsLoan", true)]
        [TestCase(OperationType.DebtorsRepayment, "DebtorsRepayment", true)]
        [TestCase(OperationType.UsersLoan, "UsersLoan", true)]
        [TestCase(OperationType.UsersRepayment, "UsersRepayment", true)]
        [TestCase(OperationType.UsersRepayment, "UsersLoan", false)]
        [TestCase(OperationType.UsersRepayment, "", false)]
        [TestCase(OperationType.UsersRepayment, null, false)]
        [TestCase("UsersRepayment", "UsersRepayment", false)]
        [TestCase("", "UsersRepayment", false)]
        [TestCase(null, "UsersRepayment", false)]
        [TestCase(null, "", false)]
        [TestCase(null, null, false)]
        public void TestConvert(object value, object parameter, bool expectedResult)
        {
            OperationTypeToBooleanConverter converter = new OperationTypeToBooleanConverter();
            bool result = (bool)converter.Convert(value, null, parameter, null);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(true, "DebtorsLoan", OperationType.DebtorsLoan)]
        [TestCase(true, "DebtorsRepayment", OperationType.DebtorsRepayment)]
        [TestCase(true, "UsersLoan", OperationType.UsersLoan)]
        [TestCase(true, "UsersRepayment", OperationType.UsersRepayment)]
        [TestCase(false, "UsersRepayment", null)]
        [TestCase(true, "UsersRepaymenta", null)]
        [TestCase(true, "", null)]
        [TestCase(true, "asdsa", null)]
        [TestCase(true, null, null)]
        [TestCase(false, null, null)]
        [TestCase(null, null, null)]
        public void TestConvertBack(object value, object parameter, object expectedResult)
        {
            OperationTypeToBooleanConverter converter = new OperationTypeToBooleanConverter();
            object result = converter.ConvertBack(value, null, parameter, null);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
