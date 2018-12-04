using System;
using System.Linq;

namespace DebtDiary.Core
{
    public static class RandomColorGenerator
    {
        private static Color[] _colors = (Color[])Enum.GetValues(typeof(Color));
        private static Random _random = new Random();

        public static Color GetRandomColor() => (Color)_random.Next(_colors.Length);

        public static Color GetRandomColorExcept(params Color[] colors)
        {
            Color color = GetRandomColor();
            while (colors.Contains(color))
                color = GetRandomColor();

            return color;
        }
    }
}
