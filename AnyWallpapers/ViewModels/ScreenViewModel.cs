using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PropertyChanged;

namespace AnyWallpapers.ViewModels
{
    [ImplementPropertyChanged]
    public class ScreenViewModel : ViewModelBase
    {
        public bool IsSelected { get; set; }
        public Screen Screen { set; get; }
        public double Width => Screen.Bounds.Width;
        public double Height => Screen.Bounds.Height;
        public Point Origin => new Point(Screen.Bounds.Left, Screen.Bounds.Top);
        public IContentViewModel Content { set; get; }
        public ObservableCollection<IContentViewModel> ContentCollection { get; } =new ObservableCollection<IContentViewModel>();
        public ScreenViewModel(Screen screen)
        {
            Screen = screen;
            ContentCollection.Add(new ImageContentViewModel(this));
            ContentCollection.Add(new RunFileContentViewModel(this));
            ContentCollection.Add(new VideoContentViewModel(this));
            Content = ContentCollection.FirstOrDefault();
        }
    }

    public interface IDispayedContent
    {
        string Name { get; }
    }
}
