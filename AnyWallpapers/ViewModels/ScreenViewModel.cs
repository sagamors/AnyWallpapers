using System.Drawing;
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
           
        // \\todo запоминать
        public Point Origin => new Point(Screen.Bounds.Left, Screen.Bounds.Top);

        public IDispayedContent Content { get; }

        public ePositionType PositionType { get; }
        public enum ePositionType
        {
            Center,
            Tile,
            Fit,
            Fill,
            Stretch,
            Custom,
        }

        public ScreenViewModel(Screen screen, IDispayedContent content)
        {
            Screen = screen;
            Content = content;
        }
    }

    public interface IDispayedContent
    {
        string Name { get; }
    }
}
