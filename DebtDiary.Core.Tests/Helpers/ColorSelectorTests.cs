using NUnit.Framework;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class ColorSelectorTests
    {
        [TestCase(Color.Green, Color.Orange)]
        [TestCase(Color.DarkOrange, Color.DarkOliveGreen)]
        [TestCase(Color.YellowGreen, Color.Green)]
        public void TestNextColorWithValidData(Color color, Color expectedNextColor)
        {
            Color actualNextColor = ColorSelector.Next(color);
            Assert.AreEqual(expectedNextColor, actualNextColor);
        }

        [TestCase(Color.Green, Color.YellowGreen)]
        [TestCase(Color.DarkOrange, Color.DarkOrchid)]
        [TestCase(Color.YellowGreen, Color.Goldenrod)]
        public void TestPreviousColorWithValidData(Color color, Color expectedPreviousColor)
        {
            Color actualPreviousColor = ColorSelector.Previous(color);
            Assert.AreEqual(expectedPreviousColor, actualPreviousColor);
        }
    }
}
