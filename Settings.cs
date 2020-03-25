using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WoWUISwitcher
{
    public static class Settings
    {
        private static Dictionary<string, string> _settingsDictionary = new Dictionary<string, string>();
        private static readonly Dictionary<string, string> Defaults = new Dictionary<string, string>()
        {
            {"test", "val"},
        };

        private const string Filename = "settings.json";

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
            string json = JsonConvert.SerializeObject(_settingsDictionary, Formatting.Indented);

            File.WriteAllText(Filename, json);

            Console.WriteLine("saved");
        }

        public static void SetSetting(string SettingName, string Value)
        {
            _settingsDictionary[SettingName] = Value;

            Console.WriteLine("set " + SettingName + " to " + Value);
        }

        public static string GetSetting(string SettingName)
        {
            return _settingsDictionary.ContainsKey(SettingName)
                ? _settingsDictionary[SettingName]
                : Defaults[SettingName];
        }
    }
}
