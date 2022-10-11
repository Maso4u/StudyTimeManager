using System;
using System.Globalization;
using System.Windows.Data;

namespace StudyTimeManager.WPF.UI.Converters
{
    internal class DateConverter : IValueConverter
    {
        private const string STRING_FORMAT = "dd/MM/yyyy";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return value;
            }

            DateTime date = (DateTime)value;
            return date.ToString(STRING_FORMAT);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.ParseExact((string)value, STRING_FORMAT, culture);
        }
    }
}
