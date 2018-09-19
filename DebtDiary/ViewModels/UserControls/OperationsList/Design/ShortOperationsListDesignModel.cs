using System;
using System.Collections.ObjectModel;

namespace DebtDiary
{
    public class ShortOperationsListDesignModel : ShortOperationsListViewModel
    {
        public static ShortOperationsListDesignModel Instance => new ShortOperationsListDesignModel();

        public ShortOperationsListDesignModel()
        {
            Operations = new ObservableCollection<ShortOperationsListItemViewModel>
            {
                new ShortOperationsListItemViewModel
                {
                    OperationDate = DateTime.Now-TimeSpan.FromDays(30),
                    Description = "test operation",
                    Value = 300m
                },
                new ShortOperationsListItemViewModel
                {
                    OperationDate = DateTime.Now-TimeSpan.FromDays(33),
                    Description = "test operation 2",
                    Value = -100m
                },

                new ShortOperationsListItemViewModel
                {
                    OperationDate = DateTime.Now-TimeSpan.FromDays(40),
                    Description = "test operation 3",
                    Value = 200m
                },
            };
        }
    }
}
