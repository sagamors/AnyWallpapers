using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;
using Control = System.Windows.Controls.Control;
using System.Drawing;
using Point = System.Drawing.Point;

namespace VideoWallpapers.Constrols
{
    public class ScreenViewerControl : Control
    {
        public static readonly DependencyProperty AlternationCountProperty = DependencyProperty.Register(
            "AlternationCount", typeof (int), typeof (ScreenViewerControl), new PropertyMetadata(default(int)));

        public int AlternationCount
        {
            get { return (int) GetValue(AlternationCountProperty); }
            set { SetValue(AlternationCountProperty, value); }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            "ItemsSource", typeof (IEnumerable<Screen>), typeof (ScreenViewerControl), new PropertyMetadata(Screen.AllScreens,
                (o, args) =>
                {
                    var control = (ScreenViewerControl) o;
                    control.SetOrigin();
                }));

        public IEnumerable<Screen> ItemsSource
        {
            get { return (IEnumerable<Screen>) GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", typeof (double), typeof (ScreenViewerControl), new PropertyMetadata(10.0));

        public double Scale
        {
            get { return (double) GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly DependencyProperty OriginProperty = DependencyProperty.Register(
            "Origin", typeof (Point), typeof (ScreenViewerControl), new PropertyMetadata(default(Point)));

        public Point Origin
        {
            get { return (Point) GetValue(OriginProperty); }
            set { SetValue(OriginProperty, value); }
        }

        static ScreenViewerControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenViewerControl), new FrameworkPropertyMetadata(typeof(ScreenViewerControl)));
        }

        public ScreenViewerControl()
        {
            ItemsSource = new ObservableCollection<Screen>(Screen.AllScreens);
            SystemEventsOnDisplaySettingsChanged(null, null);
            SystemEvents.DisplaySettingsChanged += SystemEventsOnDisplaySettingsChanged;
            SizeChanged += ScreenViewerControl_SizeChanged;
            SetOrigin();
        }

        private void ScreenViewerControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CalcScale();
        }

        private void SystemEventsOnDisplaySettingsChanged(object sender, EventArgs eventArgs)
        {
            if (Screen.AllScreens.Length > AlternationCount)
            {
                AlternationCount = Screen.AllScreens.Length;
                CalcScale();
            }

            ItemsSource = new ObservableCollection< Screen > (Screen.AllScreens);
        }

        private void CalcScale()
        {
            double widthScale = SystemParameters.VirtualScreenWidth /
                                (ActualWidth - (Padding.Left + Padding.Right));
            double heightScale = SystemParameters.VirtualScreenHeight /
                    (ActualHeight - (Padding.Top + Padding.Bottom));
            if (widthScale > heightScale)
            {
                Scale = widthScale ;
            }
            else
            {
                Scale = heightScale;
            }
        }

        private void SetOrigin()
        {
            var origin = new Point();

            foreach (var screen in ItemsSource)
            {
                //We find the right and the top point.
                if (screen.Bounds.Left < origin.X)
                {
                    origin.X = screen.Bounds.Left;
                }

                if (screen.Bounds.Top < origin.Y)
                {
                    origin.Y = screen.Bounds.Top;
                }
            }

            Origin = origin;
        }
    }
}
