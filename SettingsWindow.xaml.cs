using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WoWUISwitcher
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
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
            TextboxWoWDir.Text = PickFolderDialog(TextboxWoWDir.Text.Equals("") ? AppDomain.CurrentDomain.BaseDirectory : TextboxWoWDir.Text, "Pick WoW Directory");
        }

        private void ButtonUIDir_Click(object sender, RoutedEventArgs e)
        {
            TextboxUIDir.Text = PickFolderDialog(TextboxUIDir.Text.Equals("") ? AppDomain.CurrentDomain.BaseDirectory : TextboxUIDir.Text, "Pick UI Directory");
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
            string output = startDir;

            var folderBrowser = new FolderBrowserForWPF.Dialog
            {
                Title = dialogTitle,
                FileName = startDir
            };

            if (folderBrowser.ShowDialog() == true)
            {
                output = folderBrowser.FileName;
            }

            return output;
        }
    }
}
