using DebtDiary.Core;
using System.Collections.ObjectModel;

namespace DebtDiary
{
    public class DebtorsListDesignModel : DebtorsListViewModel
    {
        public static DebtorsListDesignModel Instance => new DebtorsListDesignModel();

        public DebtorsListDesignModel() : base()
        {
            Debtors = new ObservableCollection<DebtorsListItemViewModel>
            {
                new DebtorsListItemViewModel
                {
                    FullName = "Kamil Jaworski",
                    Initials = "KJ",
                    Debt = -190.00m,
                    AvatarColor = Color.Green
               },
               new DebtorsListItemViewModel
               {
                    FullName = "Jan Kowalski",
                    Initials = "JK",
                    Debt = 200.00m,
                    AvatarColor = Color.Orange
               },
               new DebtorsListItemViewModel
               {
                    FullName = "Adam Nowak",
                    Initials = "AN",
                    Debt = 9999.00m,
                    AvatarColor = Color.LightSeaGreen
               },
               new DebtorsListItemViewModel
               {
                    FullName = "Marek Maślana",
                    Initials = "MM",
                    Debt = 0.00m,
                    AvatarColor = Color.Green
               },
               new DebtorsListItemViewModel
               {
                    FullName = "Jacek Dzięcioł",
                    Initials = "JD",
                    Debt = 999.00m,
                    AvatarColor = Color.LightSeaGreen
               },
               new DebtorsListItemViewModel
               {
                    FullName = "Kamil Jaworski",
                    Initials = "KJ",
                    Debt = -190.00m,
                    AvatarColor = Color.Green
               },
               new DebtorsListItemViewModel
               {
                    FullName = "Jan Kowalski",
                    Initials = "JK",
                    Debt = 200.00m,
                    AvatarColor = Color.Orange
               },
               new DebtorsListItemViewModel
               {
                    FullName = "Adam Nowak",
                    Initials = "AN",
                    Debt = 9999.00m,
                    AvatarColor = Color.LightSeaGreen
               },
               new DebtorsListItemViewModel
               {
                    FullName = "Marek Maślana",
                    Initials = "MM",
                    Debt = 0.00m,
                    AvatarColor = Color.Green
               },
               new DebtorsListItemViewModel
               {
                    FullName = "Jacek Dzięcioł",
                    Initials = "JD",
                    Debt = 999.00m,
                    AvatarColor = Color.LightSeaGreen
               },               new DebtorsListItemViewModel
               {
                    FullName = "Adam Nowak",
                    Initials = "AN",
                    Debt = 9999.00m,
                    AvatarColor = Color.LightSeaGreen
               },
               new DebtorsListItemViewModel
               {
                    FullName = "Marek Maślana",
                    Initials = "MM",
                    Debt = 0.00m,
                    AvatarColor = Color.Green
               },
               new DebtorsListItemViewModel
               {
                    FullName = "Jacek Dzięcioł",
                    Initials = "JD",
                    Debt = 999.00m,
                    AvatarColor = Color.LightSeaGreen
               }
            };
        }




    }
}

