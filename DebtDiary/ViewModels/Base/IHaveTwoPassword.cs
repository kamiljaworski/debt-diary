using System.Security;

namespace DebtDiary
{
    /// <summary>
    /// An interface for a view that have 2 SecurePasswords and you want to get them from this view
    /// </summary>
    public interface IHaveTwoPassword
    {
        /// <summary>
        /// First Secure Password
        /// </summary>
        SecureString FirstPassword { get; }

        /// <summary>
        /// Second Secure Password
        /// </summary>
        SecureString SecondPassword { get; }
    }
}
