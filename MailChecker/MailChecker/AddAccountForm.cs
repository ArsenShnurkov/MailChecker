using System;

using System.Windows.Forms;

namespace MailChecker
{
    public partial class AddAccountForm : Form
    {
        public AddAccountForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text == "" || txtPassword.Text == "")
                return;

            string host_name = MainForm.myCommon.Determine_HostName(txtEmail.Text);
            string host = MainForm.myIni.Get_SettingsValue(host_name, "host");
            if (host.Contains("missing_ini_key"))
            {
                MessageBox.Show(MainForm.myIni.Get_LanguageValue("errors", "not_supported").Replace("%1", host_name));
                return;
            }
            if (MainForm.myDatabase.Check_ForDouble(txtEmail.Text))
            {
                MessageBox.Show(MainForm.myIni.Get_LanguageValue("errors", "account_already_exists").Replace("%1", txtEmail.Text));
                return;
            }

            MainForm.myDatabase.Save_Account(txtEmail.Text, txtEmail.Text.Substring(0, txtEmail.Text.IndexOf(@"@")), txtPassword.Text);
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
        private void AddAccountForm_Shown(object sender, EventArgs e)
        {
            Text = MainForm.myIni.Get_LanguageValue("add_account_panel", "add_new_account");
        }
    }
}
