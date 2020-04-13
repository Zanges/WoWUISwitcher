using System;
using System.Configuration;

namespace WoWUISwitcher
{
    static class LoadWoWUI
    {
        public static void Load(string WoWDir, string UIDir)
        {
            SymbolicLink.CreateSymbolicLink(UIDir + "\\Interface", WoWDir + "\\Interface", true);
            SymbolicLink.CreateSymbolicLink(UIDir + "\\WTF", WoWDir + "\\WTF", true);
        }
    }
}
