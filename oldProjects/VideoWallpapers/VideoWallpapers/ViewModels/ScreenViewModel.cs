using System.Drawing;
using System.Windows.Forms;
using PropertyChanged;

namespace VideoWallpapers.ViewModels
{
    [ImplementPropertyChanged]
    public class IViewModelBase
    {

    }

    public enum eWallpaperType
    {
        Programm,
        File,
        Video
    }

    // \todo bad name
    public enum ePosition
    {
        Center,
        Tile,
        Fit,
        Fill,
        Stretch,
        Custom
    }

    // Может это модель?
    public class WallpaperBase
    {
        public eWallpaperType Type { get; }
        public double Width { set; get; }
        public double Height { set; get; }
        public Point Origin { set; get; }
        public SettingsViewModel SettingsViewModel { set; get; }
        public TransformWallpaper Transform { set; get; }
        public bool IsFullScreen { set; get; }
        // \todo bad name
        public ePosition Position { set; get; }
    }

    public class TransformWallpaper
    {
        
    }

    public class SettingsViewModel
    {

    }

    public class ScreenViewModel
    {
        public ScreenViewModel(Screen screen)
        {
            Screen = screen;
        }

        public Screen Screen { set; get; }

        public double Width => Screen.Bounds.Width;

        public double Height => Screen.Bounds.Height;

        // \\todo запоминать
        public Point Origin => new Point(Screen.Bounds.Left, Screen.Bounds.Top);

        public WallpaperBase Wallpaper { set; get; }
    }
}
