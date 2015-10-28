using System.Windows;
using System.Windows.Controls;

namespace AnyWallpapers.Controls
{ 
    public class ScreenItem : HeaderedItemsControl
    {
        public static readonly DependencyProperty ScreenHeightProperty = DependencyProperty.Register(
            "ScreenHeight", typeof (double), typeof (ScreenItem), new PropertyMetadata(default(double)));

        public double ScreenHeight
        {
            get { return (double) GetValue(ScreenHeightProperty); }
            set { SetValue(ScreenHeightProperty, value); }
        }

        public static readonly DependencyProperty ScreenWidthProperty = DependencyProperty.Register(
            "ScreenWidth", typeof (double), typeof (ScreenItem), new PropertyMetadata(default(double)));

        public double ScreenWidth
        {
            get { return (double) GetValue(ScreenWidthProperty); }
            set { SetValue(ScreenWidthProperty, value); }
        }

        public static readonly DependencyProperty ScreenXProperty = DependencyProperty.Register(
            "ScreenX", typeof (double), typeof (ScreenItem), new PropertyMetadata(default(double)));

        public double ScreenX
        {
            get { return (double) GetValue(ScreenXProperty); }
            set { SetValue(ScreenXProperty, value); }
        }

        public static readonly DependencyProperty ScreenYProperty = DependencyProperty.Register(
            "ScreenY", typeof (double), typeof (ScreenItem), new PropertyMetadata(default(double)));

        public double ScreenY
        {
            get { return (double) GetValue(ScreenYProperty); }
            set { SetValue(ScreenYProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", typeof (double), typeof (ScreenItem), new PropertyMetadata(default(double), (o, args) =>
            {
                var control = (ScreenItem)o;
                control.Width = control.ScreenWidth/control.Scale;
                control.Height = control.ScreenHeight / control.Scale;
            }));

        public double Scale
        {
            get { return (double) GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        internal static readonly DependencyPropertyKey XKey = DependencyProperty.RegisterReadOnly("X", typeof(double), typeof(ScreenItem), new PropertyMetadata(double.NaN,
            (o, args) =>
            {
                var control = (ScreenItem) o;
                Canvas.SetLeft(control, control.X);
            }));
        public static readonly DependencyProperty XProperty = XKey.DependencyProperty;

        public double X
        {
            get { return (double)GetValue(XProperty); }
            protected set { SetValue(XKey, value); }
        }

        internal static readonly DependencyPropertyKey YKey = DependencyProperty.RegisterReadOnly("Y", typeof(double), typeof(ScreenItem), new PropertyMetadata(double.NaN,
            (o, args) =>
            {
                var control = (ScreenItem)o;
                Canvas.SetTop(control, control.Y);
            }));

        public static readonly DependencyProperty YProperty = YKey.DependencyProperty;

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            protected set { SetValue(YKey, value); }
        }

        static ScreenItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenItem), new FrameworkPropertyMetadata(typeof(ScreenItem)));
        }
    }
}
