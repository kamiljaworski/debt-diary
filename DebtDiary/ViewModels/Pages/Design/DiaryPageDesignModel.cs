namespace DebtDiary
{
    public class DiaryPageDesignModel : DiaryPageViewModel
    {
        public static DiaryPageDesignModel Instance = new DiaryPageDesignModel();


        public DiaryPageDesignModel() : base()
        {
            FullName = "Kamil Jaworski";
            Username = "kamilj610";
            Initials = "KJ";
        }
    }
}
