﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using AnyWallpapers.ViewModels;
using Microsoft.Win32;
using Control = System.Windows.Controls.Control;

namespace AnyWallpapers.Controls
{
    public class ScreensViewer : Control
    {
        static ScreensViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreensViewer), new FrameworkPropertyMetadata(typeof(ScreensViewer)));
        }

        public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(
        "Scale", typeof(double), typeof(ScreensViewer), new PropertyMetadata(default(double), (o, args) =>
        {
            var control = (ScreensViewer)o;
            control.CalcSystemHeight();
            control.CalcSystemWidth();
        }));

        public double Scale
        {
            get { return (double)GetValue(ScaleProperty); }
            set { SetValue(ScaleProperty, value); }
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register(
            "Items", typeof(ObservableCollection<ScreenViewModel>), typeof(ScreensViewer), new PropertyMetadata(default(ObservableCollection<ScreenViewModel>),
                (o, args) =>
                {
                    var control = (ScreensViewer)o;
                    var newCollection =  args.NewValue as ObservableCollection<ScreenViewModel>;
                    var oldCollection = args.OldValue as ObservableCollection<ScreenViewModel>;
                    if (newCollection != null) newCollection.CollectionChanged += control.Items_CollectionChanged;
                    if (oldCollection != null) oldCollection.CollectionChanged -= control.Items_CollectionChanged;
                }));

        public ObservableCollection<ScreenViewModel> Items
        {
            get { return (ObservableCollection<ScreenViewModel>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            "SelectedItem", typeof (object), typeof (ScreensViewer), new PropertyMetadata(default(object)));

        public object SelectedItem
        {
            get { return (object) GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static readonly DependencyProperty OriginXOffsetProperty = DependencyProperty.Register(
            "OriginXOffset", typeof(double), typeof(ScreensViewer), new PropertyMetadata(default(double)));

        public double OriginXOffset
        {
            get { return (double)GetValue(OriginXOffsetProperty); }
            set { SetValue(OriginXOffsetProperty, value); }
        }

        public static readonly DependencyProperty OriginYOffsetProperty = DependencyProperty.Register(
            "OriginYOffset", typeof(double), typeof(ScreensViewer), new PropertyMetadata(default(double)));

        public double OriginYOffset
        {
            get { return (double)GetValue(OriginYOffsetProperty); }
            set { SetValue(OriginYOffsetProperty, value); }
        }

        public static readonly DependencyProperty SystemWidthProperty = DependencyProperty.Register(
            "SystemWidth", typeof (double), typeof (ScreensViewer), new PropertyMetadata(default(double), (o, args) =>
            {
                var control = (ScreensViewer)o;
                control.CalcSystemWidth();
            }));

        public double SystemWidth
        {
            get { return (double) GetValue(SystemWidthProperty); }
            set { SetValue(SystemWidthProperty, value); }
        }

        public static readonly DependencyProperty SystemHeightProperty = DependencyProperty.Register(
            "SystemHeight", typeof (double), typeof (ScreensViewer), new PropertyMetadata(default(double),
                (o, args) =>
                {
                    var control = (ScreensViewer) o;
                    control.CalcSystemHeight();
                }));

        public double SystemHeight
        {
            get { return (double) GetValue(SystemHeightProperty); }
            set { SetValue(SystemHeightProperty, value); }
        }

        public ScreensViewer()
        {
            Items = new ObservableCollection<ScreenViewModel>();

            foreach (var screen in Screen.AllScreens)
            {
                Items.Add(new ScreenViewModel(screen));
            }

            SizeChanged += ScreensView_SizeChanged;
            SystemEventsOnDisplaySettingsChanged(null, null);
            SystemEvents.DisplaySettingsChanged += SystemEventsOnDisplaySettingsChanged;
            Items.CollectionChanged += Items_CollectionChanged;
            Loaded += ScreensViewer_Loaded;
        }

        private void ScreensViewer_Loaded(object sender, RoutedEventArgs e)
        {
           // var container = _itemsControl.ItemContainerGenerator.ContainerFromItem(Items.First());
        }

        ItemsControl _itemsControl;
        public override void OnApplyTemplate()
        {
            _itemsControl = (GetTemplateChild("PART_ItemsControl") as ItemsControl);
            SystemHeight = SystemParameters.VirtualScreenHeight;
            SystemWidth = SystemParameters.VirtualScreenWidth;
            base.OnApplyTemplate();
        }

        private void Items_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SetOrigin();
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

        private void SetOrigin()
        {
            var origin = new Point();

            foreach (var screen in Items)
            {
                //We find the right and the top point.
                if (screen.Origin.X < origin.X)
                {
                    origin.X = screen.Origin.X;
                }

                if (screen.Origin.Y < origin.Y)
                {
                    origin.Y = screen.Origin.Y;
                }
            }

            OriginXOffset = origin.X;
            OriginYOffset = origin.Y;
        }

        private void CalcSystemHeight()
        {
            if (Math.Abs(Scale) < 0.1) return;
            _itemsControl.Height = SystemHeight / Scale;
        }

        private void CalcSystemWidth()
        {
            if (Math.Abs(Scale) < 0.1) return;
            _itemsControl.Width = SystemWidth / Scale;
        }
    }
}
