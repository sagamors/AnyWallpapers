using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using DrawBehindDesktopIcons;
using VideoWallpapers;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
//http://www.codeproject.com/Articles/856020/Draw-behind-Desktop-Icons-in-Windows
namespace WpfTutorialSamples.Audio_and_Video
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           new WallpapersWindow().Show();
        }
    }
}