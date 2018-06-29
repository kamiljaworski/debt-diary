using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace DebtDiary
{
    /// <summary>
    /// A base value converter class that allows XAML usage
    /// </summary>
    /// <typeparam name="T">Type of this value converter</typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter where T: class, new()
    {
        #region Private Members

        /// <summary>
        /// Private static instance of the value converter 
        /// </summary>
        private static T _converter = null;
        #endregion

        #region Markup Extension Methods

        /// <summary>
        /// Provides a static instance of the value converter 
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider) => _converter ?? (_converter = new T());
        #endregion

        #region Value Converter Methods

        /// <summary>
        /// A method used to convert one type to another
        /// </summary>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// A method used to convert value back to its source type
        /// </summary>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
        #endregion
    }
}