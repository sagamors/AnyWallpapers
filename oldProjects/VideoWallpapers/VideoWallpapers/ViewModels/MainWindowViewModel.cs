using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;
using PropertyChanged;

namespace VideoWallpapers.ViewModels
{
    [ImplementPropertyChanged]
    public class MainWindowViewModel
    {
        public static MainWindowViewModel Instance { get; } = new MainWindowViewModel();

        public ScreenViewModel SelectedScreen { set; get; }
        public ObservableCollection<ScreenViewModel> ScreenViewModels { get; } =new ObservableCollection<ScreenViewModel>();

        private MainWindowViewModel()
        {
            SystemEvents.DisplaySettingsChanged += SystemEventsOnDisplaySettingsChanged;
            SystemEventsOnDisplaySettingsChanged(null, null);
            Console.WriteLine();
            foreach (var screen in Screen.AllScreens)
            {
                ScreenViewModels.Add(new ScreenViewModel(screen));
                // For each screen, add the screen properties to a list box.
                Debug.WriteLine("Device Name: " + screen.DeviceName);
                Debug.WriteLine("Bounds: " + screen.Bounds.ToString());
                Debug.WriteLine("Type: " + screen.GetType().ToString());
                Debug.WriteLine("Working Area: " + screen.WorkingArea.ToString());
                Debug.WriteLine("Primary Screen: " + screen.Primary.ToString());
            }
        }

        private void SystemEventsOnDisplaySettingsChanged(object sender, EventArgs eventArgs)
        {
//            if (Screen.AllScreens.Length > AlternationCount)
//            {
//                AlternationCount = Screen.AllScreens.Length;
////                SystemParameters.VirtualScreenWidth
//            }
        }
    }
}
