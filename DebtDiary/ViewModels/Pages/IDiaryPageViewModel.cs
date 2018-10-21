using DebtDiary.Core;
using System.Windows.Input;

namespace DebtDiary
{
    public interface IDiaryPageViewModel
    {
        string FullName { get; set; }
        string Username { get; set; }
        string Initials { get; set; }

        ICommand SummaryCommand { get; set; }
        ICommand MyAccountCommand { get; set; }
        ICommand LogoutCommand { get; set; }
        ICommand AddDebtorCommand { get; set; }
        ICommand SortCommand { get; set; }

        bool IsSummarySelected { get; set; }
        bool IsMyAccountSelected { get; set; }
        bool IsLogoutSelected { get; set; }
        SortType SortType { get; set; }

        IDebtorsListViewModel DebtorsList { get; }

        /// <summary>
        /// Reset all the selected buttons properties to false
        /// </summary>
        void ResetSelectedButtons();

        /// <summary>
        /// Reset users fullname, username and initials
        /// </summary>
        void UpdateUsersData();
    }
}
