using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Updater
{
    public partial class UpdateForm : Form
    {
        public string Status { get; set; }
        public bool Completed { get; set; }

        private class NewUpdate
        {
            public string Status { get; set; }
            public bool Completed { get; set; }
            public string RootFolderName { get; set; }
            public SourceForge.SourceForgeFolder InstallerFile { get; set; }
            public bool NewUpdatesAreAvailable { get; set; }

            public void Check_ForUpdates()
            {
                try
                {
                    //Check internet access
                    Network myNetwork = new Network();
                    if (!myNetwork.Check_InternetAccess())
                    {
                        Status = "No internet access!";
                        Completed = true;
                        return;
                    }

                    //Search for new updates on SourceForge
                    Status = "Checking for new updates...";
                    SourceForge mySourceForge = new SourceForge(myUpdater.UpdaterConfigurator.UpdateUrl);
                    //Null version
                    if (mySourceForge.VerMajor == 0 && mySourceForge.VerMinor == 0 && mySourceForge.VerBuild == 0 && mySourceForge.VerRevision == 0)
                    {
                        Status = "The online database is not ready for update!";
                        Completed = true;
                        return;
                    }
                    //Same version check
                    if (mySourceForge.VerMajor == myUpdater.UpdaterConfigurator.VerMajor && mySourceForge.VerMinor == myUpdater.UpdaterConfigurator.VerMinor &&
                        mySourceForge.VerBuild == myUpdater.UpdaterConfigurator.VerBuild && mySourceForge.VerRevision == myUpdater.UpdaterConfigurator.VerRevision)
                    {
                        Status = "Your software is up to date!";
                        Completed = true;
                        return;
                    }

                    //Check if installer .msi file is placed in root folder
                    bool FoundInstaller = false;
                    RootFolderName = mySourceForge.RootFolderName;
                    List<SourceForge.SourceForgeFolder> root_folder_files = new List<SourceForge.SourceForgeFolder>();
                    mySourceForge.Get_Folders(myUpdater.UpdaterConfigurator.UpdateUrl + RootFolderName + "/", ref root_folder_files, false, true);
                    foreach (SourceForge.SourceForgeFolder file in root_folder_files)
                        if (!file.IsFolder && file.Url.ToLower().Contains(".msi"))
                        {
                            InstallerFile = file;
                            FoundInstaller = true;
                        }
                    if (!FoundInstaller)
                    {
                        Status = "The online database is not ready for update!";
                        Completed = true;
                        return;
                    }

                    NewUpdatesAreAvailable = true;
                    Status = "";
                    Completed = true;
                }
                catch (Exception e)
                {
                    Completed = true;
                    Status = e.Message;
                }
            }
        }

        private bool FormIsOpen = false;
        private bool DownloadConfirmed = false;
        private bool DestinationIsSet = false;

        private static Updater myUpdater;
        private NewUpdate myNewUpdate;
        private Thread myThread;

        public UpdateForm(Updater updater)
        {
            InitializeComponent();

            myUpdater = updater;
            lblTitle.Text = myUpdater.UpdaterConfigurator.ApplicationName;
            tmrUpdate.Interval = ConvertMinute2Milliseconds(updater.UpdaterConfigurator.UpdateInterval);

            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width - 5,
                                      workingArea.Bottom - Size.Height);
        }
        public void Start_Update()
        {
            tmrUpdate.Enabled = true;
        }
        public void Stop_Update()
        {
            tmrUpdate.Enabled = false;
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            Check_ForUpdates();
        }
        private void tmrStatusRefresh_Tick(object sender, EventArgs e)
        {
            Process_NewUpdates();
        }

        public void Check_ForUpdates()
        {
            try
            {
                if (myThread != null)
                    if (myThread.IsAlive)
                        return;

                tmrStatusRefresh.Enabled = true;
                myNewUpdate = new NewUpdate();
                myThread = new Thread(new ThreadStart(myNewUpdate.Check_ForUpdates));
                myThread.Start();
            }
            catch (Exception e)
            {
                Status = e.Message;
            }
        }
        public void Process_NewUpdates()
        {
            try
            {
                //Break if not ready
                if (myNewUpdate == null)
                    return;
                if (!myNewUpdate.Completed)
                {
                    Status = myNewUpdate.Status;
                    return;
                }
                if (!myNewUpdate.NewUpdatesAreAvailable)
                {
                    Status = myNewUpdate.Status;
                    Completed = true;
                    tmrStatusRefresh.Enabled = false;
                    return;
                }

                //New updates are available so we need to stop the further update search
                Stop_Update();
                
                //Confirm download
                if (!DownloadConfirmed)
                    if (myUpdater.UpdaterConfigurator.ConfirmDownload)
                    {
                        tmrStatusRefresh.Enabled = false;
                        if (MessageBox.Show("New updates are available for " + myUpdater.UpdaterConfigurator.ApplicationName + ".\r\n" +
                                            "Do you want to download and install them now?", "Installing new updates", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            DownloadConfirmed = true;
                            tmrStatusRefresh.Enabled = true;
                        }
                        else
                        {
                            Status = myNewUpdate.Status;
                            Completed = true;
                            tmrStatusRefresh.Enabled = false;
                            return;
                        }
                    }

                //Input file destination
                if (!DestinationIsSet)
                {
                    tmrStatusRefresh.Enabled = false;
                    SaveFileDialog myFileDialog = new SaveFileDialog();
                    myFileDialog.InitialDirectory = "c:\\";
                    myFileDialog.Filter = "msi files (*.msi)|*.msi|All files (*.*)|*.*";
                    myFileDialog.FilterIndex = 1;
                    myFileDialog.RestoreDirectory = true;
                    myFileDialog.FileName = myNewUpdate.InstallerFile.FileName;

                    if (myFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        myNewUpdate.InstallerFile.DestinationFileName = myFileDialog.FileName;
                        DestinationIsSet = true;
                        tmrStatusRefresh.Enabled = true;
                    }
                    return;
                }

                //Start downloading the installer file
                if (!myNewUpdate.InstallerFile.DownloadInProgress)
                    if (!myNewUpdate.InstallerFile.DownloadCompleted)
                        myNewUpdate.InstallerFile.Start_Download();
                
                //Update download progress
                if (myNewUpdate.InstallerFile.DownloadInProgress)
                {
                    if (!FormIsOpen)
                    {
                        Show();
                        FormIsOpen = true;
                    }
                    Status = "Downloading file: " + myNewUpdate.InstallerFile.FileName + " " + myNewUpdate.InstallerFile.DownloadProgress + "%";
                    lblFileName.Text = myNewUpdate.InstallerFile.FileName;
                    lblProgress.Text = myNewUpdate.InstallerFile.DownloadProgress + "%";
                }
                
                //Start installer
                if (myNewUpdate.InstallerFile.DownloadCompleted)
                {
                    if (FormIsOpen)
                    {
                        Close();
                        FormIsOpen = false;
                    }
                    Status = "";
                    Completed = true;
                    tmrStatusRefresh.Enabled = false;

                    System.Diagnostics.Process.Start(myNewUpdate.InstallerFile.DestinationFileName);
                    return;
                }
            }
            catch (Exception e)
            {
                Status = e.Message;
                Completed = true;
                tmrStatusRefresh.Enabled = false;
            }
        }

        private int ConvertMinute2Milliseconds(int minute)
        {
            try
            {
                return minute * 60 * 1000;
            }
            catch
            {
                return 0;
            }
        }
    }
}
