using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyWallpapers.ViewModels
{
    class VideoContentViewModel : ContentViewModelBase
    {
        public VideoContentViewModel(ScreenViewModel parentScreen) : base(parentScreen)
        {
            Name = "Video (Simple player)";
            //todo add 
            Filter = "Images (*.mpg4)|*.mpg4";
        }

        public override void Apply()
        {
            
        }
    }
}
