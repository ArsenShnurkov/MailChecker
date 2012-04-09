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
            lblStatus.Text = "";
            lblTitle.Text = myUpdater.UpdaterConfigurator.ApplicationName;
            lblVersion.Text = myUpdater.UpdaterConfigurator.Version.ToString();
            linkLblHome.Text = myUpdater.UpdaterConfigurator.Homepage;
            linkLblSupport.Text = myUpdater.UpdaterConfigurator.SupportEmail;
            //external dlls
            lblExtDlls.Text = "";
            foreach (Updater.Configurator.ExternalDll ext_dll in myUpdater.UpdaterConfigurator.ExternalDlls)
                if (lblExtDlls.Text == "")
                    lblExtDlls.Text = ext_dll.Name + ": " + ext_dll.Version.ToString();
                else
                    lblExtDlls.Text += ("\r\n" + ext_dll.Name + ": " + ext_dll.Version);
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
