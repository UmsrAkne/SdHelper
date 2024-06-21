using System;
using System.Globalization;
using System.Windows.Data;

namespace SdHelper.Views
{
    public class WordConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not string w)
            {
                return null;
            }

            // w に改行文字が含まれる場合、テキストブロックでの改行を防ぐために空文字を返す
            return (w.Contains('\n') || w.Contains('\r')) ? " $" : w;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}