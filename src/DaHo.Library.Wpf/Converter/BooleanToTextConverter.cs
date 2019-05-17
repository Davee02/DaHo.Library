using System;
using System.Globalization;
using System.Windows.Data;

namespace DaHo.Library.Wpf.Converter
{
    public class BooleanToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool booleanValue)
            {
                return booleanValue
                    ? "Ja"
                    : "Nein";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                return string.Equals(stringValue, "Ja", StringComparison.InvariantCultureIgnoreCase);
            }

            return false;
        }
    }
}
