using System;
using System.Windows;
using FolderBrowserForWPF;

namespace WoWUISwitcher
{
    /// <summary>
    ///     Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            TextboxWoWDir.Text = Settings.GetSetting("WoWDir");
            TextboxUIDir.Text = Settings.GetSetting("UIDir");
        }

        private void ButtonWoWDir_Click(object sender, RoutedEventArgs e)
        {
            TextboxWoWDir.Text =
                PickFolderDialog(
                    TextboxWoWDir.Text.Equals("") ? AppDomain.CurrentDomain.BaseDirectory : TextboxWoWDir.Text,
                    "Pick WoW Directory");
        }

        private void ButtonUIDir_Click(object sender, RoutedEventArgs e)
        {
            TextboxUIDir.Text =
                PickFolderDialog(
                    TextboxUIDir.Text.Equals("") ? AppDomain.CurrentDomain.BaseDirectory : TextboxUIDir.Text,
                    "Pick UI Directory");
        }

        private void buttonOk_Click(object sender, RoutedEventArgs e)
        {
            Settings.SetSetting("WoWDir", TextboxWoWDir.Text);
            Settings.SetSetting("UIDir", TextboxUIDir.Text);

            Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private string PickFolderDialog(string startDir, string dialogTitle = "Pick Directory")
        {
            var output = startDir;

            var folderBrowser = new Dialog
            {
                Title = dialogTitle,
                FileName = startDir
            };

            if (folderBrowser.ShowDialog() == true) output = folderBrowser.FileName;

            return output;
        }
    }
}