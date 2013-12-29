// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using Krkadoni.EnigmaSettings;
using Krkadoni.EnigmaSettings.Interfaces;
using SharpCompress.Archive.Zip;
using SharpCompress.Common;
using SharpCompress.Compressor.Deflate;
using SharpCompress.Reader;
using Starksoft.Net.Ftp;
using AbstractWritableArchiveExtensions = SharpCompress.Archive.AbstractWritableArchiveExtensions;

namespace Krkadoni.SESE
{
    public partial class SplitList : Form
    {

        private readonly ErrorProvider _erp;
        private readonly BindingSource _bsTasks;
        private Krkadoni.EnigmaSettings.Settings _settings;
        private BindingList<VmXmlSatellite> _satellites;
        private static SplitList _defInstance;

        public static SplitList DefInstance
        {
            get { return _defInstance ?? (_defInstance = new SplitList()); }
            set { _defInstance = value; }
        }

        // state variables to control GUI
        private static bool isLoadingList;
        private static bool isSavingList;
        private static bool isFTPDownload;
        private static bool isFTPUpload;

        public SplitList()
        {

            InitializeComponent();
            SetProgressBarVisible(false);
            Version.Text = "ver. " + Application.ProductVersion;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplitList));
            var NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            NameColumn.DataPropertyName = "Name";
            NameColumn.HeaderText = Properties.Resources.TASKS_COLUMN_HEADER;
            NameColumn.Name = "Tasks";
            NameColumn.ReadOnly = true;

