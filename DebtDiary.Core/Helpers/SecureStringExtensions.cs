using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DebtDiary.Core
{
    public static class SecureStringExtensions
    {
        public static string GetPassword(this SecureString secureString)
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
    }
}
