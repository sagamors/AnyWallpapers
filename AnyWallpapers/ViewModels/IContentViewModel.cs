using System;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AnyWallpapers.Helpers;
using Microsoft.Win32;

namespace AnyWallpapers.ViewModels
{
    public enum ePosition
    {
        Center,
        Tile,
        Fit,
        Fill,
        Stretch,
        Custom
    }

    public interface IContentViewModel
    {
        string Name { get; }
        string Filter { get; }
        ePosition Position { set; get; }
        ICommand ShowLoadDialogCommand { get; }
        string FullPath { get; }
        Image Picture { get; }
    }

    abstract class  ContentViewModelBase : IContentViewModel
    {
        public string Name { protected set;get; }
        public string Filter { protected set; get; }
        public ePosition Position { set; get; }
        public ICommand ShowLoadDialogCommand { protected set; get; }
        public string FullPath { protected set; get; }
        public Image Picture { protected set;get; }

        public ContentViewModelBase()
        {
            ShowLoadDialogCommand = new RelayCommand(o => ShowLoadDialog());
            Picture = new Image();
        }

        public virtual void ShowLoadDialog()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = Filter;
            var isOpen = ofd.ShowDialog();
            if (isOpen != null && isOpen.Value)
            {
                FullPath = ofd.FileName;
            }
        }
    }

    class RunFileContentViewModel : ContentViewModelBase
    {
        public RunFileContentViewModel()
        {
            Name = "From file";
            Filter = "All files (*.*)|*.*";
        }
    }

    internal class ImageContentViewModel : ContentViewModelBase
    {
        public ImageContentViewModel()
        {
            Name = "Image";
            Filter = "Images (*.jpg,*.jpeg,*.png,*.bmp,)|*.jpg;*.jpeg,*.png,*.bmp";
        }

        public override void ShowLoadDialog()
        {
            base.ShowLoadDialog();
            if (File.Exists(FullPath))
            {
                ImageSource imageSource = new BitmapImage(new Uri(FullPath));
                Picture.Source = imageSource;
            }
        }
    }
}
