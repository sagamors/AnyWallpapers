namespace AnyWallpapers.ViewModels
{
    class RunFileContentViewModel : ContentViewModelBase
    {
        public RunFileContentViewModel(ScreenViewModel parentScreen) : base(parentScreen)
        {
            Name = "From file";
            Filter = "All files (*.*)|*.*";
        }
    }
}