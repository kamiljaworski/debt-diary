namespace DebtDiary
{
    public class SideMenuDesignModel : SideMenuViewModel
    {
        public static SideMenuDesignModel Instance => new SideMenuDesignModel();

        public SideMenuDesignModel()
        {
            FullName = "Kamil Jaworski";
            Username = "kamilj610";
            Initials = "KJ";
        }
    }
}
