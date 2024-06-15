using System;
using System.Globalization;
using System.Windows.Data;

namespace SdHelper.Views
{
    public class StringToDoubleConverter : IValueConverter
    {
        private string lastConvertBackString;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not double)
            {
                return null;
            }

            var stringValue = lastConvertBackString ?? value.ToString();
            lastConvertBackString = null;

            return stringValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string s))
            {
                return null;
            }

            if (!double.TryParse(s, out var result))
            {
                return null;
            }

            lastConvertBackString = s;
            return result;
        }
    }
}