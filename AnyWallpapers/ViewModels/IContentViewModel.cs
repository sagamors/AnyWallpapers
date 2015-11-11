using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
    }

    class FromFileContentViewModel : IContentViewModel
    {
        public string FullPath { private set; get; }
        public string Name { get; }
        public string Filter { get; }
        public ePosition Position { get; set; }
        public ICommand ShowLoadDialogCommand { get; }

        public FromFileContentViewModel()
        {
            Name = "From file";
            Filter = "";
            ShowLoadDialogCommand = new RelayCommand(o => ShowLoadDialog());
        }

        public void ShowLoadDialog()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = Filter;
            var isOpen =ofd.ShowDialog();
            if (isOpen != null && isOpen.Value)
            {
                FullPath = ofd.FileName;
            }
        }
    }
}
