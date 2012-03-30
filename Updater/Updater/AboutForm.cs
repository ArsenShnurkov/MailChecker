using System;
using System.Windows.Forms;

namespace Updater
{
    public partial class AboutForm : Form
    {
        private static Updater myUpdater;
        private UpdateForm myUpdateForm;

        public AboutForm(Updater updater)
        {
            InitializeComponent();

            myUpdater = updater;
            lblTitle.Text = myUpdater.UpdaterConfigurator.ApplicationName;
            lblVersion.Text = myUpdater.UpdaterConfigurator.VerMajor + "." + myUpdater.UpdaterConfigurator.VerMinor + "." + 
                              myUpdater.UpdaterConfigurator.VerBuild + "." + myUpdater.UpdaterConfigurator.VerRevision;
            linkLblHome.Text = myUpdater.UpdaterConfigurator.Homepage;
            linkLblSupport.Text = myUpdater.UpdaterConfigurator.SupportEmail;
        }

        private void tmrStatusRefresh_Tick(object sender, EventArgs e)
        {
            if (myUpdateForm == null)
                return;

            lblStatus.Text = myUpdateForm.Status;
            if (myUpdateForm.Completed)
            {
                btnCheckForUpdates.Enabled = true;
                tmrStatusRefresh.Enabled = false;
            }
        }
        private void btnCheckForUpdates_Click(object sender, EventArgs e)
        {
            if (btnCheckForUpdates.Enabled)
                btnCheckForUpdates.Enabled = false;
            tmrStatusRefresh.Enabled = true;
            myUpdateForm = new UpdateForm(myUpdater);
            myUpdateForm.Check_ForUpdates();
        }
        private void linkLblHome_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLblHome.Text);
        }
    }
}
