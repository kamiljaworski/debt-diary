using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace DebtDiary.Core
{
    /// <summary>
    /// Extension class for SecureString
    /// </summary>
    public static class SecureStringExtensions
    {
        /// <summary>
        /// Returns a password from SecureString
        /// </summary>
        /// <param name="secureString">SecureString you want to get password from</param>
        /// <returns>Password</returns>
        private static string GetPassword(this SecureString secureString)
        {
            IntPtr value = IntPtr.Zero;
            try
            {
                value = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(value);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(value);
            }
        }

        /// <summary>
        /// Method that check if secure string has a value and is not null
        /// </summary>
        /// <param name="secureString">Secure string you want to check</param>
        public static bool IsNullOrEmpty(this SecureString secureString) => string.IsNullOrEmpty(secureString.GetPassword());

        /// <summary>
        /// Returns a password from SecureString encrypted with SHA265 algorithm
        /// </summary>
        /// <param name="secureString">SecureString you want to get password from</param>
        /// <returns>Encrypted password</returns>
        public static string GetEncryptedPassword(this SecureString secureString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(secureString.GetPassword());
            byte[] hash = sha256.ComputeHash(bytes);

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
                result.Append(hash[i].ToString("X2"));

            return result.ToString();
        }
    }
}