            _erp = new ErrorProvider(this);
            _erp.ContainerControl = this;
            _bsTasks = new BindingSource(this.components);
            _bsTasks.DataMember = "Tasks";
            _bsTasks.CurrentChanged += _bsTasks_CurrentChanged;
            _bsTasks.DataSource = AppSettings.DefInstance;
            gridTasks.AutoGenerateColumns = false;
            gridTasks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { NameColumn });
            gridTasks.DataSource = _bsTasks;


            //txtTaskDirectory.DataBindings.Add(new Binding("Text", _bsTasks, "Directory", true, DataSourceUpdateMode.OnPropertyChanged));
            //txtTaskName.DataBindings.Add(new Binding("Text", _bsTasks, "Name", true, DataSourceUpdateMode.OnPropertyChanged));
            //txtZipFileName.DataBindings.Add(new Binding("Text", _bsTasks, "FileName", true, DataSourceUpdateMode.OnPropertyChanged));
            //ceZIP.DataBindings.Add(new Binding("Checked", _bsTasks, "ZIP", true, DataSourceUpdateMode.OnPropertyChanged));
            //ceDVBT.DataBindings.Add(new Binding("Checked", _bsTasks, "DVBT", true, DataSourceUpdateMode.OnPropertyChanged));
            //ceDVBC.DataBindings.Add(new Binding("Checked", _bsTasks, "DVBC", true, DataSourceUpdateMode.OnPropertyChanged));
            //ceStream.DataBindings.Add(new Binding("Checked", _bsTasks, "Streams", true, DataSourceUpdateMode.OnPropertyChanged));
        }

        private void FrmSplitList_Load(object sender, System.EventArgs e)
        {
            var selectedColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            selectedColumn.DataPropertyName = "Selected";
            selectedColumn.HeaderText = Properties.Resources.SELECT_COLUMN_HEADER;
            selectedColumn.Name = "Selected";
            selectedColumn.ReadOnly = false;

            var satelliteColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            satelliteColumn.DataPropertyName = "Name";
            satelliteColumn.HeaderText = Properties.Resources.SATELLITE_COLUMN_HEADER;
            satelliteColumn.Name = "Satellite";
            satelliteColumn.ReadOnly = true;

            var positionStringColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            positionStringColumn.DataPropertyName = "PositionString";
            positionStringColumn.HeaderText = Properties.Resources.POSITION_COLUMN_HEADER;
            positionStringColumn.Name = "PositionString";
            positionStringColumn.ReadOnly = true;
            positionStringColumn.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            positionStringColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            gridSatellites.AutoGenerateColumns = false;
            gridSatellites.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { selectedColumn, satelliteColumn, positionStringColumn });
            positionStringColumn.Width = 60;
            positionStringColumn.Resizable = DataGridViewTriState.False;
            selectedColumn.Width = 80;
            selectedColumn.Resizable = DataGridViewTriState.False;
            satelliteColumn.Width = 360;
            satelliteColumn.Resizable = DataGridViewTriState.True; ;
            gridSatellites.EditMode = DataGridViewEditMode.EditOnEnter;

            AppSettings.DefInstance.PropertyChanged += AppSettings_PropertyChanged;
            btnDonate.Visible = !AppSettings.DefInstance.Donated;
            UpdateControlsEnabled();
            UpdateProfileList();
            UpdateControlsWithTaskInfo();
        }

        /// <summary>
        /// Fills controls with informations about currently selected task
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _bsTasks_CurrentChanged(object sender, EventArgs e)
        {
            UpdateControlsWithTaskInfo();
            UpdateControlsEnabled();
        }

        /// <summary>
        /// Updates controls when one of settings changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppSettings_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            btnDonate.Visible = !AppSettings.DefInstance.Donated;
        }

        #region "Helper Methods"

        /// <summary>
        /// Returns currently selected task from the Tasks grid
        /// </summary>
        /// <returns></returns>
        private Task CurrentTask()
        {
            //var currentRow = gridTasks.CurrentRow;
            //Task task = null;
            //if (currentRow != null)
            //    task = (Task)currentRow.DataBoundItem;

            if (_bsTasks != null)
            {
                return (Task)_bsTasks.Current;
            }

            return null;

        }

        /// <summary>
        /// Updates Download and Upload dropdown buttons with the list of profiles
        /// </summary>
        private void UpdateProfileList()
        {
            AppSettings.Log.Debug("Updating profile list");
            for (int i = btnDownload.DropDownItems.Count - 1; i >= 0; i--)
            {
                var menuItem = (ToolStripMenuItem)btnDownload.DropDownItems[i];
                btnDownload.DropDownItems.RemoveAt(i);
                menuItem.Dispose();
            }

            for (int i = btnUpload.DropDownItems.Count - 1; i >= 0; i--)
            {
                var menuItem = (ToolStripMenuItem)btnUpload.DropDownItems[i];
                btnUpload.DropDownItems.RemoveAt(i);
                menuItem.Dispose();
            }

            for (int i = 0; i < AppSettings.DefInstance.Profiles.Count(); i++)
            {
                var downloadMenuItem = new ToolStripMenuItem();
                downloadMenuItem.Text = AppSettings.DefInstance.Profiles[i].Name;
                downloadMenuItem.Name = "downloadMenuItem" + i.ToString(CultureInfo.InvariantCulture);
                downloadMenuItem.Click += btnDownloadMenuItem_OnClick;
                btnDownload.DropDownItems.Add(downloadMenuItem);

                var uploadMenuItem = new ToolStripMenuItem();
                uploadMenuItem.Text = AppSettings.DefInstance.Profiles[i].Name;
                uploadMenuItem.Name = "uploadMenuItem" + i.ToString(CultureInfo.InvariantCulture);
                uploadMenuItem.Click += btnUploadMenuItem_OnClick;
                btnUpload.DropDownItems.Add(uploadMenuItem);
            }
            AppSettings.Log.Debug("Profile list updated");
        }

        /// <summary>
        /// Creates temporary directory and returns its path
        /// </summary>
        /// <returns></returns>
        private string GetTemporaryDirectory()
        {
            AppSettings.Log.Debug("Creating temporary directory");
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            AppSettings.Log.DebugFormat("Temporary directory {0} created", tempDirectory);
            return tempDirectory;
        }

        /// <summary>
        /// Shows Error style MessageBox to the user
        /// </summary>
        /// <param name="errorMsg"></param>
        private void ShowErrorDialog(string errorMsg)
        {
            AppSettings.Log.ErrorFormat("Displaying error dialog with error {0}", errorMsg);
            MessageBox.Show(errorMsg, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Unzips archive to specified destination directory
        /// </summary>
        /// <param name="archiveFileName"></param>
        /// <param name="destinationDirectory"></param>
        /// <returns></returns>
        private bool UnzipArchive(String archiveFileName, String destinationDirectory)
        {
            AppSettings.Log.DebugFormat("Unziping {0} to {1}", archiveFileName, destinationDirectory);
            try
            {
                using (Stream stream = File.OpenRead(archiveFileName))
                {
                    var reader = ReaderFactory.Open(stream);
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            reader.WriteEntryToDirectory(destinationDirectory, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                        }
                    }
                }
                AppSettings.Log.DebugFormat("Unziped sucessfully {0} to {1}", archiveFileName, destinationDirectory);
            }
            catch (Exception ex)
            {
                AppSettings.Log.ErrorFormat("Failed to unzip {0} to {1}", archiveFileName, destinationDirectory);
                AppSettings.Log.Error(ex);
                ShowErrorDialog(String.Format(Properties.Resources.ERROR_FAILED_UNZIP + "{0}{1}", Environment.NewLine, ex.Message));
                return false;
            }

            return true;

        }

        /// <summary>
        /// Adds content of specified folder to ZIP archive
        /// </summary>
        /// <param name="archiveFileName">Filename of the newly created ZIP archive</param>
        /// <param name="sourceDirectory">Directory with files to be zipped</param>
        /// <returns></returns>
        private bool ZipDirectory(String archiveFileName, String sourceDirectory)
        {
            AppSettings.Log.DebugFormat("Ziping {0} to {1}", sourceDirectory, archiveFileName);
            using (var archive = ZipArchive.Create())
            {
                AbstractWritableArchiveExtensions.AddAllFromDirectory(archive, sourceDirectory);
                var ci = new CompressionInfo();
                ci.DeflateCompressionLevel = CompressionLevel.Default;
                using (FileStream fs = new FileStream(archiveFileName, FileMode.Create))
                {
                    try
                    {
                        archive.SaveTo(fs, ci);
                    }
                    catch (Exception ex)
                    {
                        AppSettings.Log.ErrorFormat("Failed to zip {0} to {1}", sourceDirectory, archiveFileName);
                        AppSettings.Log.Error(ex);
                        throw new Exception(String.Format(Properties.Resources.ERROR_FAILED_ZIP + "{0}{1}", Environment.NewLine, ex.Message));
                    }
                }
            }
            AppSettings.Log.DebugFormat("Sucesfully zipped {0} to {1}", sourceDirectory, archiveFileName);
            return true;
        }

        /// <summary>
        /// Tries to delete directory, fails silently
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        private void TryDeleteDirectorySilent(String directoryPath)
        {
            try
            {
                AppSettings.Log.DebugFormat("Trying to silently delete directory {0}", directoryPath);
                Directory.Delete(directoryPath, true);
            }
            catch (Exception ex)
            {
                AppSettings.Log.Error("Silent directory delete failed.",ex);
            }
        }

        /// <summary>
        /// Updates controls with CurrentTask values thread safe
        /// </summary>
        private void UpdateControlsWithTaskInfo()
        {
            AppSettings.Log.Debug("UpdateControlsWithTaskInfo is initialized.");
            InvokeIfRequired(txtTaskDirectory, () =>
            {
                if (CurrentTask() != null)
                {
                    txtTaskDirectory.Text = CurrentTask().Directory;
                    txtTaskName.Text = CurrentTask().Name;
                    txtZipFileName.Text = CurrentTask().FileName;
                    ceZIP.Checked = CurrentTask().Zip;
                    ceDVBT.Checked = CurrentTask().DVBT;
                    ceDVBC.Checked = CurrentTask().DVBC;
                    ceStream.Checked = CurrentTask().Streams;
                }
                else
                {
                    txtTaskDirectory.Text = string.Empty;
                    txtTaskName.Text = string.Empty;
                    txtZipFileName.Text = string.Empty;
                    ceZIP.Checked = true;
                    ceDVBT.Checked = true;
                    ceDVBC.Checked = true;
                    ceStream.Checked = true;
                }

                //update selected satellite positions
                if (CurrentTask() != null && _satellites != null)
                {
                    foreach (var vmXmlSatellite in _satellites)
                    {
                        vmXmlSatellite.Selected = CurrentTask().Positions.Any(x => x == vmXmlSatellite.XmlSatellite.Position);
                    }
                }
                else
                {
                    if (_satellites != null)
                    {
                        foreach (var vmXmlSatellite in _satellites)
                        {
                            vmXmlSatellite.Selected = false;
                        }
                    }
                }
            });
            AppSettings.Log.Debug("UpdateControlsWithTaskInfo has finished.");
        }

        /// <summary>
        /// Updates task with data from controls (used when saving and adding new task)
        /// </summary>
        /// <param name="task"></param>
        private void UpdateTaskInfoFromControls(Task task)
        {

            AppSettings.Log.Debug("UpdateTaskInfoFromControls is initialized");
            //invoke grid validation to update current cell
            if (gridSatellites.CurrentCell != null)
            {
                gridSatellites.Enabled = false;
                gridSatellites.Enabled = true;
            }

            if (task != null)
            {
                task.Directory = txtTaskDirectory.Text;
                task.Name = txtTaskName.Text;
                task.FileName = txtZipFileName.Text;
                task.Zip = ceZIP.Checked;
                task.DVBT = ceDVBT.Checked;
                task.DVBC = ceDVBC.Checked;
                task.Streams = ceStream.Checked;

                if (_satellites != null)
                {
                    task.Positions =
                        _satellites.Where(x => x.Selected)
                            .OrderBy(x => Int32.Parse(x.XmlSatellite.Position))
                            .Select(x => x.XmlSatellite.Position.ToString(CultureInfo.InvariantCulture))
                            .ToArray();
                }
                else
                {
                    task.Positions = new string[] { };
                }
            }
            AppSettings.Log.Debug("UpdateTaskInfoFromControls has finished");
        }

        /// <summary>
        /// Sends a message to the receiver
        /// </summary>
        /// <param name="profile">Profile to send the message to</param>
        /// <param name="message">Message to be displayed to the user</param>
        /// <param name="timeout">Number of seconds message will be displayed</param>
        private bool SendMessage(Profile profile, string message, int timeout)
        {
            AppSettings.Log.DebugFormat("Sending message {0} to profile {1} with timeout {2}", message, profile.Name, timeout);
            try
            {
                string url = string.Empty;
                switch (profile.Enigma)
                {
                    case 1:
                        url = string.Format(@"http://{0}:{1}/cgi-bin/xmessage?timeout={2}&caption={3}&body={4}",
                            profile.Address, profile.Port, timeout, Uri.EscapeUriString(Application.ProductName),
                            Uri.EscapeUriString(message.Replace(" ", "+")));
                        break;
                    case 2:
                        url = string.Format(@"http://{0}:{1}/web/message?text={2}&type=2&timeout={3}",
                            profile.Address, profile.Port, Uri.EscapeUriString(message), timeout);
                        break;
                }
                MakeWebRequestToReceiver(profile, url, 15000);
                AppSettings.Log.DebugFormat("Message {0} sent successfully to profile {1}", message, profile.Name);
                return true;
            }
            catch (Exception ex)
            {
                AppSettings.Log.ErrorFormat("Failed to send message {0} to profile {1}", message, profile.Name);
                AppSettings.Log.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Initiates web request to receiver to reload settings
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private bool ReloadSettings(Profile profile)
        {
            AppSettings.Log.DebugFormat("Reloading settings for profile {0} with enigma type {1}", profile.Name, profile.Enigma);
            try
            {
                switch (profile.Enigma)
                {
                    case 1:
                        var url1 = string.Format(@"http://{0}:{1}/cgi-bin/reloadSettings", profile.Address, profile.Port);
                        MakeWebRequestToReceiver(profile, url1, 30000);
                        var url12 = string.Format(@"http://{0}:{1}/cgi-bin/reloadUserBouquets", profile.Address, profile.Port);
                        MakeWebRequestToReceiver(profile, url12, 30000);
                        break;
                    case 2:
                        string url2 = string.Format(@"http://{0}:{1}/web/servicelistreload?mode=0", profile.Address, profile.Port);
                        MakeWebRequestToReceiver(profile, url2, 30000);
                        break;
                }
                AppSettings.Log.DebugFormat("Successfully reloaded settings for profile {0} with enigma type {1}", profile.Name, profile.Enigma);
                return true;
            }
            catch (Exception ex)
            {
                AppSettings.Log.ErrorFormat("Failed to reload settings for profile {0} with enigma type {1}", profile.Name, profile.Enigma);
                AppSettings.Log.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// Makes HttpWebRequest to profile and returns response if sucessfull
        /// </summary>
        /// <param name="profile">Profile to connect to</param>
        /// <param name="url">URL to obtain</param>
        /// <param name="timeOut">Timeout for request in miliseconds</param>
        /// <returns></returns>
        private string MakeWebRequestToReceiver(Profile profile, string url, int timeOut)
        {
            if (profile == null)
                throw new ArgumentNullException("Profile");

            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            AppSettings.Log.DebugFormat("Making web request {0} for profile {1} with timeout {2}", url, profile.Name, timeOut);
            
            var request = (HttpWebRequest)WebRequest.Create(url);
            if (!string.IsNullOrEmpty(profile.PasswordDecrypted))
            {
                request.Credentials = new System.Net.NetworkCredential(profile.Username, profile.PasswordDecrypted);
            }
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            request.Timeout = timeOut;

            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();

            if (responseStream != null)
            {
                using (var reader = new StreamReader(responseStream))
                {                   
                    var result = reader.ReadToEnd();
                    AppSettings.Log.DebugFormat("Web request {0} for profile {1} returned {2}{3}", url, profile.Name, Environment.NewLine, result);
                    return result;
                }
            }

            return null;

        }

        #endregion

        #region "Buttons"

        /// <summary>
        /// Sets Enabled property on GUI controls based on application state
        /// Method is Thread safe
        /// </summary>
        private void UpdateControlsEnabled()
        {
            AppSettings.Log.Debug("UpdateControlsEnabled is initialized");
            InvokeIfRequired(toolStrip1, () =>
            {
                btnAddTask.Enabled = AddTaskEnabled();
                btnRemoveTask.Enabled = RemoveTaskEnabled();
                btnProcessTask.Enabled = ProcessTasksEnabled();
                btnSettings.Enabled = SettingsEnabled();
                btnZIP.Enabled = OpenZipEnabled();
                btnOpen.Enabled = OpenEnabled();
                btnUpload.Enabled = UploadEnabled();
                btnDownload.Enabled = DownloadEnabled();
                btnSaveTask.Enabled = SaveTaskEnabled();
                txtTaskDirectory.Enabled = TaskDirectoryEnabled();
                btnFolder.Enabled = TaskDirectoryEnabled();
                txtTaskName.Enabled = TaskNameEnabled();
                ceZIP.Enabled = ZipEnabled();
                txtZipFileName.Enabled = ZipFilenameEnabled();
                ceDVBT.Enabled = DVBTEnabled();
                ceDVBC.Enabled = DVBCEnabled();
                ceStream.Enabled = StreamsEnabled();
                gridTasks.Enabled = gridTasksEnabled();
                gridSatellites.Enabled = gridSatellitesEnabled();
            });
            AppSettings.Log.Debug("UpdateControlsEnabled has finished");
        }

        private void btnDownloadMenuItem_OnClick(object sender, EventArgs eventArgs)
        {
            if (sender.GetType() != typeof(ToolStripMenuItem))
                return;
            var profile = AppSettings.DefInstance.Profiles.ToList().Single(x => x.Name == ((ToolStripMenuItem)sender).Text);
            DownloadSettingsAsync(profile, DownloadSettingsCallback);
        }

        private void btnUploadMenuItem_OnClick(object sender, EventArgs eventArgs)
        {
            if (sender.GetType() != typeof(ToolStripMenuItem))
                return;
            var profile = AppSettings.DefInstance.Profiles.ToList().Single(x => x.Name == ((ToolStripMenuItem)sender).Text);
            UploadSettingsAsync(profile, UploadSettingsCallback);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var dlg = new OptionsDialog();
            dlg.Pages.Add(new GeneralOptionsPage());
            dlg.Pages.Add(new ProfilesOptionsPage());
            dlg.Pages.Add(new MoveOptionsPage(MoveOptionsPage.TransformType.Copy));
            dlg.Pages.Add(new MoveOptionsPage(MoveOptionsPage.TransformType.Move));
            AppSettings.DefInstance.BeginEdit();
            dlg.ShowDialog(this);
            AppSettings.DefInstance.CancelEdit();
            UpdateProfileList();
        }

        private void btnAddTask_Click(object sender, EventArgs e)
        {
            Task task = new Task();
            UpdateTaskInfoFromControls(task);
            if (task.Error.Length > 0)
            {
                ShowErrorDialog(task.Error);
                return;
            }

            AppSettings.DefInstance.Tasks.Add(task);
            AppSettings.Save();
            _bsTasks.MoveLast();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            About.DefInstance.ShowDialog();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = @"Enigma2 settings (lamedb)|lamedb|Enigma1 settings (services)|services";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofd.RestoreDirectory = true;
                ofd.Title = Properties.Resources.STATUS_SELECT_SERVICES;
                ofd.ValidateNames = true;

                //check if user has canceled the action
                if (ofd.ShowDialog() == DialogResult.Cancel)
                    return;
                OpenSettingsFileAsync(ofd.FileName);
            }
        }

        private void btnRemoveTask_Click(object sender, EventArgs e)
        {
            if (CurrentTask() != null)
            {
                if (MessageBox.Show(Properties.Resources.QUESTION_DELETE_TASK, CurrentTask().Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                    DialogResult.Yes)
                {
                    AppSettings.DefInstance.Tasks.Remove(CurrentTask());
                    AppSettings.Save();
                }
            }

        }

        private void btnSaveTask_Click(object sender, EventArgs e)
        {
            if (CurrentTask() != null)
            {
                CurrentTask().BeginEdit();
                UpdateTaskInfoFromControls(CurrentTask());

                if (CurrentTask().Error.Length > 0)
                {
                    ShowErrorDialog(CurrentTask().Error);
                    CurrentTask().CancelEdit();
                }
                else
                {
                    CurrentTask().EndEdit();
                    AppSettings.Save();
                }

            }
        }

        private void btnDonate_Click(object sender, EventArgs e)
        {
            String url =
                "https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=donations%40krkadoni%2ecom&lc=HR&item_name=Krkadoni%2ecom&item_number=Split%20This%20List%20application&currency_code=EUR&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted";
            System.Diagnostics.Process.Start(url);
        }

        private void btnZIP_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Filter = @"ZIP Archive (*.zip)|*.zip|RAR Archive (*.rar)|*.rar";
                ofd.CheckFileExists = true;
                ofd.CheckPathExists = true;
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofd.RestoreDirectory = true;
                ofd.Title = Properties.Resources.STATUS_SELECT_ARCHIVE;
                ofd.ValidateNames = true;

                //check if user has canceled the action
                if (ofd.ShowDialog() == DialogResult.Cancel)
                    return;

                //unzip archive to temporary folder
                var tempDir = GetTemporaryDirectory();
                if (!UnzipArchive(ofd.FileName, tempDir))
                    return;

                //try to find and load lamedb file
                var lamedbFiles = Directory.GetFiles(tempDir, "lamedb", SearchOption.AllDirectories);
                if (lamedbFiles.Any())
                {
                    OpenSettingsFileAsync(lamedbFiles[0]);
                    return;
                }

                //there is no lamedb file, try to find and load services file
                var servicesFiles = Directory.GetFiles(tempDir, "services", SearchOption.AllDirectories);
                if (servicesFiles.Any())
                {
                    OpenSettingsFileAsync(servicesFiles[0]);
                    return;
                }

                //there is no lamedb or services file inside archive, show error msg
                ShowErrorDialog(Properties.Resources.ERROR_INVALID_SERV_DATA);
            }

        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtTaskDirectory.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnProcessTask_Click(object sender, EventArgs e)
        {
            ProcessTasksAsync(AppSettings.DefInstance.Tasks.ToList(), ProcessTasksCallback);
        }

        private void btnForum_Click(object sender, EventArgs e)
        {
            String url = "http://www.krkadoni.com/log/forum";
            System.Diagnostics.Process.Start(url);
        }

        #endregion

        #region "Controls enabled"

        private bool IsBackgroundTaskRunning()
        {
            return isLoadingList || isSavingList || isFTPDownload || isFTPUpload;
        }

        private bool AddTaskEnabled()
        {
            if (_satellites == null || _satellites.Count == 0)
                return false;
            return !IsBackgroundTaskRunning();
        }

        private bool RemoveTaskEnabled()
        {
            if (CurrentTask() == null)
                return false;
            if (_satellites == null || _satellites.Count == 0)
                return false;
            return !IsBackgroundTaskRunning();
        }

        private bool ProcessTasksEnabled()
        {
            if (!AppSettings.DefInstance.Tasks.Any()) return false;
            if (_satellites == null || _satellites.Count == 0)
                return false;
            return !IsBackgroundTaskRunning();
        }

        private bool SettingsEnabled()
        {
            return !IsBackgroundTaskRunning();
        }

        private bool OpenZipEnabled()
        {
            return !IsBackgroundTaskRunning();
        }

        private bool OpenEnabled()
        {
            return !IsBackgroundTaskRunning();
        }

        private bool UploadEnabled()
        {
            if (_satellites == null || _satellites.Count == 0)
                return false;
            if (AppSettings.DefInstance.Profiles.Count == 0)
                return false;
            return !IsBackgroundTaskRunning();
        }

        private bool DownloadEnabled()
        {
            if (AppSettings.DefInstance.Profiles.Count == 0)
                return false;
            return !IsBackgroundTaskRunning();
        }

        private bool TaskDirectoryEnabled()
        {
            return !IsBackgroundTaskRunning();
        }

        private bool TaskNameEnabled()
        {
            return !IsBackgroundTaskRunning();
        }

        private bool ZipEnabled()
        {
            return !IsBackgroundTaskRunning();
        }

        private bool ZipFilenameEnabled()
        {
            return !IsBackgroundTaskRunning();
        }

        private bool DVBTEnabled()
        {
            return !IsBackgroundTaskRunning();
        }

        private bool DVBCEnabled()
        {
            return !IsBackgroundTaskRunning();
        }

        private bool StreamsEnabled()
        {
            return !IsBackgroundTaskRunning();
        }

        private bool SaveTaskEnabled()
        {
            if (_satellites == null || _satellites.Count == 0)
                return false;
            return !IsBackgroundTaskRunning();
        }

        private bool gridTasksEnabled()
        {
            if (_satellites == null || _satellites.Count == 0)
                return false;
            return !IsBackgroundTaskRunning();
        }

        private bool gridSatellitesEnabled()
        {
            if (_satellites == null || _satellites.Count == 0)
                return false;
            return !IsBackgroundTaskRunning();
        }

        #endregion

        #region "Load Settings"

        /// <summary>
        /// Starts loading settings file
        /// </summary>
        /// <param name="fileName"></param>
        private void OpenSettingsFileAsync(String fileName)
        {
            AppSettings.Log.DebugFormat("Initialized OpenSettingsFileAsync with filename {0}", fileName);
            isLoadingList = true;
            UpdateControlsEnabled();
            SetProgressBarVisible(true);
            SetStatus(Properties.Resources.STATUS_READING_SERVICES);
            EnigmaSettings.SettingsIO settingsIO = new SettingsIO();
            settingsIO.LoadAsync(new System.IO.FileInfo(fileName), OpenSettingsFileCallback);
            AppSettings.Log.DebugFormat("OpenSettingsFileAsync finished");
        }

        private void OpenSettingsFileCallback(IAsyncResult ar)
        {
            AppSettings.Log.Debug("OpenSettingsFileCallback initialized");

            isLoadingList = false;
            SetProgressBarVisible(false);
            SetStatus(string.Empty);

            // Retrieve the delegate.
            AsyncResult result = (AsyncResult)ar;
            var caller = (Func<FileInfo, ISettings>)result.AsyncDelegate;

            try
            {
                _satellites = new BindingList<VmXmlSatellite>();
                _settings = (EnigmaSettings.Settings)caller.EndInvoke(ar);
                foreach (var xmlSatellite in _settings.Satellites.OrderByDescending(x => Int32.Parse(x.Position)).ToList())
                {
                    _satellites.Add(new VmXmlSatellite(xmlSatellite));
                }

                //clean up if settings are loaded from archive
                if (_settings.SettingsDirectory.ToLower().StartsWith(Path.GetTempPath().ToLower()))
                {
                    TryDeleteDirectorySilent(_settings.SettingsDirectory);
                }
            }
            catch (Exception ex)
            {
                AppSettings.Log.Error(Properties.Resources.ERROR_FAILED_SERVICES, ex);
                ShowErrorDialog(String.Format(Properties.Resources.ERROR_FAILED_SERVICES + "{0}{1}", Environment.NewLine, ex.Message));
            }
            finally
            {
                SetGridDatasource(gridSatellites, _satellites);
                UpdateControlsWithTaskInfo();
                UpdateControlsEnabled();
            }
            AppSettings.Log.Debug("OpenSettingsFileCallback finished");
        }

        #endregion

        #region "Task Processing"

        /// <summary>
        /// Creates and writes settings for selected task
        /// </summary>
        /// <param name="task">Reference to existing task object</param>
        private void ProcessTask(Task task, Settings settings)
        {
            if (task == null)
                throw new ArgumentException("Task");

            if (settings == null)
                throw new ArgumentNullException("Settings");
            
            AppSettings.Log.DebugFormat("ProcessTask initialized for task {0} with positions {1}", task.Name, string.Join(",",task.Positions));

            SetStatus(Properties.Resources.STATUS_PROCESSING_TASK + @" " + task.Name);


            //remove and update satellites in settings for current task 
            ProcessSettingsPositions(settings, task);

            //if task result is ZIP archive use temp directory, otherwise use directory specified in task
            var destinationFolder = task.Directory;
            if (task.Zip)
                destinationFolder = GetTemporaryDirectory();

            //save settings
            EnigmaSettings.SettingsIO settingsIO = new SettingsIO();
            settingsIO.EditorName = "Generated by " + Application.ProductName + " ver. " + Application.ProductVersion + " - http://www.krkadoni.com";
            var st = new Stopwatch();
            AppSettings.Log.Debug("Calling SettingsIO.Save method");
            st.Start();
            settingsIO.Save(new DirectoryInfo(destinationFolder), settings);
            st.Stop();
            AppSettings.Log.DebugFormat("SettingsIO.Save method finished in {0} ms", st.ElapsedMilliseconds);
            //If task result is ZIP file create ZIP archive and clean up afterwards
            if (task.Zip)
            {
                var fileName = task.FileName;
                CultureInfo ci = CultureInfo.InvariantCulture;

                if (fileName.IndexOf("%d", System.StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    fileName = fileName.Replace("%d", DateTime.Now.ToString("dd.MM.yyyy", ci));
                }
                if (fileName.IndexOf("%D", System.StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    fileName = fileName.Replace("%D", DateTime.Now.ToString("dd.MM.yyyy", ci));
                }

                var enigmaVersion = (settings.SettingsVersion == Enums.SettingsVersion.Enigma2Ver3 |
                                     settings.SettingsVersion == Enums.SettingsVersion.Enigma2Ver4)
                    ? "E2"
                    : "E1";

                if (fileName.IndexOf("%e", System.StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    fileName = fileName.Replace("%e", enigmaVersion);
                }
                if (fileName.IndexOf("%E", System.StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    fileName = fileName.Replace("%E", enigmaVersion);
                }

                var zipFileName = Path.Combine(task.Directory, fileName);

                if (!ZipDirectory(zipFileName, destinationFolder))
                    return;
                TryDeleteDirectorySilent(destinationFolder);
            }
            AppSettings.Log.DebugFormat("ProcessTask for task {0} was successfull", task.Name);
        }

        /// <summary>
        /// Process all tasks in the list
        /// </summary>
        /// <param name="tasks"></param>
        private void ProcessTasks(List<Task> tasks)
        {
            AppSettings.Log.DebugFormat("ProcessTasks is initialized with {0} tasks", tasks.Count);
            foreach (var task in tasks)
            {
                ProcessTask(task, CloneSettings());
            }
            AppSettings.Log.DebugFormat("ProcessTasks has processed {0} tasks", tasks.Count);
        }

        /// <summary>
        /// Process all tasks in the list asynchronusly
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="callback"></param>
        public void ProcessTasksAsync(List<Task> tasks, AsyncCallback callback)
        {
            AppSettings.Log.DebugFormat("ProcessTasksAsync is initialized with {0} tasks", tasks.Count);
            isSavingList = true;
            UpdateControlsEnabled();
            SetProgressBarVisible(true);
            new Action<List<Task>>(ProcessTasks).BeginInvoke(tasks, callback, null);
            AppSettings.Log.DebugFormat("ProcessTasksAsync finished");
        }

        private void ProcessTasksCallback(IAsyncResult ar)
        {
            AppSettings.Log.Debug("ProcessTasksCallback initialized");
            isSavingList = false;
            SetProgressBarVisible(false);

            // Retrieve the delegate.
            AsyncResult result = (AsyncResult)ar;
            var caller = (Action<List<Task>>)result.AsyncDelegate;

            try
            {
                caller.EndInvoke(ar);
            }
            catch (SettingsException ex)
            {
                AppSettings.Log.ErrorFormat("ProcessTasksCallback invoked SettingsException");
                AppSettings.Log.Error(ex);
                string message;
                if (ex.InnerException != null)
                {
                    message = ex.Message + Environment.NewLine + ex.InnerException.Message;
                }
                else
                {
                    message = ex.Message;
                }
                ShowErrorDialog(String.Format(Properties.Resources.ERROR_FAILED_SERVICES + "{0}{1}", Environment.NewLine, message));
            }
            catch (Exception ex)
            {
                AppSettings.Log.ErrorFormat("ProcessTasksCallback invoked Exception");
                AppSettings.Log.Error(ex);
                ShowErrorDialog(String.Format(Properties.Resources.ERROR_FAILED_SERVICES + "{0}{1}", Environment.NewLine, ex.Message));
            }
            finally
            {
                SetStatus(string.Empty);
                UpdateControlsEnabled();
            }
            AppSettings.Log.Debug("ProcessTasksCallback finished");
        }

        /// <summary>
        /// Clones current settings into new instance of Settings
        /// </summary>
        /// <returns>Return null if current settings are empty</returns>
        private EnigmaSettings.Settings CloneSettings()
        {
            if (_settings == null) return null;
            var st = new Stopwatch();
            AppSettings.Log.DebugFormat("CloneSettings initialized for {0} services, {1} transponders and {2} satellites", _settings.Services, _settings.Transponders, _settings.Satellites);
            st.Start();
            var settings = ObjectCopier.Clone(_settings);
            st.Stop();
            AppSettings.Log.DebugFormat("CloneSettings finished in {0} ms", st.ElapsedMilliseconds);
            return settings;
        }

        /// <summary>
        /// Deletes satellites not specifed in task positions, copies, moves and cleans settings
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        private static void ProcessSettingsPositions(EnigmaSettings.Settings settings, Task task)
        {
            if (settings != null && task != null)
            {
                AppSettings.Log.DebugFormat("ProcessSettingsPositions was initialized for task {0}", task);

                //delete satellites not selected in current task
                DeleteSatellites(settings, task.Positions);

                //copy satellites
                CopySatellites(settings, AppSettings.DefInstance.CopyList.ToList());

                //move satellites
                MoveSatellites(settings, AppSettings.DefInstance.MoveList.ToList());

                //remove cable services if not selected
                if (!task.DVBC)
                {
                    AppSettings.Log.DebugFormat("Removing cable transponders and services for task {0}", task);
                    List<ITransponder> dvbcTransponders = new List<ITransponder>(settings.Transponders.OfType<ITransponderDVBC>().Select(x => (ITransponder)x).ToList());
                    settings.RemoveTransponders(dvbcTransponders);
                }

                //remove terrestrial services if not selected
                if (!task.DVBT)
                {
                    AppSettings.Log.DebugFormat("Removing terrestrial transponders and services for task {0}", task);
                    List<ITransponder> dvbtTransponders = new List<ITransponder>(settings.Transponders.OfType<ITransponderDVBT>().Select(x => (ITransponder)x).ToList());
                    settings.RemoveTransponders(dvbtTransponders);
                }

                //remove stream services if not selected
                if (!task.Streams)
                {
                    AppSettings.Log.DebugFormat("Removing stream services for task {0}", task);
                    settings.RemoveStreams();
                }

                AppSettings.Log.DebugFormat("Doing cleanup for task {0}", task);
                //cleanup
                settings.RemoveServicesWithoutTransponder();
                settings.RemoveInvalidBouquetItems();
                settings.RemoveEmptyMarkers(x => AppSettings.DefInstance.Delimiters.Count(e => x.Description.ToLower().Contains(e)) > 0);
                settings.RemoveEmptyBouquets();
                settings.RenumberBouquetFileNames();
                settings.RenumberMarkers();
                AppSettings.Log.DebugFormat("ProcessSettingsPositions finished successfully for task {0}", task);
            }
        }

        /// <summary>
        /// Removes satellites not selected in task from settings
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="positions"></param>
        private static void DeleteSatellites(EnigmaSettings.Settings settings, string[] positions)
        {
            if (settings != null && positions != null)
            {
                AppSettings.Log.DebugFormat("DeleteSatellites initialized with positions {0}", string.Join(",",positions));
                var positionsToDelete = settings.Satellites.Where(x => !positions.Contains(x.Position)).Select(x => x.Position).ToList();
                foreach (var position in positionsToDelete)
                {
                    //removing satellite from settings removes it from satellites.xml
                    //settings.RemoveSatellite(settings.Satellites.Single(x => x.Position == position));

                    //remove services and transponders from settings, leave satellites.xml as is
                    var transponders = settings.Transponders.OfType<ITransponderDVBS>().Where(x => x.OrbitalPositionInt == Int32.Parse(position)).Select(x => (ITransponder)(x)).ToList();
                    AppSettings.Log.DebugFormat("Deleting {0} transponders for satellite position {1}", transponders.Count, position);
                    settings.RemoveTransponders(transponders);
                    AppSettings.Log.DebugFormat("{0} transponders deleted from satellite position {1}", transponders.Count, position);
                }
                AppSettings.Log.DebugFormat("DeleteSatellites finished sucessfully for positions {0}", string.Join(",", positions));
            }
        }

        /// <summary>
        /// Creates copies of satellites defined by copy list
        /// </summary>
        /// <param name="settings">Existing Settings reference</param>
        /// <param name="copyList">List of all positions that should be copied</param>
        /// <remarks>Does not touch bouquets in any way</remarks>
        private static void CopySatellites(EnigmaSettings.Settings settings, List<PositionTransform> copyList)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            if (copyList == null)
                throw new ArgumentNullException("copyList");

            // copy satellites
            foreach (var positionTransform in copyList)
            {
                AppSettings.Log.DebugFormat("CopySatellites initialized with positions {0}", string.Join(",", copyList.Select(x => x.Display).ToArray()));

                //find satellite to be copied
                var existingSat =
                    settings.Satellites.FirstOrDefault(x =>
                        x.Position == positionTransform.OriginalPosition.ToString(CultureInfo.InvariantCulture));

                //make sure destination sat doesn't already exists
                var destinationSat =
                    settings.Satellites.FirstOrDefault(x =>
                        x.Position == positionTransform.Destination.ToString(CultureInfo.InvariantCulture));

                if (existingSat != null && destinationSat == null)
                {
                    AppSettings.Log.DebugFormat("Copying satellite {0} to {1}", existingSat.Position, positionTransform.Destination);
                    //create temporary list to hold transponders to be added 
                    var newTransponders = new List<ITransponderDVBS>();

                    //create temporary list to hold services to be added
                    var newServices = new List<IService>();

                    //create new satellite as a shallow copy of existing
                    var newSat = existingSat.ShallowCopy();
                    newSat.Name = existingSat.Name;
                    newSat.Position = positionTransform.Destination.ToString(CultureInfo.InvariantCulture);


                    //go trough the list of transponders for current satellite
                    var existingSatTransponders = settings.FindTranspondersForSatellite(existingSat);
                    foreach (var existingSatTransponder in existingSatTransponders)
                    {
                        //create new instance of transponder as a copy of existing transponder
                        var newSatTransponder = (ITransponderDVBS)existingSatTransponder.ShallowCopy();

                        //update position properties
                        newSatTransponder.OrbitalPositionInt = positionTransform.Destination;
                        newSatTransponder.NameSpc = newSatTransponder.CalculatedNameSpace;

                        //add new transponder to temporary list
                        newTransponders.Add(newSatTransponder);

                        //go trough the list of services for current transponder
                        var existingServices = settings.FindServicesForTransponder(existingSatTransponder);
                        foreach (var existingService in existingServices)
                        {
                            //create new service as a shallow copy of existing
                            var newService = existingService.ShallowCopy();

                            //update transponder reference for new service
                            newService.Transponder = newSatTransponder;

                            //add new service to temporary list
                            newServices.Add(newService);
                        }
                    }

                    //finished cloning current satellite with transponders and services
                    //add newly created objects from temporary lists to settings
                    settings.Satellites.Add(newSat);
                    newTransponders.ForEach(x => settings.Transponders.Add(x));
                    newServices.ForEach(x => settings.Services.Add(x));
                    AppSettings.Log.DebugFormat("Satellite {0} coppied to {1} successfully", existingSat.Position, positionTransform.Destination);
                }
                AppSettings.Log.DebugFormat("CopySatellites finished for positions {0}", string.Join(",", copyList.Select(x => x.Display).ToArray()));
            }
        }

        /// <summary>
        /// Moves satellites to new positions as defined by move list
        /// </summary>
        /// <param name="settings">Existing Settings reference</param>
        /// <param name="moveList">List of all positions that should be moved</param>
        /// <remarks>Does not touch bouquets in any way</remarks>
        private static void MoveSatellites(EnigmaSettings.Settings settings, List<PositionTransform> moveList)
        {
            if (settings != null && moveList != null)
            {
                AppSettings.Log.DebugFormat("MoveSatellites initialized with positions {0}", string.Join(",", moveList.Select(x => x.Display).ToArray()));
                // move satellites by first cloning then deleting existing ones
                foreach (var positionTransform in moveList)
                {
                    //find satellite to be copied
                    var existingSat =
                        settings.Satellites.FirstOrDefault(x =>
                            x.Position == positionTransform.OriginalPosition.ToString(CultureInfo.InvariantCulture));

                    //make sure destination sat doesn't already exists
                    var destinationSat =
                        settings.Satellites.FirstOrDefault(x =>
                            x.Position == positionTransform.Destination.ToString(CultureInfo.InvariantCulture));

                    if (existingSat != null && destinationSat == null)
                    {
                        AppSettings.Log.DebugFormat("Moving satellite {0} to {1}", existingSat.Position, positionTransform.Destination);
                        settings.ChangeSatellitePosition(existingSat, positionTransform.Destination);
                        AppSettings.Log.DebugFormat("Satellite {0} moved to {1} successfully", existingSat.Position, positionTransform.Destination);
                    }
                }
                AppSettings.Log.DebugFormat("MoveSatellites finished for positions {0}", string.Join(",", moveList.Select(x => x.Display).ToArray()));
            }
        }

        #endregion

        #region "Menu Items"

        private void SelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var vmXmlSatellite in _satellites)
            {
                vmXmlSatellite.Selected = true;
            }
        }

        private void SelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridSatellites.CurrentRow != null)
            {
                var vmSatellite = (VmXmlSatellite)gridSatellites.CurrentRow.DataBoundItem;
                vmSatellite.Selected = true;
            }
        }

        private void DeselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var vmXmlSatellite in _satellites)
            {
                vmXmlSatellite.Selected = false;
            }
        }

        private void DeselectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (gridSatellites.CurrentRow != null)
            {
                var vmSatellite = (VmXmlSatellite)gridSatellites.CurrentRow.DataBoundItem;
                vmSatellite.Selected = false;
            }
        }

        private void ProcessTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tasks = new List<Task>();
            foreach (DataGridViewRow selectedRow in gridTasks.SelectedRows)
            {
                tasks.Add((Task)selectedRow.DataBoundItem);
            }
            if (tasks.Count > 0)
                ProcessTasksAsync(tasks, ProcessTasksCallback);
        }

        private void ProcessAllTasksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProcessTasksAsync(AppSettings.DefInstance.Tasks.ToList(), ProcessTasksCallback);
        }

        private void RemoveTaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnRemoveTask_Click(sender, e);
        }

        #endregion

        #region "GUI Thread Safe Update"

        /// <summary>
        /// Checks if action requires invoking, invokes if neccessary and runs action
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        private static void InvokeIfRequired(Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        /// <summary>
        /// Update status text thread safe
        /// </summary>
        /// <param name="status"></param>
        private void SetStatus(string status)
        {
            InvokeIfRequired(statusStrip1, () =>
            {
                if (!string.IsNullOrEmpty(status))
                {
                    Status.Text = status;
                }
                else
                {
                    Status.Text = @"http://www.krkadoni.com";
                }
            });
        }

        /// <summary>
        /// Update progress bar visibillity thread safe
        /// </summary>
        /// <param name="visible"></param>
        private void SetProgressBarVisible(bool visible)
        {
            InvokeIfRequired(statusStrip1, () =>
            {
                ProgressBar.Visible = visible;
            });
        }

        /// <summary>
        /// Sets grid datasource thread safe
        /// </summary>
        /// <param name="gridView"></param>
        /// <param name="source"></param>
        private void SetGridDatasource(DataGridView gridView, Object source)
        {
            if (gridView != null)
            {
                InvokeIfRequired(gridView, () =>
                {
                    gridView.DataSource = source;
                });
            }
        }

        #endregion

        #region "FTP Download"

        /// <summary>
        /// Downloads settings from receiver and loads into memory
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>Returns full path to settings file on disk</returns>
        private string DownloadSettings(Profile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            AppSettings.Log.DebugFormat("DownloadSettings initialized for profile {0}", profile.Name);

            // create a new FtpClient object with the host and port number to use
            using (var ftp = new FtpClient(profile.Address, profile.FTPPort))
            {
                // specify a binary, passive connection with no zlib file compression 
                ftp.FileTransferType = TransferType.Binary;
                ftp.DataTransferMode = TransferMode.Passive;

                AppSettings.Log.Debug("Opening FTP connection");
                // open a connection to the ftp server 
                ftp.Open(profile.Username, profile.PasswordDecrypted);
                AppSettings.Log.Debug("FTP connection opened");

                SetStatus(Properties.Resources.STATUS_CONNECTED_TO + " " + profile.Name);

                var destinationFolder = GetTemporaryDirectory();

                //send message to receiver
                SendMessage(profile, String.Format("Please wait while {0} is reading your settings...", Uri.EscapeUriString(Application.ProductName)), 4);

                AppSettings.Log.DebugFormat("Changing remote FTP folder to {0}",profile.SatellitesFolder);
                ftp.ChangeDirectory(profile.SatellitesFolder);
                AppSettings.Log.DebugFormat("Changed remote FTP folder to {0}", profile.SatellitesFolder);
                AppSettings.Log.DebugFormat("Listing directory {0}", profile.SatellitesFolder);
                FtpItemCollection satCol = ftp.GetDirList();
                AppSettings.Log.DebugFormat("Directory {0} listed with {1} items", profile.SatellitesFolder, satCol.Count);

                foreach (var ftpItem in satCol)
                {
                    if (ftpItem.Name.ToLower() == "satellites.xml")
                    {
                        AppSettings.Log.DebugFormat("Starting FTP download of {0} to {1}", ftpItem.Name, Path.Combine(destinationFolder, "satellites.xml"));
                        SetStatus(Properties.Resources.DOWNLOADING + " " + ftpItem.Name);
                        ftp.GetFile(ftpItem.Name, Path.Combine(destinationFolder, "satellites.xml"));
                        AppSettings.Log.DebugFormat("File {0} was downloaded to {1} successfully", ftpItem.Name, Path.Combine(destinationFolder, "satellites.xml"));
                    }
                }

                // change to the settings directory on the ftp server 
                AppSettings.Log.DebugFormat("Changing remote FTP folder to {0}", profile.ServicesFolder);
                ftp.ChangeDirectory(profile.ServicesFolder);
                AppSettings.Log.DebugFormat("Changed remote FTP folder to {0}", profile.ServicesFolder);

                // retrieve a listing of the files in the directory 'files'
                // as a collection of FtpItem objects
                AppSettings.Log.DebugFormat("Listing directory {0}", profile.ServicesFolder);
                FtpItemCollection col = ftp.GetDirList();
                AppSettings.Log.DebugFormat("Directory {0} listed with {1} items", profile.ServicesFolder, col.Count);

                foreach (var ftpItem in col)
                {
                    if (
                        ftpItem.Name.ToLower().EndsWith(".tv")
                        || ftpItem.Name.ToLower().EndsWith(".radio")
                        || ftpItem.Name == "services"
                        || ftpItem.Name == "lamedb"
                        || ftpItem.Name == "bouquets"
                        || ftpItem.Name == "services.locked"
                        || ftpItem.Name == "whitelist"
                        || ftpItem.Name == "blacklist"
                        )
                    {
                        AppSettings.Log.DebugFormat("Starting FTP download of {0} to {1}", ftpItem.Name, Path.Combine(destinationFolder, ftpItem.Name));
                        SetStatus(Properties.Resources.DOWNLOADING + " " + ftpItem.Name);
                        ftp.GetFile(ftpItem.FullPath, Path.Combine(destinationFolder, ftpItem.Name));
                        AppSettings.Log.DebugFormat("File {0} was downloaded to {1} successfully", ftpItem.Name, Path.Combine(destinationFolder, ftpItem.Name));
                    }
                }

                // close connection to the ftp server 
                ftp.Close();

                AppSettings.Log.Debug("FTP download successfull");

                //send information to receiver
                SendMessage(profile, "Operation completed!", 4);

                AppSettings.Log.DebugFormat("DownloadSettings finished for profile {0}", profile.Name);

                if (profile.Enigma == 2 && File.Exists(Path.Combine(destinationFolder, "lamedb")))
                    return Path.Combine(destinationFolder, "lamedb");

                if (profile.Enigma == 1 && File.Exists(Path.Combine(destinationFolder, "services")))
                    return Path.Combine(destinationFolder, "services");

                return null;

            }

        }

        /// <summary>
        /// Downloads settings from receiver via FTP and loads into memory
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="callback"></param>
        private void DownloadSettingsAsync(Profile profile, AsyncCallback callback)
        {
            AppSettings.Log.DebugFormat("DownloadSettingsAsync is initialized for profile {0}", profile.Name);
            isFTPDownload = true;
            UpdateControlsEnabled();
            SetProgressBarVisible(true);
            SetStatus(Properties.Resources.STATUS_CONNECTING_TO + " " + profile.Name);
            new Func<Profile, string>(DownloadSettings).BeginInvoke(profile, callback, null);
            AppSettings.Log.Debug("DownloadSettingsAsync finished");
        }

        private void DownloadSettingsCallback(IAsyncResult ar)
        {
            AppSettings.Log.Debug("DownloadSettingsCallback initialized");
            isFTPDownload = false;
            SetProgressBarVisible(false);
            SetStatus(string.Empty);

            // Retrieve the delegate.
            AsyncResult result = (AsyncResult)ar;
            var caller = (Func<Profile, string>)result.AsyncDelegate;

            try
            {
                var fileName = caller.EndInvoke(ar);
                if (fileName != null)
                {
                    OpenSettingsFileAsync(fileName);
                }
            }
            catch (SettingsException ex)
            {
                AppSettings.Log.ErrorFormat("DownloadSettingsCallback invoked SettingsException");
                AppSettings.Log.Error(ex);
                string message;
                if (ex.InnerException != null)
                {
                    message = ex.Message + Environment.NewLine + ex.InnerException.Message;
                }
                else
                {
                    message = ex.Message;
                }
                ShowErrorDialog(String.Format(Properties.Resources.ERROR_DOWNLOADING + "{0}{1}", Environment.NewLine, message));
            }
            catch (Exception ex)
            {
                AppSettings.Log.ErrorFormat("DownloadSettingsCallback invoked Exception");
                AppSettings.Log.Error(ex);
                ShowErrorDialog(String.Format(Properties.Resources.ERROR_DOWNLOADING + "{0}{1}", Environment.NewLine, ex.Message));
            }
            finally
            {
                UpdateControlsEnabled();
            }
            AppSettings.Log.Debug("DownloadSettingsCallback finished");
        }

        #endregion

        #region "FTP Upload"

        /// <summary>
        /// Upload all files from settings folder to specified profile via ftp
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="settingsFolder"></param>
        private void UploadSettingsViaFTP(Profile profile, String settingsFolder)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            if (string.IsNullOrEmpty(settingsFolder) || !Directory.Exists(settingsFolder))
                return;
            AppSettings.Log.DebugFormat("UploadSettingsViaFTP initialized for profile {0} and folder {1}", profile.Name, settingsFolder);

            // create a new FtpClient object with the host and port number to use
            using (var ftp = new FtpClient(profile.Address, profile.FTPPort))
            {
                // specify a binary, passive connection with no zlib file compression 
                ftp.FileTransferType = TransferType.Binary;
                ftp.DataTransferMode = TransferMode.Passive;

                AppSettings.Log.Debug("Opening FTP connection");
                // open a connection to the ftp server 
                ftp.Open(profile.Username, profile.PasswordDecrypted);
                AppSettings.Log.Debug("FTP connection opened");

                SetStatus(Properties.Resources.STATUS_CONNECTED_TO + " " + profile.Name);

                //send message to receiver
                SendMessage(profile, String.Format("Please wait while {0} is writing your settings...", Uri.EscapeUriString(Application.ProductName)), 4);

                AppSettings.Log.DebugFormat("Changing remote FTP folder to {0}", profile.SatellitesFolder);
                ftp.ChangeDirectory(profile.SatellitesFolder);
                AppSettings.Log.DebugFormat("Changed remote FTP folder to {0}", profile.SatellitesFolder);

                //remove existing satellites.xml
                AppSettings.Log.DebugFormat("Listing directory {0}", profile.SatellitesFolder);
                FtpItemCollection satCol = ftp.GetDirList();
                AppSettings.Log.DebugFormat("Directory {0} listed with {1} items", profile.SatellitesFolder, satCol.Count);

                foreach (var ftpItem in satCol)
                {
                    if (ftpItem.Name.ToLower() == "satellites.xml")
                    {
                        AppSettings.Log.DebugFormat("Deleting remote file {0}", ftpItem.FullPath);
                        SetStatus(Properties.Resources.DELETING + " " + ftpItem.Name);
                        ftp.DeleteFile(ftpItem.Name);
                        AppSettings.Log.DebugFormat("Deleted remote file {0}", ftpItem.FullPath);
                    }
                }

                //upload new one
                SetStatus(Properties.Resources.UPLOADING + " " + "satellites.xml");
                AppSettings.Log.DebugFormat("Starting FTP upload of {0}", Path.Combine(settingsFolder, "satellites.xml"));
                ftp.PutFile(Path.Combine(settingsFolder, "satellites.xml"));
                AppSettings.Log.DebugFormat("File {0} uploaded successfully", Path.Combine(settingsFolder, "satellites.xml"));

                // change to the settings directory on the ftp server 
                AppSettings.Log.DebugFormat("Changing remote FTP folder to {0}", profile.ServicesFolder);
                ftp.ChangeDirectory(profile.ServicesFolder);
                AppSettings.Log.DebugFormat("Changed remote FTP folder to {0}", profile.ServicesFolder);

                // retrieve a listing of the files in the directory 'files'
                // as a collection of FtpItem objects
                AppSettings.Log.DebugFormat("Listing directory {0}", profile.ServicesFolder);
                FtpItemCollection col = ftp.GetDirList();
                AppSettings.Log.DebugFormat("Directory {0} listed with {1} items", profile.ServicesFolder, col.Count);

                //delete existing settings
                foreach (var ftpItem in col)
                {
                    if (
                        ftpItem.Name.ToLower().EndsWith(".tv")
                        || ftpItem.Name.ToLower().EndsWith(".radio")
                        || ftpItem.Name == "services"
                        || ftpItem.Name == "lamedb"
                        || ftpItem.Name == "bouquets"
                        || ftpItem.Name == "services.locked"
                        || ftpItem.Name == "whitelist"
                        || ftpItem.Name == "blacklist"
                        )
                    {
                        AppSettings.Log.DebugFormat("Deleting remote file {0}", ftpItem.FullPath);
                        SetStatus(Properties.Resources.DELETING + " " + ftpItem.Name);
                        ftp.DeleteFile(ftpItem.FullPath);
                        AppSettings.Log.DebugFormat("Deleted remote file {0}", ftpItem.FullPath);
                    }
                }

                //transfer new settings
                var di = new DirectoryInfo(settingsFolder);
                foreach (var fileInfo in di.GetFiles())
                {
                    if (fileInfo.Name.ToLower() != "satellites.xml")
                    {
                        AppSettings.Log.DebugFormat("Starting FTP upload of {0}", fileInfo.FullName);
                        SetStatus(Properties.Resources.UPLOADING + " " + fileInfo.Name);
                        ftp.PutFile(fileInfo.FullName);
                        AppSettings.Log.DebugFormat("File {0} uploaded successfully", fileInfo.FullName);
                    }
                }

                // close connection to the ftp server 
                ftp.Close();

                AppSettings.Log.Debug("FTP upload successfull");
                AppSettings.Log.DebugFormat("UploadSettingsViaFTP finished for profile {0}", profile.Name);
            }
        }

        /// <summary>
        /// Creates task from currently selected satellites, processes settings, does FTP upload, and reloads settings on receiver
        /// </summary>
        /// <param name="profile"></param>
        /// <returns></returns>
        private Profile UploadSettings(Profile profile)
        {
            if (profile == null)
                throw new ArgumentNullException("profile");

            if (_satellites == null)
                throw new ArgumentNullException("Satellites");

            if (_settings == null)
                throw new ArgumentNullException("Settings");

            AppSettings.Log.DebugFormat("UploadSettings initialized for profile {0}", profile.Name);

            var task = new Task();
            task.Positions = _satellites.Where(x => x.Selected)
                    .OrderBy(x => Int32.Parse(x.XmlSatellite.Position))
                    .Select(x => x.XmlSatellite.Position.ToString(CultureInfo.InvariantCulture))
                    .ToArray();
            task.DVBC = ceDVBC.Checked;
            task.DVBT = ceDVBT.Checked;
            task.Directory = GetTemporaryDirectory();
            task.Name = profile.Name;
            task.Streams = ceStream.Checked;
            task.Zip = false;

            //create new settings instance to preserve original
            var settings = CloneSettings();

            //make sure settings type matches receiver type
            settings.SettingsVersion = profile.Enigma == 2 ? Enums.SettingsVersion.Enigma2Ver4 : Enums.SettingsVersion.Enigma1V1;

            ProcessTask(task, settings);
            UploadSettingsViaFTP(profile, task.Directory);
            ReloadSettings(profile);
            SendMessage(profile, "Operation completed!", 4);

            AppSettings.Log.DebugFormat("UploadSettings finished for profile {0}", profile.Name);
            return profile;

        }

        private void UploadSettingsAsync(Profile profile, AsyncCallback callback)
        {
            AppSettings.Log.DebugFormat("UploadSettingsAsync is initialized for profile {0}", profile.Name);
            isFTPUpload = true;
            UpdateControlsEnabled();
            SetProgressBarVisible(true);
            new Func<Profile, Profile>(UploadSettings).BeginInvoke(profile, callback, null);
            AppSettings.Log.Debug("UploadSettingsAsync finished");
        }

        private void UploadSettingsCallback(IAsyncResult ar)
        {
            AppSettings.Log.Debug("UploadSettingsCallback initialized");
            isFTPUpload = false;
            SetProgressBarVisible(false);
            SetStatus(string.Empty);

            // Retrieve the delegate.
            AsyncResult result = (AsyncResult)ar;
            var caller = (Func<Profile, Profile>)result.AsyncDelegate;

            try
            {
                var profile = (Profile)caller.EndInvoke(ar);

            }
            catch (SettingsException ex)
            {
                AppSettings.Log.ErrorFormat("UploadSettingsCallback invoked SettingsException");
                AppSettings.Log.Error(ex);
                string message;
                if (ex.InnerException != null)
                {
                    message = ex.Message + Environment.NewLine + ex.InnerException.Message;
                }
                else
                {
                    message = ex.Message;
                }
                ShowErrorDialog(String.Format(Properties.Resources.ERROR_UPLOADING + "{0}{1}", Environment.NewLine, message));
            }
            catch (Exception ex)
            {
                AppSettings.Log.ErrorFormat("UploadSettingsCallback invoked Exception");
                AppSettings.Log.Error(ex);
                ShowErrorDialog(String.Format(Properties.Resources.ERROR_UPLOADING + "{0}{1}", Environment.NewLine, ex.Message));
            }
            finally
            {
                UpdateControlsWithTaskInfo();
                UpdateControlsEnabled();
            }
            AppSettings.Log.Debug("UploadSettingsCallback finished");
        }

        #endregion

    }
}
