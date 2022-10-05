using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BcWpfCommon.Converters
{
    public class MultiplyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double result = 1.0;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is double value)
                {
                    result *= value;
                }
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            MessageBox.Show("Problem converting back in converter of this object");
            return targetTypes;
        }
    }
}
