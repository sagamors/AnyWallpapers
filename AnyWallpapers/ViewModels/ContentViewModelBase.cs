using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using AnyWallpapers.Helpers;
using Microsoft.Win32;

namespace AnyWallpapers.ViewModels
{
    abstract class  ContentViewModelBase : IContentViewModel
    {
        public ScreenViewModel ParentScreen { get; }
        public double Width
        {
            protected set
            {
                _width = value;
                PreviewPicture.Width = _width;
            }
            get { return _width; }
        }

        public double Height
        {
            protected set
            {
                _height = value;
                PreviewPicture.Height = _height;
            }
            get { return _height; }
        }

        private ePreviewPosition _previewPosition;
        private double _width;
        private double _height;
        public string Name { protected set;get; }
        public string Filter { protected set; get; }

        public ePreviewPosition PreviewPosition
        {
            private set
            {
                _previewPosition = value;
                SetPositionToPreviewPicture(_previewPosition);
            }
            get { return _previewPosition; }
        }

        public ICommand ShowLoadDialogCommand { protected set; get; }
        public string FullPath { protected set; get; }
        public Image PreviewPicture { protected set;get; }

        public int X { set; get; }
        public int Y { set; get; }

        public ContentViewModelBase(ScreenViewModel parentScreen)
        {
            ParentScreen = parentScreen;
            ShowLoadDialogCommand = new RelayCommand(o => ShowLoadDialog());
            PreviewPicture = new Image();
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

        protected void SetPositionToPreviewPicture(ePreviewPosition osition)
        {
            switch (osition)
            {
                case ePreviewPosition.Center:
                    int centerScreenX = (int) (ParentScreen.Width / 2);
                    int centerScreenY = (int) (ParentScreen.Height / 2);
                    int centerContentX = (int) (Width / 2);
                    int centerContentY = (int) (Height / 2);
                    X = centerScreenX - centerContentX;
                    Y = centerScreenY - centerContentY;
                    break;
                case ePreviewPosition.Custom:
                    SetXYToBegin();
                    break;
                case ePreviewPosition.Fill:
                    PreviewPicture.Stretch = Stretch.UniformToFill;
                    SetXYToBegin();
                    break;
                case ePreviewPosition.Fit:
                    PreviewPicture.Stretch = Stretch.Uniform;
                    SetXYToBegin();
                    break;
                case ePreviewPosition.Stretch:
                    PreviewPicture.Stretch = Stretch.Fill;
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
}