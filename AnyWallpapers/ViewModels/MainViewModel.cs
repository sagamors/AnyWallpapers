using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;

namespace AnyWallpapers.ViewModels
{
   public  class MainViewModel : ViewModelBase
    {
/*        public static MainViewModel Instance { get; } = new MainViewModel();*/
        public ScreenViewModel SelectedScreen { set; get; }
        public ObservableCollection<ScreenViewModel> ScreenViewModels { get; } = new ObservableCollection<ScreenViewModel>();
        public ObservableCollection<IContentViewModel> ContentViewModels { get; } = new ObservableCollection<IContentViewModel>(); 
        public IContentViewModel SelectedContentViewModel { get; set; }
        public MainViewModel()
        {
            foreach (var screen in Screen.AllScreens)
            {
                ScreenViewModels.Add(new ScreenViewModel(screen,null));
            }
            ContentViewModels.Add(new FromFileContentViewModel());

            SelectedContentViewModel = ContentViewModels.FirstOrDefault();

        }
    }
}
