using System;

namespace DebtDiary.Core
{
    public static class ColorSelector
    {
        private static Color[] _colors = (Color[])Enum.GetValues(typeof(Color));

        public static Color Next(Color currentColor)
        {
            int index = Array.IndexOf(_colors, currentColor);

            if (index + 1 >= _colors.Length)
                return _colors[0];

            return _colors[index + 1];
        }

        public static Color Previous(Color currentColor)
        {
            int index = Array.IndexOf(_colors, currentColor);

            if (index - 1 < 0)
                return _colors[_colors.Length - 1];

            return _colors[index - 1];
        }
    }
}
