using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AnyWallpapers.Controls
{ 
    public class ScreenItem : ToggleButton
    {
        private Canvas _canvas;

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
            "Scale", typeof (double), typeof (ScreenItem), new PropertyMetadata(1.0, (o, args) =>
            {
                var control = (ScreenItem)o;
                control.Width = control.ScreenWidth/control.Scale;
                control.Height = control.ScreenHeight / control.Scale;
                control.CalcX();
                control.CalcY();
            }));

        public double Scale
        {
            get { return (double) GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        internal ScreensViewer ScreensViewer { get; set; }

     /*   internal static readonly DependencyPropertyKey XKey = DependencyProperty.RegisterReadOnly("X", typeof(double), typeof(ScreenItem), new PropertyMetadata(double.NaN,
            (o, args) =>
            {
/*                var control = (ScreenItem) o;
                Canvas.SetLeft(control, control.X);#1#
            }));
        public static readonly DependencyProperty XProperty = XKey.DependencyProperty;

        public double X
        {
            get { return (double)GetValue(XProperty); }
            protected set { SetValue(XKey, value); }
        }*/
/*
        internal static readonly DependencyPropertyKey YKey = DependencyProperty.RegisterReadOnly("Y", typeof(double), typeof(ScreenItem), new PropertyMetadata(double.NaN,
            (o, args) =>
            {
/*                var control = (ScreenItem)o;
                Canvas.SetTop(control, control.Y);#1#
            }));

        public static readonly DependencyProperty YProperty = YKey.DependencyProperty;

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            protected set { SetValue(YKey, value); }
        }*/

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
            "X", typeof (double), typeof (ScreenItem), new PropertyMetadata(default(double)));

        public double X
        {
            get { return (double) GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
            "Y", typeof (double), typeof (ScreenItem), new PropertyMetadata(default(double)));

        public double Y
        {
            get { return (double) GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty XOffsetProperty = DependencyProperty.Register(
            "XOffset", typeof (double), typeof (ScreenItem), new PropertyMetadata(default(double), (o, args) =>
            {
                var control = (ScreenItem) o;
                control.CalcX();
            }));

        public double XOffset
        {
            get { return (double) GetValue(XOffsetProperty); }
            set { SetValue(XOffsetProperty, value); }
        }

        public static readonly DependencyProperty YOffsetProperty = DependencyProperty.Register(
            "YOffset", typeof (double), typeof (ScreenItem), new PropertyMetadata(default(double), (o, args) =>
            {
                var control = (ScreenItem)o;
                control.CalcX();
            }));

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", typeof (bool), typeof (ScreenItem), new PropertyMetadata(default(bool), (o, args) =>
            {
                var control = (ScreenItem) o;
                if (control.IsSelected)
                {
                    control.ParentScreensViewer.SelectedItem = control;
                }
            }));

        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        private ScreensViewer ParentScreensViewer
        {
            get
            {
                return ParentSelector as ScreensViewer;
            }
        }

        internal Control ParentSelector
        {
            get
            {
                return ItemsControl.ItemsControlFromItemContainer(this) as Control;
            }
        }

        public double YOffset
        {
            get { return (double) GetValue(YOffsetProperty); }
            set { SetValue(YOffsetProperty, value); }
        }

        public ScreenItem()
        {
            Checked += ScreenItem_Checked;
        }

        private void ScreenItem_Checked(object sender, RoutedEventArgs e)
        {
            if (IsChecked.HasValue && IsChecked.Value)
            {
                ParentScreensViewer.SelectedItem = this;
            }
        }

        static ScreenItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenItem), new FrameworkPropertyMetadata(typeof(ScreenItem)));
        }

        public void CalcX()
        {
            X = Math.Abs((-XOffset + ScreenX) / Scale);
        }

        public void CalcY()
        {
            Y = Math.Abs((-YOffset + ScreenY) / Scale);
        }
    }
}
