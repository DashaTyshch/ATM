using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ATM
{
    public abstract class BaseValueConverters<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        #region Private Members 


        private static T mConverter = null;

        #endregion

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new T());
        }
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
