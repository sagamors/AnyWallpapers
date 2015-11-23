using System;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AnyWallpapers.ViewModels
{
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
                PreviewPicture.Source = imageSource;
                Height = imageSource.Height;
                Width = imageSource.Width;
                SetPositionToPreviewPicture(PreviewPosition);
            }
        }
    }
}