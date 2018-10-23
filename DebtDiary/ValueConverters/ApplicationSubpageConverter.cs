using System;
using System.Globalization;

namespace DebtDiary
{
    /// <summary>
    /// Converts <see cref="ApplicationSubpage"/> to an actual Subpage
    /// </summary>
    public class ApplicationSubpageConverter : BaseValueConverter<ApplicationSubpageConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationSubpage)value)
            {
                case ApplicationSubpage.SummarySubpage:
                    return new SummarySubpage();

                case ApplicationSubpage.AddDebtorSubpage:
                    return new AddDebtorSubpage();

                case ApplicationSubpage.DebtorInfoSubpage:
                    return new DebtorInfoSubpage();

                case ApplicationSubpage.MyAccountSubpage:
                    return new MyAccountSubpage();

                case ApplicationSubpage.EditDebtorSubpage:
                    return new EditDebtorSubpage();

                case ApplicationSubpage.DeleteDebtorSubpage:
                    return new DeleteDebtorSubpage();

                default:
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
