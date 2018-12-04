using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace DebtDiary.Core
{
    public static class SecureStringExtensions
    {
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

        public static bool IsNullOrEmpty(this SecureString secureString) => string.IsNullOrEmpty(secureString.GetPassword());

        public static string GetEncryptedPassword(this SecureString secureString)
        {
            if (secureString.IsNullOrEmpty())
                return string.Empty;

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
