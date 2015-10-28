using System;
using System.Collections.Generic;
using System.Windows;
using AnyWallpapers.Controls;
using Microsoft.Win32;
using UserControl = System.Windows.Controls.UserControl;

namespace AnyWallpapers.Views
{
    /// <summary>
    /// Interaction logic for ScreensView.xaml
    /// </summary>
    public partial class ScreensView : UserControl
    {
        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
            "Scale", typeof (double), typeof (ScreensView), new PropertyMetadata(default(double)));

        public double Scale
        {
            get { return (double) GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof (IEnumerable<ScreenItem>), typeof (ScreensView), new PropertyMetadata(default(IEnumerable<ScreenItem>)));

      
        public IEnumerable<ScreenItem> Items
        {
            get { return (IEnumerable<ScreenItem>) GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty OriginOffsetProperty = DependencyProperty.Register(
            "OriginOffset", typeof (double), typeof (ScreensView), new PropertyMetadata(default(double)));

        public double OriginOffset
        {
            get { return (double) GetValue(OriginOffsetProperty); }
            set { SetValue(OriginOffsetProperty, value); }
        }
        
        public ScreensView()
        {
            InitializeComponent();
            SizeChanged += ScreensView_SizeChanged;
            SystemEventsOnDisplaySettingsChanged(null, null);
            SystemEvents.DisplaySettingsChanged += SystemEventsOnDisplaySettingsChanged;
        }

        private void ScreensView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CalcScale();
        }

        private void CalcScale()
        {
            double widthScale = SystemParameters.VirtualScreenWidth / (ActualWidth - (Padding.Left + Padding.Right));
            double heightScale = SystemParameters.VirtualScreenHeight / (ActualHeight - (Padding.Top + Padding.Bottom));
            if (widthScale > heightScale)
            {
                Scale = widthScale;
            }
            else
            {
                Scale = heightScale;
            }
        }

        private void SystemEventsOnDisplaySettingsChanged(object sender, EventArgs eventArgs)
        {
            //if (Screen.AllScreens.Length > AlternationCount)
            //{
            //    AlternationCount = Screen.AllScreens.Length;
            //    CalcScale();
            //}

           // Items = new ObservableCollection<Screen>(Screen.AllScreens);
        }
    }
}
