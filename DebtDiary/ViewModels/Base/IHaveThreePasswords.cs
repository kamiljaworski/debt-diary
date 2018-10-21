using System.Security;

namespace DebtDiary
{
    /// <summary>
    /// An interface for a view that have 3 SecurePasswords and you want to get them from this view
    /// </summary>
    public interface IHaveThreePasswords : IHaveTwoPasswords
    {
        SecureString ThirdPassword { get; }
    }
}
