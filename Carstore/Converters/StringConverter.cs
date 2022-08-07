using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Carstore.Converters
{
    public class StringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is string && !string.IsNullOrWhiteSpace(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "";
        }
    }
}
