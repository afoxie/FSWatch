using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace FSWatch
{
    public partial class Main : Form
    {
        private FileSystemWatcher watcher;
        private SecondaryWindow sw = new SecondaryWindow();

        private bool running = false;

        private string startDir = "C:\\";
        private string filter = "*.txt";
        private bool recursive = false;
        private bool dupeNewFiles = false;

        public Main()
        {
            Form.CheckForIllegalCrossThreadCalls = false; // TERRIBLE practice! but im lazy and this works for my needs.
            InitializeComponent();
        }

        private void main_Focused(object sender, EventArgs e)
        {
            if (running && sw != null) sw.Focus(); // remove? never seems to be called.
        }

        [STAThread]
        private void startButton_Click(object sender, EventArgs e)
        {
            if (running)
            {
                sw.Hide();
                if (watcher != null) watcher.Dispose();
                running = false;
                startButton.Text = "Start Watching";
            }
            else
            {
                string workingDir = Directory.GetCurrentDirectory() + "\\Workspace_"
                    + (DateTime.Now.Subtract(DateTime.MinValue.AddYears(1969)).TotalMilliseconds.ToString());
                if (Directory.Exists(workingDir)) Directory.Delete(workingDir, true);
                Directory.CreateDirectory(workingDir);

                sw.Show();

                sw.fileList.Items.Clear();
                sw.fileList.Items.Add("-- START OF LOG --");

                watcher = new FileSystemWatcher(startDir);
                watcher.NotifyFilter = NotifyFilters.Attributes
                                     | NotifyFilters.CreationTime
                                     | NotifyFilters.DirectoryName
                                     | NotifyFilters.FileName
                                     | NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.Security
                                     | NotifyFilters.Size;
                watcher.Changed += (object _sender, FileSystemEventArgs _e) =>
                {
                    if (_e.ChangeType != WatcherChangeTypes.Changed)
                    {
                        return;
                    }
                    sw.fileList.Items.Add($"FILE MODIFIED @ {_e.FullPath}");
                };
                watcher.Created += (object _sender, FileSystemEventArgs _e) =>
                {
                    sw.fileList.Items.Add($"FILE CREATED @ {_e.FullPath}");
                    if (dupeNewFiles)
                    {
                        if (File.Exists(_e.FullPath) && Path.GetDirectoryName(_e.FullPath) != workingDir)
                        {
                            string fn = Path.Combine(workingDir, (_e.Name.Replace("\\", "_")));
                            if (File.Exists(fn)) File.Delete(fn);
                            File.Copy(_e.FullPath, fn);
                            sw.fileList.Items.Add($"     FILE DUPLICATED @ {workingDir + _e.Name}");
                        } else
                        {
                            sw.fileList.Items.Add($"     CANNOT DUPLICATE");
                        }
                    }
                };
                watcher.Deleted += (object _sender, FileSystemEventArgs _e) =>
                {
                    sw.fileList.Items.Add($"FILE CREATED @ {_e.FullPath}");
                };
                watcher.Renamed += (object _sender, RenamedEventArgs _e) =>
                {
                    sw.fileList.Items.Add($"FILE RENAMED (New: {_e.Name} Old: {_e.OldName}) @ {_e.FullPath}");
                };

                watcher.Filter = filter;
                watcher.IncludeSubdirectories = recursive;
                watcher.EnableRaisingEvents = true;

                running = true;
                startButton.Text = "Stop Watching";
            }
        }

        private void filterInput_TextChanged(object sender, EventArgs e)
        {
            filter = filterInput.Text;
        }

        private void startingInput_TextChanged(object sender, EventArgs e)
        {
            startDir = startingInput.Text;
        }

        private void recursiveCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            recursive = recursiveCheckbox.Checked;
        }

        private void copyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            dupeNewFiles = copyCheckbox.Checked;
        }
    }
}
