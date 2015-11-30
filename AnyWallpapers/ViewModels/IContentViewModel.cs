using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AnyWallpapers.ViewModels
{
    public enum ePreviewPosition
    {
        Center=1,
        Fit=2,
        Fill=3,
        Stretch=4,
        Custom=5
    }

    public enum eFilePosition
    {
        Stretch = 4,
        Custom = 5
    }

    public interface IPosition
    {
        ScreenViewModel Screen { get; }
        string Name { get; }
        Rect GetPosition();
    }

    
    public interface IContentViewModel
    {
        double Width { get; }
        double Height { get; }
        string Name { get; }
        string Filter { get; }
        ePreviewPosition PreviewPosition {get; }
        ICommand ShowLoadDialogCommand { get; }
        string FullPath { get; }
        Image PreviewPicture { get; }
        int X { set; get; }
        int Y { set; get; }
        void  Apply();
    }
}
