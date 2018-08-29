using System;

namespace DebtDiary.Core
{
    public static class AvatarColorChanger
    {
        private static AvatarColor[] _colors = (AvatarColor[])Enum.GetValues(typeof(AvatarColor));

        /// <summary>
        /// Get next avatar color
        /// </summary>
        /// <param name="currentColor">Current <see cref="AvatarColor"/></param>
        /// <returns>Next <see cref="AvatarColor"/></returns>
        public static AvatarColor Next(AvatarColor currentColor)
        {
            int index = Array.IndexOf(_colors, currentColor);

            if (index + 1 >= _colors.Length)
                return _colors[0];

            return _colors[index + 1];
        }

        /// <summary>
        /// Get previous avatar color
        /// </summary>
        /// <param name="currentColor">Current <see cref="AvatarColor"/></param>
        /// <returns>Previous <see cref="AvatarColor"/></returns>
        public static AvatarColor Previous(AvatarColor currentColor)
        {
            int index = Array.IndexOf(_colors, currentColor);

            if (index - 1 < 0)
                return _colors[_colors.Length - 1];

            return _colors[index - 1];
        }
    }
}
