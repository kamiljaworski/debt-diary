using DebtDiary.Core;
using System.Windows.Input;

namespace DebtDiary
{
    public interface IDiaryPageViewModel
    {
        string FullName { get; set; }
        string Username { get; set; }
        string Initials { get; set; }
        Color AvatarColor { get; set; }

        ICommand SummaryCommand { get; set; }
        ICommand MyAccountCommand { get; set; }
        ICommand LogoutCommand { get; set; }
        ICommand AddDebtorCommand { get; set; }
        ICommand SortCommand { get; set; }

        SortType SortType { get; set; }

        IDebtorsListViewModel DebtorsList { get; }

        /// <summary>
        /// Update user's fullname, username and initials
        /// </summary>
        void UpdateUsersData();

        /// <summary>
        /// Update debtors list 
        /// </summary>
        void UpdateDebtorsList();
    }
}
