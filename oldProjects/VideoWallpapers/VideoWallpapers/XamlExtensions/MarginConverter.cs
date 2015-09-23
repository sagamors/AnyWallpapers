using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Drawing;
using Point = System.Drawing.Point;

namespace VideoWallpapers.XamlExtensions
{
    public class ConsiderOriginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is Point || values[1] is Point))
            {
                throw new Exception("invalid type");
            }

            var origin =(Point) values[0];
            var bounds = (Point)values[1];
            double scale = (double) values[2];
            return new Thickness((bounds.X - origin.X)/ scale, (bounds.Y - origin.Y)/ scale, 0,0);
        }

        private void CheckType(Type value)
        {
            if (!(value == typeof(double) || value == typeof(int)))
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



    public class MarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                foreach (var value in values)
                {
                    CheckType(value.GetType());
                }
                Thickness result = new Thickness(); 
                var properties = result.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                int count = values.Length < 4 ? values.Length : 4;

                if (count > 0)
                {
                    result.Left = ToDouble(values[0]);
                }

                if (count > 1)
                {
                    result.Top = ToDouble(values[1]);
                }

                if (count > 2)
                {
                    result.Right = ToDouble(values[2]);
                }

                if (count > 3)
                {
                    result.Bottom = ToDouble(values[3]);
                }

                return result;
            }

            catch (Exception)
            {
                return null;
            }
        }

        private void CheckType(Type value)
        {
            if (!(value == typeof(double) || value == typeof(int)))
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
            if (!(value is Thickness))
            {
                throw new Exception("Not supported type");
            }

            foreach (var targetType in targetTypes)
            {
                CheckType(targetType);
            }

            object[] result = new object[targetTypes.Length];
            var properties = GetType().GetProperties(BindingFlags.Public);
            int count = targetTypes.Length < 4 ? targetTypes.Length : 4;
            for (int index = 0; index < count; index++)
            {
                properties[index].GetValue(result);
            }

            return result;
        }
    }
}