using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AutoRenameApp.Shared
{
    public class Convert : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vis = Boolean.Parse(value.ToString());

            return vis ? "Visible" : "Hidden";
        }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool IsReversed { get; set; }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);
            if (this.IsReversed)
            {
                val = !val;
            }
            return (val) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = ((Visibility)value == Visibility.Visible) ? true : false;
            if (this.IsReversed)
            {
                val = !val;
            }
            return val;
        }
    }
}