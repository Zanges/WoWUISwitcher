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
        private Dictionary<string, string> uiPathDictionary = new Dictionary<string, string>();

        public MainWindow()
        {
            InitializeComponent();

            RefreshComboUiSelect();
        }

        private void ButtonLoadOnly_Click(object sender, RoutedEventArgs e)
        {
            LoadUi();
        }

        private void ButtonLoadLaunch_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LoadUi()
        {
            LoadWoWUI.Load(Settings.GetSetting("WoWDir"), uiPathDictionary[ComboUiSelect.Text]); // TODO potential Dictionary has no key
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settingsWindow = new SettingsWindow();
            if (settingsWindow.ShowDialog() == true)
            {
                RefreshComboUiSelect();
            }
        }

        private void RefreshComboUiSelect()
        {
            // Clear Old Data
            uiList = new List<string>();
            uiPathDictionary = new Dictionary<string, string>();

            // Get all Subfolders
            string[] possibleUIs = Directory.GetDirectories(Settings.GetSetting("UIDir"));

            foreach (string possibleUi in possibleUIs)
            {
                if (possibleUi != "_Global") // filter out the global folder
                {
                    // get sub-directory names
                    string[] subDirectoriesAbsolute = Directory.GetDirectories(possibleUi);
                    List<string> subDirectories = new List<string>();
                    foreach (string directory in subDirectoriesAbsolute)
                    {
                        subDirectories.Add(directory.Split(Path.DirectorySeparatorChar).Last());
                    }

                    if (subDirectories.Contains("Interface") && subDirectories.Contains("WTF")) // filter to keep only correct directories
                    {
                        string uiName = possibleUi.Split(Path.DirectorySeparatorChar).Last();
                        uiList.Add(uiName);
                        uiPathDictionary[uiName] = possibleUi;
                    }
                }

            }

            ComboUiSelect.ItemsSource = uiList;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshComboUiSelect();
        }
    }
}