using NUnit.Framework;
using System;
using System.Windows.Media;

namespace DebtDiary.Tests.ValueConverters
{
    [TestFixture]
    public class DialogMessageToBrushConverterTests
    {
        [TestCase(DialogMessage.None, "#FFFF0000")]
        [TestCase(DialogMessage.NoInternetConnection, "#FFFF0000")]
        [TestCase(DialogMessage.AccountCreated, "#FF008000")]
        [TestCase(DialogMessage.DebtorDeleted, "#FF008000")]
        [TestCase(DialogMessage.ProfileUpdated, "#FF008000")]
        [TestCase(null, "#FF008000")]
        [TestCase("", "#FF008000")]
        [TestCase("string", "#FF008000")]
        public void TestConvert(object value, string hexColor)
        {
            DialogMessageToBrushConverter converter = new DialogMessageToBrushConverter();
            SolidColorBrush result = (SolidColorBrush)converter.Convert(value, null, null, null);

            Assert.AreEqual(hexColor, result.ToString());
        }

        [Test]
        public void TestConvertBack()
        {
            DialogMessageToBrushConverter converter = new DialogMessageToBrushConverter();

            Assert.Throws<NotImplementedException>(() => converter.ConvertBack(null, null, null, null));
        }
    }
}
