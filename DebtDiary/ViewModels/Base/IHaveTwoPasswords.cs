using System.Security;

namespace DebtDiary
{
    /// <summary>
    /// An interface for a view that have 2 SecurePasswords and you want to get them from this view
    /// </summary>
    public interface IHaveTwoPasswords : IHavePassword
    {
        /// <summary>
        /// Second Secure Password
        /// </summary>
        SecureString SecondPassword { get; }
    }
}
