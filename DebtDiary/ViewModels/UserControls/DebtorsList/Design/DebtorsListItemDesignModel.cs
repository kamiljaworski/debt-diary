using DebtDiary.Core;

namespace DebtDiary
{
    /// <summary>
    /// The design time data for <see cref="DebtorsListItemViewModel"/>
    /// </summary>
    public class DebtorsListItemDesignModel : DebtorsListItemViewModel
    {
        public static DebtorsListItemDesignModel Instance => new DebtorsListItemDesignModel();

        public DebtorsListItemDesignModel()
        {
            FullName = "Kamil Jaworski";
            Initials = "KJ";
            Debt = -190.00m;
            AvatarColor = Color.Green;
        }
    }
}
