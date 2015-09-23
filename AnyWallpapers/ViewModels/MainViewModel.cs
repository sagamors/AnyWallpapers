using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AnyWallpapers.ViewModels
{
    class MainViewModel : ViewModelBase
    {
/*        public static MainViewModel Instance { get; } = new MainViewModel();*/
        public ScreenViewModel SelectedScreen { set; get; }
        public ObservableCollection<ScreenViewModel> ScreenViewModels { get; } = new ObservableCollection<ScreenViewModel>();

        public MainViewModel()
        {
            foreach (var screen in Screen.AllScreens)
            {
                ScreenViewModels.Add(new ScreenViewModel(screen));
            }
        }
    }
}
