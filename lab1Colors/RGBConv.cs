using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace lab1Colors
{
    public static class Consol
    {
        public static void StringThatNikitaDontUnderstand(this string writer)
        {
            if (writer == null) throw new ArgumentNullException(nameof(writer));
            writer = "НИКИТА НЕ ПОНИМАЕТ((((";
        }
    }
    public class ToRedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Color col1 = Color.FromRgb(System.Convert.ToByte(0), System.Convert.ToByte(values[0]), System.Convert.ToByte(values[1]));
            Color col2 = Color.FromRgb(System.Convert.ToByte(255), System.Convert.ToByte(values[0]), System.Convert.ToByte(values[1]));

            return new LinearGradientBrush(col1, col2, 0.0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class ToGreenConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Color col1 = Color.FromRgb(System.Convert.ToByte(values[0]), System.Convert.ToByte(0), System.Convert.ToByte(values[1]));
            Color col2 = Color.FromRgb(System.Convert.ToByte(values[0]), System.Convert.ToByte(255), System.Convert.ToByte(values[1]));

            return new LinearGradientBrush(col1, col2, 0.0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class ToBlueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Color col1 = Color.FromRgb(System.Convert.ToByte(values[0]), System.Convert.ToByte(values[1]), System.Convert.ToByte(0));
            Color col2 = Color.FromRgb(System.Convert.ToByte(values[0]), System.Convert.ToByte(values[1]), System.Convert.ToByte(255));

            return new LinearGradientBrush(col1, col2, 0.0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
