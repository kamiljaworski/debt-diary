using NUnit.Framework;
using System;
using System.Windows;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class TimeSpanToDurationConverterTests
    {
        [Test]
        public void TestConvertWithValidTimeSpan()
        {
            var minutes = 10.0;
            var timeSpan = TimeSpan.FromMinutes(minutes);
            TimeSpanToDurationConverter converter = new TimeSpanToDurationConverter();
            var duration = (Duration)converter.Convert(timeSpan, null, null, null);

            Assert.AreEqual(minutes, duration.TimeSpan.Minutes);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("asdasd")]
        public void TestConvertWithInvalidObject(object value)
        {
            TimeSpanToDurationConverter converter = new TimeSpanToDurationConverter();
            var duration = (Duration)converter.Convert(value, null, null, null);

            Assert.AreEqual(0.0, duration.TimeSpan.Minutes);
        }

        [Test]
        public void TestConvertBack()
        {
            TimeSpanToDurationConverter converter = new TimeSpanToDurationConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
