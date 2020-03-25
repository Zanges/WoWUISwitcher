using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace WoWUISwitcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> uiList = new List<string>();

        public MainWindow()
        {
            InitializeComponent();

            RefreshComboUiSelect();
        }

        private void buttonLoadOnly_Click(object sender, RoutedEventArgs e)
        {
            //LoadWoWUI.Load();
        }

        private void buttonLoadLaunch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            if (settingsWindow.ShowDialog() == true)
            {
                RefreshComboUiSelect();
            }
        }

        private void RefreshComboUiSelect()
        {
            uiList = new List<string>();

            string[] possibleUIs = Directory.GetDirectories(Settings.GetSetting("UIDir"));

            foreach (string possibleUi in possibleUIs)
            {
                if (possibleUi != "_Global")
                {
                    string[] subDirectoriesAbsolute = Directory.GetDirectories(possibleUi);
                    
                    List<string> subDirectories = new List<string>();

                    foreach (string directory in subDirectoriesAbsolute)
                    {
                        subDirectories.Add(directory.Split(Path.DirectorySeparatorChar).Last());
                    }

                    if (subDirectories.Contains("Interface") && subDirectories.Contains("WTF"))
                    {
                        uiList.Add(possibleUi.Split(Path.DirectorySeparatorChar).Last());
                    }
                }

            }

            ComboUiSelect.ItemsSource = uiList;
        }
    }
}