using System.Security;

namespace DebtDiary
{
    /// <summary>
    /// An interface for a class that wants to get Secure Password
    /// </summary>
    public interface IHavePassword
    {
        /// <summary>
        /// The Secure Password
        /// </summary>
        SecureString Password { get; }
    }
}
