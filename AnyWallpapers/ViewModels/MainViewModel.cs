using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AnyWallpapers.ViewModels
{
   public  class MainViewModel : ViewModelBase
    {
       private Screen _selectedScreen;
       public Screen SelectedScreen
       {
           set
           {
               _selectedScreen = value;

               var index = Array.FindIndex(Screen.AllScreens, (h) =>
               {
                   return Equals(h, value);
               });

               if (index != -1)
               {
                    SelectedContentViewModel = ContentViewModels[index];
               }
                
           }
           get { return _selectedScreen; }
       }

       public ObservableCollection<IContentViewModel> ContentViewModels { get; } = new ObservableCollection<IContentViewModel>(); 
        public IContentViewModel SelectedContentViewModel { get; set; }

       public MainViewModel()
       {
           ContentViewModels.Add(new RunFileContentViewModel());
           ContentViewModels.Add(new ImageContentViewModel());

           SelectedContentViewModel = ContentViewModels.FirstOrDefault();
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
