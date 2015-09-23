using System;
using System.Globalization;
using System.Windows.Data;

namespace VideoWallpapers.XamlExtensions
{
    public class DividersConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                CheckType(values[0]);
                double result = ToDouble(values[0]);

                for (int index = 1; index < values.Length; index++)
                {
                    var value = values[index];
                    CheckType(value);
                    result /= ToDouble(value);
                }
                return result;
            }

            catch (Exception)
            {
                return null;
            }
        }

        private void CheckType(object value)
        {
            if (!(value is double || value is int))
            {
                throw new Exception("Not supported type");
            }
        }

        private double ToDouble(object value)
        {
            double result = 0;
            if (value is int)
            {
                result = (double)(int)value;
            }
            if (value is double)
            {
                result = (double)value;
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
