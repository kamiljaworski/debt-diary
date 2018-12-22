using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DebtDiary.Core.Tests
{
    [TestFixture]
    public class RandomColorGeneratorTests
    {
        [Test]
        public void TestGetRandomColor()
        {
            IList<Color> randomColors = new List<Color>();

            for (int i = 0; i < 1000; ++i)
                randomColors.Add(RandomColorGenerator.GetRandomColor());

            Assert.That(randomColors.Select(x => x != randomColors[0]).Count() > 0);
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
