using System;

namespace DebtDiary.Core
{
    public static class AvatarColorChanger
    {
        private static AvatarColor[] _colors = (AvatarColor[])Enum.GetValues(typeof(AvatarColor));

        public static AvatarColor Next(AvatarColor currentColor)
        {
            int index = Array.IndexOf(_colors, currentColor);

            if (index + 1 >= _colors.Length)
                return _colors[0];

            return _colors[index + 1];
        }

        public static AvatarColor Previous(AvatarColor currentColor)
        {
            int index = Array.IndexOf(_colors, currentColor);

            if (index - 1 <= 0)
                return _colors[_colors.Length - 1];

            return _colors[index - 1];
        }
    }
}
