using System.Security;

namespace DebtDiary
{
    /// <summary>
    /// An interface for a view that have SecurePassword and you want to get it from this view
    /// </summary>
    public interface IHavePassword
    {
        /// <summary>
        /// The Secure Password
        /// </summary>
        SecureString Password { get; }

        /// <summary>
        /// Clears the PasswordBox in the view
        /// </summary>
        void ClearPassword();
    }
}
