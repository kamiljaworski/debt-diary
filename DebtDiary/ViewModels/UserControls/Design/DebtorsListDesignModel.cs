using DebtDiary.Core;
using System.Collections.Generic;

namespace DebtDiary
{
    public class DebtorsListDesignModel : DebtorsListViewModel
    {
        public static DebtorsListDesignModel Instance => new DebtorsListDesignModel();

        public DebtorsListDesignModel()
        {
            Debtors = new List<DebtorsListItemViewModel>
            {
                new DebtorsListItemViewModel
                {
                    FullName = "Kamil Jaworski",
                    Initials = "KJ",
                    Debt = -190.00m,
                    AvatarColor = AvatarColor.Green
               },
               new DebtorsListItemViewModel
                {
                    FullName = "Jan Kowalski",
                    Initials = "JK",
                    Debt = 200.00m,
                    AvatarColor = AvatarColor.Orange
               }
            };
        }




    }
}

