using System;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace AnyWallpapers.ViewModels
{
   public  class MainViewModel : ViewModelBase
    {
       public ObservableCollection<ScreenViewModel> Items { get; set; } = new ObservableCollection<ScreenViewModel>();
       public ScreenViewModel SelectedScreen { set; get; }

       public MainViewModel()
       {
           SystemEvents.DisplaySettingsChanged += SystemEventsOnDisplaySettingsChanged;
       }

       private void SystemEventsOnDisplaySettingsChanged(object sender, EventArgs eventArgs)
       {
//           for (int index = 0; index < Screen.AllScreens.Length; index++)
//           {
//               var screen = Screen.AllScreens[index];
//               ContentViewModels[index].Screen = screen;
//           }
        }
    }
}
