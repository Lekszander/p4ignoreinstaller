﻿using System.Windows;

namespace p4ignoreinstaller
{
    /// <summary>
    /// Interaction logic for ContentEditor.xaml
    /// </summary>
    public partial class ContentEditor : Window
    {
        IgnoreFileInfo ifi;

        public ContentEditor()
        {
            InitializeComponent();

            // Take up 60% of the screen vertically and horizontally.
            Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 0.6);
            Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 0.6);

            ifi = (IgnoreFileInfo)App.Current.Properties["IgnoreFile"];

            // Set the editor's starting text to the default ignored Unreal files and folders.
            Editor.Text = ifi.Text;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Can't modify read only resource file so we set the IgnoreFileInfo text instead.
            ifi.Text = Editor.Text;
            Close();
        }

        private void NoSave_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
