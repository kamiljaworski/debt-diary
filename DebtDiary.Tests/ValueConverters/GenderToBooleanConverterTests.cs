using DebtDiary.Core;
using NUnit.Framework;
using System;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class GenderToBooleanConverterTests
    {
        [TestCase(Gender.Male, "Male", true)]
        [TestCase(Gender.Female, "Female", true)]
        [TestCase(Gender.None, "None", false)]
        [TestCase(Gender.Male, "Female", false)]
        [TestCase(Gender.Female, "Male", false)]
        [TestCase(Gender.Female, "asd", false)]
        [TestCase(Gender.Female, "", false)]
        [TestCase(Gender.Female, null, false)]
        [TestCase("Female", Gender.Female, false)]
        [TestCase("", Gender.Female, false)]
        [TestCase(null, Gender.Female, false)]
        [TestCase(null, "", false)]
        [TestCase(null, null, false)]
        [TestCase("", null, false)]
        public void TestConvert(object value, object parameter, bool expectedResult)
        {
            GenderToBooleanConverter converter = new GenderToBooleanConverter();
            bool result = (bool)converter.Convert(value, null, parameter, null);

            Assert.AreEqual(expectedResult, result);
        }

        [TestCase(true, "Male", Gender.Male)]
        [TestCase(true, "Female", Gender.Female)]
        [TestCase(false, "Female", Gender.None)]
        [TestCase(false, "Male", Gender.None)]
        [TestCase(false, "asd", Gender.None)]
        [TestCase(true, "asd", Gender.None)]
        [TestCase(true, "", Gender.None)]
        [TestCase(false, "", Gender.None)]
        [TestCase(false, null, Gender.None)]
        [TestCase(true, null, Gender.None)]
        [TestCase(null, null, Gender.None)]
        [TestCase("", "", Gender.None)]
        [TestCase("asd", "", Gender.None)]
        public void TestConvertBack(object value, object parameter, Gender expectedResult)
        {
            GenderToBooleanConverter converter = new GenderToBooleanConverter();
            Gender result = (Gender)converter.ConvertBack(value, null, parameter, null);

            Assert.AreEqual(expectedResult, result);
        }
    }
}
