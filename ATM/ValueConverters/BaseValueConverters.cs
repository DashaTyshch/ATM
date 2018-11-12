using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ATM
{
    public abstract class BaseValueConverters<T> : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
