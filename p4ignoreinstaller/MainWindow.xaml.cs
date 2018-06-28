using System.Diagnostics;
using System.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Text;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Media;

namespace p4ignoreinstaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IgnoreFileInfo ifi;
        string installLog;

        public MainWindow()
        {
            InitializeComponent();
            ifi = (IgnoreFileInfo) App.Current.Properties["IgnoreFile"];
        }

        private void Edit_Contents_Click(object sender, RoutedEventArgs e)
        {
            ContentEditor contentEditor = new ContentEditor();
            contentEditor.Show();
        }

        private void Select_Folder_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;

                CommonFileDialogResult result = dialog.ShowDialog(this);

                if (result == CommonFileDialogResult.Ok)
                {
                    ifi.FilePath = dialog.FileName;
                    FolderLabel.Text = ifi.FilePath;
                    InstallBox.IsEnabled = true;
                }
            }
        }

        private void Install_Click(object sender, RoutedEventArgs e)
        {
            DeployIgnoreFile();
            SetEnvironmentVariable(ifi.File);

            bool succeeded;

            ValidateInstall(out succeeded, out installLog);

            if (succeeded)
            {
                InstallStatusLabel.Text = "Installation Complete";
                InstallButton.Visibility = Visibility.Collapsed;
                CompleteButton.Visibility = Visibility.Visible;
            }
            else
            {
                InstallStatusLabel.Text = "An error occurred";
                InstallButton.Visibility = Visibility.Collapsed;
                LogButton.Visibility = Visibility.Visible;
            }
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(installLog, "Log", MessageBoxButton.OK, MessageBoxImage.Error);

            if (result == MessageBoxResult.OK)
            {
                // No fallback for now.
                Application.Current.Shutdown();
            }
        }

        private void Complete_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            string p4Variables = GetP4Variables();
            string path = String.Empty;

            p4Variables = Regex.Replace(p4Variables, @"(?:\ \(set\))", "");
            string[] variables = p4Variables.Split(Environment.NewLine.ToCharArray());

            for (int i = 0; i < variables.Length; i++)
            {
                string[] tokens = variables[i].Split('=');
                // Even is variable name, odd is value.
                if(tokens[0] == "P4IGNORE")
                {
                    path = tokens[1];
                    break;
                }
            }

            try
            {
                if(!String.IsNullOrEmpty(path))
                {
                    File.Delete(path);
                }

                SetEnvironmentVariable(String.Empty);

                if (!ValidateCleanup())
                {
                    throw new CleanupException("The P4IGNORE environment variable was not cleared.");
                }
            }
            catch(CleanupException cex)
            {
                Console.WriteLine("An error occurred: '{0}'", cex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: '{0}'", ex);
            }

            MessageBoxResult result = MessageBox.Show("The current p4ignore file was successfully deleted and the P4IGNORE environment variable was cleared.", "Successfull Cleanup", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeployIgnoreFile()
        {
            string file = ifi.File;

            if(!string.IsNullOrEmpty(file))
            {
                try
                {
                    // Delete the file if it exists.
                    if (File.Exists(file))
                    {
                        // Note that no lock is put on the
                        // file and the possibility exists
                        // that another process could do
                        // something with it between
                        // the calls to Exists and Delete.
                        File.Delete(file);
                    }

                    // Create the file.
                    using (FileStream fs = File.Create(file))
                    {
                        System.Byte[] info = new UTF8Encoding(true).GetBytes(ifi.Text);
                        // Add some information to the file.
                        fs.Write(info, 0, info.Length);
                    }
                }
                catch(IOException e)
                {
                    Console.WriteLine("An error occurred: '{0}'", e);
                }
            }
        }

        private void SetEnvironmentVariable(string value)
        {
            Process proc = CreateConsoleProcess("p4 set P4IGNORE=" + value);
            proc.Start();
            proc.WaitForExit();
        }

        private bool ValidateCleanup()
        {
            string p4Variables = GetP4Variables();

            using (StringReader sr = new StringReader(p4Variables))
            {
                while (true)
                {
                    string line = sr.ReadLine();

                    if (line != null)
                    {
                        if (line.Contains("P4IGNORE"))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                return true;
            }
        }

        private void ValidateInstall(out bool succeeded, out string output)
        {
            succeeded = false;

            output = GetP4Variables();

            using (StringReader sr = new StringReader(output))
            {
                while (true)
                {
                    string line = sr.ReadLine();
                    
                    if(line != null)
                    {
                        if (line.Contains("P4IGNORE"))
                        {
                            succeeded = true;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private string GetP4Variables()
        {
            StringBuilder sb = new StringBuilder();

            using (Process proc = CreateConsoleProcess("p4 set", true))
            {
                proc.Start();

                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();

                    sb.AppendLine(line);
                }
            }

            return sb.ToString();
        }

        private Process CreateConsoleProcess(string command, bool redirectStandardOutput = false, bool enableRaisingEvents = false, EventHandler callback = null)
        {
            string baseCommand = "/C ";
            string fullCommand = baseCommand + command;

            Process proc = new Process();
            proc.EnableRaisingEvents = enableRaisingEvents;

            proc.StartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = fullCommand,
                UseShellExecute = false,
                RedirectStandardOutput = redirectStandardOutput,
                CreateNoWindow = true
            };

            if(enableRaisingEvents && callback != null)
            {
                proc.Exited += callback;
            }

            return proc;
        }
    }
}
