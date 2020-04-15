using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;

namespace WoWUISwitcher
{
    internal static class LaunchWoW
    {
        public static bool Launch()
        {
            if (Process.GetProcessesByName("Battle.net").Length <= 0)
            {
                MessageBox.Show("Battle.net must be running!");
                return false;
            }

            var process = Process.GetProcessesByName("Battle.net").First();
            var path = process.GetMainModuleFileName();


            Process.Start(path, "--exec=\"launch WoW\"");
            Process.Start(path, "--exec=\"Close\"");

            return true;
        }
    }

    internal static class Extensions
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags,
            [Out] StringBuilder lpExeName, [In] [Out] ref uint lpdwSize);

        public static string GetMainModuleFileName(this Process process, int buffer = 1024)
        {
            var fileNameBuilder = new StringBuilder(buffer);
            var bufferLength = (uint) fileNameBuilder.Capacity + 1;
            return QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength)
                ? fileNameBuilder.ToString()
                : null;
        }
    }
}