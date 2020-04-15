using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace WoWUISwitcher
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
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
            LoadUi();

            LaunchWoW.Launch();
        }

        private void LoadUi()
        {
            if (uiList.Count > 0)
            {
                LoadWoWUI.Load(Settings.GetSetting("WoWDir"),
                                uiPathDictionary[ComboUiSelect.Text]);
            }
            
        }

        private void ButtonSettings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            if (settingsWindow.ShowDialog() == true) RefreshComboUiSelect();
        }

        private void RefreshComboUiSelect()
        {
            // Clear Old Data
            uiList = new List<string>();
            uiPathDictionary = new Dictionary<string, string>();

            // Get all Subfolders
            var possibleUIs = Directory.GetDirectories(Settings.GetSetting("UIDir"));

            foreach (var possibleUi in possibleUIs)
                if (possibleUi != "_Global") // filter out the global folder
                {
                    // get sub-directory names
                    var subDirectoriesAbsolute = Directory.GetDirectories(possibleUi);
                    var subDirectories = new List<string>();
                    foreach (var directory in subDirectoriesAbsolute)
                        subDirectories.Add(directory.Split(Path.DirectorySeparatorChar).Last());

                    if (subDirectories.Contains("Interface") && subDirectories.Contains("WTF")
                    ) // filter to keep only correct directories
                    {
                        var uiName = possibleUi.Split(Path.DirectorySeparatorChar).Last();
                        uiList.Add(uiName);
                        uiPathDictionary[uiName] = possibleUi;
                    }
                }

            ComboUiSelect.ItemsSource = uiList;

            if (uiList.Count > 0)
            {
                var path = SymbolicLink.GetRealPath(Settings.GetSetting("WoWDir") + "\\Interface");
                var splitPath = path.Split(Path.DirectorySeparatorChar);
                var name = splitPath[^2];

                if (uiList.Contains(name))
                    ComboUiSelect.SelectedItem = name;
                //ComboUiSelect.Text
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshComboUiSelect();
        }
    }
}