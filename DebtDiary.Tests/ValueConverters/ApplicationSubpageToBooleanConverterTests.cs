using NUnit.Framework;
using System;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class ApplicationSubpageToBooleanConverterTests
    {
        [TestCase(ApplicationSubpage.SummarySubpage, "SummarySubpage", true)]
        [TestCase(ApplicationSubpage.DebtorInfoSubpage, "DebtorInfoSubpage", true)]
        [TestCase(ApplicationSubpage.SummarySubpage, "SummaarySubpage", false)]
        [TestCase(null, "DebtorInfoSubpage", false)]
        [TestCase(ApplicationSubpage.SummarySubpage, null, false)]
        [TestCase(ApplicationSubpage.SummarySubpage, ApplicationSubpage.SummarySubpage, false)]
        public void TestConvert(object value, object parameter, bool expectedResult)
        {
            ApplicationSubpageToBooleanConverter converter = new ApplicationSubpageToBooleanConverter();
            bool result = (bool)converter.Convert(value, null, parameter, null);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void TestConvertBack()
        {
            ApplicationSubpageToBooleanConverter converter = new ApplicationSubpageToBooleanConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
