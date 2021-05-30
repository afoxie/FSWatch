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
        private bool logToFile = false;
        private NewFileBehavior onNewFile = NewFileBehavior.None;

        private enum NewFileBehavior
        {
            Duplicate,
            Delete,
            None
        }

        public Main()
        {
            Form.CheckForIllegalCrossThreadCalls = false; // TERRIBLE practice! but im lazy and this works for my needs.
            InitializeComponent();
        }

        private void main_Focused(object sender, EventArgs e)
        {
            if (running && sw != null) sw.Focus(); // remove? never seems to be called.
        }

        private void WriteLog(string txt, bool appendTimestamp = true)
        {
            if (logToFile)
            {
                string logFile = Directory.GetCurrentDirectory() + "\\out.log";
                if (!File.Exists(logFile)) File.Create(logFile);
                if (appendTimestamp)
                {
                    txt = "[" + (DateTime.Now.ToString("HH:mm:ss")) + "]: " + txt;
                }
                try
                {
                    File.AppendAllText(logFile, txt + "\n");
                } catch (Exception) { }
            }
        }

        private bool ValidityCheck(string path, string comparativeDir)
        {
            return (
                File.Exists(path)
                && Path.GetDirectoryName(path) != comparativeDir
                && Path.GetDirectoryName(path) != Directory.GetCurrentDirectory()
            );
        }

        [STAThread]
        private void startButton_Click(object sender, EventArgs e)
        {
            if (running)
            {
                sw.Hide();
                if (watcher != null) watcher = null;
                running = false;
                startButton.Text = "Start Watching";
                WriteLog("-- STOPPED WATCHING FOR FILESYSTEM UPDATES --", false);
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
                    if (ValidityCheck(_e.FullPath, workingDir))
                    {
                        if (_e.ChangeType != WatcherChangeTypes.Changed)
                        {
                            return;
                        }
                        sw.fileList.Items.Add($"FILE MODIFIED @ {_e.FullPath}");
                        WriteLog($"FILE MODIFIED @ {_e.FullPath}");
                    }
                };
                watcher.Created += (object _sender, FileSystemEventArgs _e) =>
                {
                    sw.fileList.Items.Add($"FILE CREATED @ {_e.FullPath}");
                    WriteLog($"FILE CREATED @ {_e.FullPath}");
                    if (ValidityCheck(_e.FullPath, workingDir))
                    {
                        switch (onNewFile)
                        {
                            case NewFileBehavior.Duplicate:
                                string fn = Path.Combine(workingDir, (_e.Name.Replace("\\", "_")));
                                if (File.Exists(fn)) File.Delete(fn);
                                File.Copy(_e.FullPath, fn);
                                sw.fileList.Items.Add($"     FILE DUPLICATED @ {workingDir + _e.Name}");
                                WriteLog($"     FILE DUPLICATED @ {workingDir + _e.Name}");
                                break;
                            case NewFileBehavior.Delete:
                                try
                                {
                                    File.Delete(_e.FullPath);
                                    if (!File.Exists(_e.FullPath))
                                    {
                                        sw.fileList.Items.Add("     FILE DELETED");
                                        WriteLog("     FILE DELETED");
                                    } else
                                    {
                                        throw new Exception();
                                    }
                                } catch(Exception)
                                {
                                    sw.fileList.Items.Add("     COULD NOT DELETE FILE");
                                    WriteLog("     COULD NOT DELETE FILE");
                                }
                                break;
                            case NewFileBehavior.None:
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        sw.fileList.Items.Add($"     CAN'T R/W FILE");
                        WriteLog("     COULD NOT ACCESS FILE");
                    }
                };
                watcher.Deleted += (object _sender, FileSystemEventArgs _e) =>
                {
                    if (ValidityCheck(_e.FullPath, workingDir))
                    {
                        sw.fileList.Items.Add($"FILE CREATED @ {_e.FullPath}");
                        WriteLog($"FILE CREATED @ {_e.FullPath}");
                    }
                };
                watcher.Renamed += (object _sender, RenamedEventArgs _e) =>
                {
                    if (ValidityCheck(_e.FullPath, workingDir))
                    {
                        sw.fileList.Items.Add($"FILE RENAMED (New: {_e.Name} Old: {_e.OldName}) @ {_e.FullPath}");
                        WriteLog($"FILE RENAMED (New: {_e.Name} Old: {_e.OldName}) @ {_e.FullPath}");
                    }
                };

                watcher.Filter = filter;
                watcher.IncludeSubdirectories = recursive;
                watcher.EnableRaisingEvents = true;

                running = true;
                startButton.Text = "Stop Watching";
                WriteLog("-- STARTED WATCHING FOR FILESYSTEM UPDATES --", false);
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

        private void newFileActionChoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            Debug.WriteLine(newFileActionChoice.SelectedIndex);
            switch (newFileActionChoice.SelectedIndex)
            {
                case 0: // nothing
                    onNewFile = NewFileBehavior.None;
                    break;
                case 1: // dupe
                    onNewFile = NewFileBehavior.Duplicate;
                    break;
                case 2: // delete
                    onNewFile = NewFileBehavior.Delete;
                    break;
                default:
                    break;
            }
        }

        private void logToFileCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            logToFile = logToFileCheckbox.Checked;
        }
    }
}
