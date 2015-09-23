using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VideoWallpapers.Helpers
{
    class ValueContainer <T> : DependencyObject
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            "Value", typeof (T), typeof (ValueContainer<T>), new PropertyMetadata(default(T)));

        public T Value
        {
            get { return (T) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }   
    }

    class DoubleValue : ValueContainer<Double>
    {
    }
}
