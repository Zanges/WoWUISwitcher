using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WoWUISwitcher
{
    public static class Settings
    {
        private const string Filename = "settings.json";
        private static Dictionary<string, string> _settingsDictionary = new Dictionary<string, string>();

        private static readonly Dictionary<string, string> Defaults = new Dictionary<string, string>
        {
            {"WoWDir", AppDomain.CurrentDomain.BaseDirectory},
            {"UIDir", AppDomain.CurrentDomain.BaseDirectory + "\\UI"}
        };

        public static void Load()
        {
            if (File.Exists(Filename) == false)
            {
                _settingsDictionary = Defaults;
                return;
            }

            _settingsDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(Filename));
        }

        public static void Save()
        {
            var json = JsonConvert.SerializeObject(_settingsDictionary, Formatting.Indented);

            File.WriteAllText(Filename, json);
        }

        public static void SetSetting(string SettingName, string Value)
        {
            _settingsDictionary[SettingName] = Value;
        }

        public static string GetSetting(string SettingName)
        {
            return _settingsDictionary.ContainsKey(SettingName)
                ? _settingsDictionary[SettingName]
                : Defaults[SettingName];
        }
    }
}