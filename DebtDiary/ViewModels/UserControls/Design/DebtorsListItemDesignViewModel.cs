using DebtDiary.Core;

namespace DebtDiary
{
    /// <summary>
    /// The design time data for <see cref="DebtorsListItemViewModel"/>
    /// </summary>
    public class DebtorsListItemDesignViewModel : DebtorsListItemViewModel
    {
        public DebtorsListItemDesignViewModel Instance { get; set; } = new DebtorsListItemDesignViewModel();

        public DebtorsListItemDesignViewModel()
        {
            FullName = "Kamil Jaworski";
            Initials = "KJ";
            Debt = -190.00m;
            AvatarColor = AvatarColor.Green;
        }
    }
}
