using System.Windows;

namespace p4ignoreinstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Edit_Contents_Click(object sender, RoutedEventArgs e)
        {
            ContentEditor contentEditor = new ContentEditor();
            contentEditor.Show();
        }

        private void Select_Folder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Install_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
