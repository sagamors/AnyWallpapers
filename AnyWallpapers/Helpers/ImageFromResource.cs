using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AnyWallpapers.Helpers
{
   public static class ImageFromResource
    {
        static internal ImageSource Get(string psAssemblyName, string psResourceName)
        {
            Uri oUri = new Uri("pack://application:,,,/" + psAssemblyName + ";component/" + psResourceName, UriKind.RelativeOrAbsolute);
            return BitmapFrame.Create(oUri);
        }

       static internal ImageSource Get(string psResourceName)
       {
           return Get(System.Reflection.Assembly.GetExecutingAssembly().GetName().Name, psResourceName);
       }
    }
}
