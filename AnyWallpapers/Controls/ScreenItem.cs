using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnyWallpapers.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AnyWallpapers.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:AnyWallpapers.Controls;assembly=AnyWallpapers.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ScreenItem/>
    ///
    /// </summary>
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

        public static readonly DependencyProperty OriginXProperty = DependencyProperty.Register(
            "OriginX", typeof (double), typeof (ScreenItem), new PropertyMetadata(default(double)));

        public double OriginX
        {
            get { return (double) GetValue(OriginXProperty); }
            set { SetValue(OriginXProperty, value); }
        }

        public static readonly DependencyProperty OriginYProperty = DependencyProperty.Register(
            "OriginY", typeof (double), typeof (ScreenItem), new PropertyMetadata(default(double)));

        public double OriginY
        {
            get { return (double) GetValue(OriginYProperty); }
            set { SetValue(OriginYProperty, value); }
        }

        static ScreenItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ScreenItem), new FrameworkPropertyMetadata(typeof(ScreenItem)));
        }
    }
}
