namespace WoWUISwitcher
{
    static class LoadWoWUI
    {
        public static void Load(string WoWDir, string UIDir)
        {
            SymbolicLink.CreateSymbolicLink(UIDir + "\\Interface", WoWDir + "\\Interface", true);
            SymbolicLink.CreateSymbolicLink(UIDir + "\\WTF", WoWDir + "\\WTF", true);
        }

        private static void Link(string Link, string Target)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C mklink /D " + Link + " " + Target;
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
