using System;
using System.Windows;
using System.Windows.Data;

namespace BcWpfCommon.Converters
{
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class StringToVisibleConverter : IValueConverter
    {
        public object Convert(
            object value,
            Type targetType,
            object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
            {
                throw new InvalidOperationException("The target must be System.Windows.Visibility");
            }
            if (string.IsNullOrEmpty((string)value))
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
