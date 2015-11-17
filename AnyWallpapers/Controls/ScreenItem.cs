using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using AnyWallpapers.ViewModels;

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

        public static readonly DependencyProperty PictureProperty = DependencyProperty.Register(
            "Picture", typeof (Image), typeof (ScreenItem), new PropertyMetadata(default(Image)));

        public Image Picture
        {
            get { return (Image) GetValue(PictureProperty); }
            set { SetValue(PictureProperty, value); }
        }

        internal ScreensViewer ScreensViewer { get; set; }

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
                control.CalcY();
            }));

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
            "IsSelected", typeof (bool), typeof (ScreenItem), new PropertyMetadata(default(bool), (o, args) =>
            {
                var control = (ScreenItem) o;
                var selectedContainer = control.GetSelectedScreenItemFromItem();

                if (control.IsSelected)
                {
                    if (selectedContainer != null)
                        selectedContainer.IsSelected = false;
                    // todo fix
                    control.ParentScreensViewer.SetValue(ScreensViewer.SelectedItemProperty, (ScreenViewModel)control.DataContext);
                }
                else
                {
                    control.ParentScreensViewer.SetValue(ScreensViewer.SelectedItemProperty, null);
                }
                control.IsChecked = control.IsSelected;
            }));

        public bool IsSelected
        {
            get { return (bool) GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        internal ScreensViewer ParentScreensViewer
        {
            get { return VisualTreeHelperEx.FindParent<ScreensViewer>(this); }
        }

        internal ItemsControl ParentItemsControl
        {
            get
            {
                return ItemsControl.ItemsControlFromItemContainer(VisualTreeHelper.GetParent(this));
            }
        }

        /*        ///<summary>
                /// Return the ItemsControl that owns the given container element
                ///</summary>
                public static ItemsControl ItemsControlFromItemContainer(DependencyObject container)
                {
                    UIElement ui = container as UIElement;
                    if (ui == null)
                        return null;

                    // ui appeared in items collection
                    ItemsControl ic = LogicalTreeHelper.GetParent(ui) as ItemsControl;
                    if (ic != null)
                    {
                        // this is the right ItemsControl as long as the item
                        // is (or is eligible to be) its own container
                        //IGeneratorHost host = ic as IGeneratorHost;
                        //if (host.IsItemItsOwnContainer(ui))
                            return ic;
                        /*else
                            return null;#1#
                    }

                    ui = VisualTreeHelper.GetParent(ui) as UIElement;

                    return GetItemsOwner(ui);
                }
                /// </summary>
                public static ItemsControl GetItemsOwner(DependencyObject element)
                {
                    ItemsControl container = null;
                    Panel panel = element as Panel;

                    if (panel != null && panel.IsItemsHost)
                    {
                        // see if element was generated for an ItemsPresenter
                        /*
                        ItemsPresenter ip = ItemsPresenter.FromPanel(panel);

                        if (ip != null)
                        {
                            // if so use the element whose style begat the ItemsPresenter
                            container = ip.Owner;
                        }
                        else
                        {
                            // otherwise use element's templated parent
                            container = panel.TemplatedParent as ItemsControl;
                        }
                        #1#
                    }

                    return container;
                }*/

        public double YOffset
        {
            get { return (double) GetValue(YOffsetProperty); }
            set { SetValue(YOffsetProperty, value); }
        }

        public ScreenItem()
        {
            Checked += ScreenItem_Checked;
            Unchecked += ScreenItem_Unchecked;
        }

        private void ScreenItem_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsChecked.HasValue && !IsChecked.Value)
            {
                IsSelected = false;
            }
        }

        private void ScreenItem_Checked(object sender, RoutedEventArgs e)
        {
            if (IsChecked.HasValue && IsChecked.Value)
            {
                IsSelected = true;
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

        private ScreenItem GetSelectedScreenItemFromItem()
        {
            if (ParentScreensViewer.SelectedItem == null) return null;
            var screenItem = ParentScreensViewer.SelectedScreenItem as ScreenItem;
            if (screenItem != null)
            {
                return screenItem;
            }
            return VisualTreeHelper.GetChild(ParentItemsControl.ItemContainerGenerator.ContainerFromItem(ParentScreensViewer.SelectedItem),0) as ScreenItem;
        }
    }
}
