using System.Windows;

namespace p4ignoreinstaller
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Properties["IgnoreFile"] = new IgnoreFileInfo();
        }
    }
}
