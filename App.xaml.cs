using System.Windows;

namespace WoWUISwitcher
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Settings.Load();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            CloseHandler();
        }

        private void CloseHandler()
        {
            Settings.Save();
        }
    }
}