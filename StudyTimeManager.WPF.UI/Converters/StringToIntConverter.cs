using System;
using System.Globalization;
using System.Windows.Data;

namespace StudyTimeManager.WPF.UI.Converters
{
    internal class StringToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!int.TryParse(value.ToString(), out int d))
            {
                return Binding.DoNothing;
            }
            return d;
        }
    }
}
