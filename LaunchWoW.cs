using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WoWUISwitcher
{

    static class LaunchWoW
    {

        public static void Launch()
        {
            var process = Process.GetProcessesByName("Battle.net").First();
            string path = process.GetMainModuleFileName();


            Process.Start(path, "--exec=\"launch WoW\"");
            Process.Start(path, "--exec=\"Close\"");
        }

        public static void LaunchCustomized()
        {
            Launch();

            bool modificationPending = true;

            while (modificationPending)
            {
                WindowHelper.EnumWindows enumWindows = new WindowHelper.EnumWindows();
                enumWindows.GetWindows();
                foreach (var obj in enumWindows.Items)
                {
                    WindowHelper.EnumWindowsItem window = (WindowHelper.EnumWindowsItem) obj;
                    if (window.Text == "World of Warcraft")
                    {
                        System.Diagnostics.Debug.WriteLine(window.Process.Id);

                        System.Threading.Thread.Sleep(5000);
                        
                        IntPtr handle = window.Handle;
                        //MoveWindow(handle, 0, 0, 800, 600, false);
                        WindowHelper.MakeExternalWindowBorderless(handle);
                        modificationPending = false;
                    }
                }

                System.Diagnostics.Debug.WriteLine("wait");
            }

            
            //while (FindWindow(null, "World of Warcraft") != 0)
            //{
            //    System.Diagnostics.Debug.WriteLine("wait");
            //    System.Threading.Thread.Sleep(100);
            //}

            //System.Threading.Thread.Sleep(1000);

            //System.Diagnostics.Debug.WriteLine(Process.GetProcessesByName("World of Warcraft").First().Id);
            //var handle = Process.GetProcessesByName("World of Warcraft").First().MainWindowHandle;

            //MoveWindow(handle, 0, 0, 800, 600, true);
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
    }

    internal static class Extensions
    {

        [DllImport("Kernel32.dll")]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

        public static string GetMainModuleFileName(this Process process, int buffer = 1024)
        {
            var fileNameBuilder = new StringBuilder(buffer);
            uint bufferLength = (uint)fileNameBuilder.Capacity + 1;
            return QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) ?
                fileNameBuilder.ToString() :
                null;
        }

    }

}
