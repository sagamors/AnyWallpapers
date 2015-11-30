using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AnyWallpapers.Helpers;
using Microsoft.Win32;

namespace AnyWallpapers.ViewModels
{
   public class MainViewModel : ViewModelBase
    {
       public ObservableCollection<ScreenViewModel> Items { get; set; } = new ObservableCollection<ScreenViewModel>();
       public ScreenViewModel SelectedScreen { set; get; }
       public ICommand ApplyCommand { get; }
       public MainViewModel()
       {
           SystemEvents.DisplaySettingsChanged += SystemEventsOnDisplaySettingsChanged;
            ApplyCommand = new RelayCommand(o => Apply());
       }

       public void Apply()
       {
           foreach (var screenViewModel in Items)
           {
               screenViewModel.Content.Apply();
           }
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
