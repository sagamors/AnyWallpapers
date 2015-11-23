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
        Center=1,
        Fit=2,
        Fill=3,
        Stretch=4,
        Custom=5
    }

    public interface IContentViewModel
    {
        double Width { get; }
        double Height { get; }
        string Name { get; }
        string Filter { get; }
        ePosition Position { set; get; }
        ICommand ShowLoadDialogCommand { get; }
        string FullPath { get; }
        Image Picture { get; }
        int X { set; get; }
        int Y { set; get; }
    }

    abstract class  ContentViewModelBase : IContentViewModel
    {
        public ScreenViewModel ParentScreen { get; }
        public double Width
        {
            protected set
            {
                _width = value;
                Picture.Width = _width;
            }
            get { return _width; }
        }

        public double Height
        {
            protected set
            {
                _height = value;
                Picture.Height = _height;
            }
            get { return _height; }
        }

        private ePosition _position;
        private double _width;
        private double _height;
        public string Name { protected set;get; }
        public string Filter { protected set; get; }

        public ePosition Position
        {
            set
            {
                _position = value;
                SetPositionToPicture(_position);
            }
            get { return _position; }
        }

        public ICommand ShowLoadDialogCommand { protected set; get; }
        public string FullPath { protected set; get; }
        public Image Picture { protected set;get; }

        public int X { set; get; }
        public int Y { set; get; }

        public ContentViewModelBase(ScreenViewModel parentScreen)
        {
            ParentScreen = parentScreen;
            ShowLoadDialogCommand = new RelayCommand(o => ShowLoadDialog());
            Picture = new Image();
        }

        public virtual void ShowLoadDialog()
        {
            var ofd = new OpenFileDialog {Filter = Filter};
            var isOpen = ofd.ShowDialog();
            if (isOpen != null && isOpen.Value)
            {
                FullPath = ofd.FileName;
            }
        }

        protected void SetPositionToPicture(ePosition osition)
        {
            switch (osition)
            {
                case ePosition.Center:
                    int centerScreenX = (int) (ParentScreen.Width / 2);
                    int centerScreenY = (int) (ParentScreen.Height / 2);
                    int centerContentX = (int) (Width / 2);
                    int centerContentY = (int) (Height / 2);
                    X = centerScreenX - centerContentX;
                    Y = centerScreenY - centerContentY;
                    break;
                case ePosition.Custom:
                    SetXYToBegin();
                    break;
                case ePosition.Fill:
                    Picture.Stretch = Stretch.UniformToFill;
                    SetXYToBegin();
                    break;
                case ePosition.Fit:
                    Picture.Stretch = Stretch.Uniform;
                    SetXYToBegin();
                    break;
                case ePosition.Stretch:
                    Picture.Stretch = Stretch.Fill;
                    SetXYToBegin();
                    break;
            }
        }

        public void SetXYToBegin()
        {
            X = 0;
            Y = 0;
        }
    }

    class RunFileContentViewModel : ContentViewModelBase
    {
        public RunFileContentViewModel(ScreenViewModel parentScreen) : base(parentScreen)
        {
            Name = "From file";
            Filter = "All files (*.*)|*.*";
        }
    }

    internal class ImageContentViewModel : ContentViewModelBase
    {
        public ImageContentViewModel(ScreenViewModel parentScreen) : base(parentScreen)
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
                Height = imageSource.Height;
                Width = imageSource.Width;
                SetPositionToPicture(Position);
            }
        }
    }
}
