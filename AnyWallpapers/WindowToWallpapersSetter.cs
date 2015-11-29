using System;
using System.Windows;
using System.Windows.Interop;

namespace AnyWallpapers
{
    class WindowToWallpapersSetter
    {
        public void Set(Window window)
        {
            // Fetch the Progman window
            IntPtr progman = Win32.FindWindow("Progman", null);

            IntPtr result = IntPtr.Zero;

            // Send 0x052C to Progman. This message directs Progman to spawn a 
            // WorkerW behind the desktop icons. If it is already there, nothing 
            // happens.
            Win32.SendMessageTimeout(progman,
                                   0x052C,
                                   new IntPtr(0),
                                   IntPtr.Zero,
                                   Win32.SendMessageTimeoutFlags.SMTO_NORMAL,
                                   1000,
                                   out result);

            //         this.Width=   SystemParameters.PrimaryScreenWidth;
            //            this.Height =SystemParameters.PrimaryScreenHeight;
/*
            this.Height = Screen.AllScreens[0].Bounds.Height;
            this.Width = Screen.AllScreens[0].Bounds.Width;*/

            IntPtr workerw = IntPtr.Zero;

            // We enumerate all Windows, until we find one, that has the SHELLDLL_DefView 
            // as a child. 
            // If we found that window, we take its next sibling and assign it to workerw.

            Win32.EnumWindows(((tophandle, topparamhandle) =>
            {
                IntPtr p = Win32.FindWindowEx(tophandle,
                                            IntPtr.Zero,
                                            "SHELLDLL_DefView",
                                            IntPtr.Zero);

                if (p != IntPtr.Zero)
                {
                    // Gets the WorkerW Window after the current one.
                    workerw = Win32.FindWindowEx(IntPtr.Zero,
                                               tophandle,
                                               "WorkerW",
                                               IntPtr.Zero);
                }
                return true;
            }), IntPtr.Zero);
            //            W32.RECT rect = new W32.RECT();
            //            W32.GetWindowRect(workerw, out rect);
            //            Debug.WriteLine(rect);
            IntPtr ptr = new WindowInteropHelper(window).Handle;
            Win32.SetParent(ptr, workerw);
            //Win32.MoveWindow(ptr, 0, 0, (int)Width, (int)Height, true);
        }
    }
}
