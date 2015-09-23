using System.Drawing;
using System.Windows.Forms;
using PropertyChanged;


namespace AnyWallpapers.ViewModels
{
    public class ScreenViewModel : ViewModelBase
    {
        public Screen Screen { set; get; }

        public double Width => Screen.Bounds.Width;

        public double Height => Screen.Bounds.Height;

        // \\todo запоминать
        public Point Origin => new Point(Screen.Bounds.Left, Screen.Bounds.Top);
        public ScreenViewModel(Screen screen)
        {
            Screen = screen;
        }
    }
}
