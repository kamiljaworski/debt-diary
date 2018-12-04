using NUnit.Framework;
using System.Collections.Generic;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class RandomColorGeneratorTest
    {
        [Test]
        public void TestGetRandomColor()
        {
            Color firstColor = RandomColorGenerator.GetRandomColor();
            Color secondColor = RandomColorGenerator.GetRandomColor();

            Assert.AreNotEqual(firstColor, secondColor);
        }

        [Test]
        public void TestGetRandomColorExcept()
        {
            Color skippedColor = Color.LightSkyBlue;
            IList<Color> randomColors = new List<Color>();

            for (int i = 0; i < 1000; ++i)
                randomColors.Add(RandomColorGenerator.GetRandomColorExcept(skippedColor));

            Assert.IsFalse(randomColors.Contains(skippedColor));
        }
    }
}
