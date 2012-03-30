using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace MailChecker
{
    public partial class MainForm : Form
    {
        public static Common myCommon = new Common();
        public static Language myLanguage = new Language();
        public static Database myDatabase = new Database();
        public static Ini myIni = new Ini();
        public static Network myNetwork = new Network();

        private bool CloseApplication = false;
        private bool Connected = false;
        private bool IconOfflineIsSet = false;
        private bool IconNewMailIsSet = false;
        private string ToolTipMessage = "";

        private static string dbFile = myCommon.Get_ApplicationPath() + @"\data\data.db";
        private static string iniSettingsFile = myCommon.Get_ApplicationPath() + @"\settings.ini";
        private static string languageDir = myCommon.Get_ApplicationPath() + @"\language\";
        private static string offlineIcon = myCommon.Get_ApplicationPath() + @"\icons\offline.ico";
        private static string normalIcon = myCommon.Get_ApplicationPath() + @"\icons\normal.ico";
        private static string newMailIcon = myCommon.Get_ApplicationPath() + @"\icons\new_mail.ico";
        public static string beepSound = myCommon.Get_ApplicationPath() + @"\sounds\beep.wav";

        private static List<Account> myAccounts = new List<Account>();
        private List<Thread> myThreads = new List<Thread>();
        private List<string> myAlertedMessages = new List<string>();
        private AlertForm myAlertForm = new AlertForm();
        private AddAccountForm myAddAccountForm = new AddAccountForm();
        private Updater.Updater myUpdater;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                //Set helper classes
                myDatabase = new Database(dbFile);
                myIni = new Ini(iniSettingsFile);
                
                //Set language menu
                List<ToolStripMenuItem> languageMenuItems = myLanguage.Create_LanguageMenuItems(ref toolSSeperator, ref toolSMLanguage, languageDir);
                foreach (ToolStripMenuItem menuItem in languageMenuItems)
                    menuItem.Click += new EventHandler(LanguageMenuItem_Click);
                InitLanguage();

                //Initialize controls
                InitControls();

                //Check for unnseen mails when program start
                Check_AccountsForUnseenMails();

                //Start program updater
                Updater.Updater.Configurator myConfig = new Updater.Updater.Configurator();
                myConfig.ApplicationName = AppDomain.CurrentDomain.FriendlyName.Replace(".exe", "");
                myConfig.Homepage = "https://sourceforge.net/projects/mailcheckerpro/";
                myConfig.UpdateUrl = "https://sourceforge.net/projects/mailcheckerpro/files/";
                myConfig.SupportEmail = "mihaly.sogorka@aol.com";
                myConfig.VerMajor = Assembly.GetEntryAssembly().GetName().Version.Major;
                myConfig.VerMinor = Assembly.GetEntryAssembly().GetName().Version.Minor;
                myConfig.VerBuild = Assembly.GetEntryAssembly().GetName().Version.Build;
                myConfig.VerRevision = Assembly.GetEntryAssembly().GetName().Version.Revision;
                myConfig.ConfirmDownload = true;
                myConfig.UpdateInterval = 10;
                myUpdater = new Updater.Updater(myConfig);
                myUpdater.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("Fatal error: " + e.Message);
                CloseApplication = true;
                Close();
            }
        }
        private void InitLanguage()
        {
            myLanguage.Set_SavedLanguage(ref toolSMLanguage, myDatabase.Get_SavedLanguage());
            myIni.LanguageFile = languageDir + myLanguage.SavedLanguage + ".ini";

            //menu
            toolSMLanguage.Text = myIni.Get_LanguageValue("menu", "language");
            toolSMCheckMailNow.Text = myIni.Get_LanguageValue("menu", "check_mail_now");
            toolSMSettings.Text = myIni.Get_LanguageValue("menu", "settings");
            toolSMAbout.Text = myIni.Get_LanguageValue("menu", "about");
            toolSMExit.Text = myIni.Get_LanguageValue("menu", "exit");

            //settings panel
            lblCheckForNewMails.Text = myIni.Get_LanguageValue("settings_panel", "check_for_new_mails");
            lblMinutes.Text = myIni.Get_LanguageValue("settings_panel", "minutes");
            btnClose.Text = myIni.Get_LanguageValue("settings_panel", "close");

            //add new account panel
            (myAddAccountForm.Controls["lblEmail"] as Label).Text = myIni.Get_LanguageValue("add_account_panel", "email");
            (myAddAccountForm.Controls["lblPassword"] as Label).Text = myIni.Get_LanguageValue("add_account_panel", "password");
            (myAddAccountForm.Controls["btnSave"] as Button).Text = myIni.Get_LanguageValue("add_account_panel", "save");
            (myAddAccountForm.Controls["btnCancel"] as Button).Text = myIni.Get_LanguageValue("add_account_panel", "cancel");
        }
        private void InitControls()
        {
            //Set saved shedule time
            int minute = myDatabase.Get_SavedTimerInterval();
            for (int i = 0; i < comboMinutes.Items.Count; i++)
                if (int.Parse(comboMinutes.Items[i].ToString()) == minute)
                    comboMinutes.SelectedIndex = i;

            //Fill saved accounts
            listAccounts.Items.Clear();
            List<Account> myAccounts = myDatabase.Get_SavedAccounts();
            foreach (Account account in myAccounts)
                listAccounts.Items.Add(account.Email);

            //Set timer interval
            tmrCheckAccounts.Interval = myCommon.ConvertMinute2Milliseconds(myDatabase.Get_SavedTimerInterval());
            tmrCheckAccounts.Enabled = true;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Hide();
        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
                Hide();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CloseApplication)
            {
                Hide();
                e.Cancel = true;
            }
        }

        private void LanguageMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripDropDownItem item = (sender as ToolStripDropDownItem);

            myDatabase.Save_Language(item.Text);
            InitLanguage();
        }
        private void toolSMExit_Click(object sender, EventArgs e)
        {
            CloseApplication = true;
            Close();
        }
        private void toolSMAbout_Click(object sender, EventArgs e)
        {
            myUpdater.Show_AboutForm();
        }
        private void toolSMSettings_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }
        private void toolSMCheckMailNow_Click(object sender, EventArgs e)
        {
            myAlertedMessages = new List<string>();
            Check_AccountsForUnseenMails();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            (myAddAccountForm.Controls["txtEmail"] as TextBox).Text = "";
            (myAddAccountForm.Controls["txtPassword"] as TextBox).Text = "";
            if (myAddAccountForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                InitControls();
                Check_AccountsForUnseenMails();
            }
        }
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listAccounts.SelectedItem == null)
                return;
            string confirm_title = myIni.Get_LanguageValue("messages", "confirm_title");
            string confirm = myIni.Get_LanguageValue("messages", "confirm").Replace("%1", listAccounts.SelectedItem.ToString());
            if (MessageBox.Show(confirm, confirm_title, MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            myDatabase.Delete_Account(listAccounts.SelectedItem.ToString());
            InitControls();
            Check_AccountsForUnseenMails();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            myDatabase.Save_TimerInterval(int.Parse(comboMinutes.SelectedItem.ToString()));
            InitControls();
            Close();
        }
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            List<Account> myAccounts = myDatabase.Get_SavedAccounts();
            if (myAccounts.Count == 0)
                return;

            if (myAccounts.Count == 1)
            {
                string host_name = myCommon.Determine_HostName(myAccounts[0].Email);
                string url = myIni.Get_SettingsValue(host_name, "url");
                myNetwork.Start_InDefaultBrowser(url);
            }
            else
            {
                //Start launcher
                //Should be implemented soon
            }
        }

        private void tmrCheckAccounts_Tick(object sender, EventArgs e)
        {
            Check_AccountsForUnseenMails();
        }
        private void tmrThreadListener_Tick(object sender, EventArgs e)
        {
            try
            {
                //Network monitor
                Connected = myNetwork.Check_InternetAccess();
                if (!Connected)
                {
                    if (!IconOfflineIsSet)
                    {
                        notifyIcon.Icon = new System.Drawing.Icon(offlineIcon);
                        IconOfflineIsSet = true;
                        IconNewMailIsSet = false;
                    }
                    Set_TooltipMessage(myIni.Get_LanguageValue("messages", "no_internet"));
                    return;
                }

                if (IconOfflineIsSet && !IconNewMailIsSet)
                {
                    notifyIcon.Icon = new System.Drawing.Icon(normalIcon);
                    IconOfflineIsSet = false;
                    Check_AccountsForUnseenMails();
                }
                if (ToolTipMessage == "")
                    Set_TooltipMessage("MailChecker");

                Process_UnseenMails();
            }
            catch (Exception ex)
            {
                ToolTipMessage = ex.Message;
                Set_TooltipMessage(ToolTipMessage);
            }
        }

        private void Check_AccountsForUnseenMails()
        {
            try
            {
                //Don't check if not connected to internet
                Connected = myNetwork.Check_InternetAccess();
                if (!Connected)
                    return;

                //Don't check if threads are already running
                foreach (Thread thread in myThreads)
                    if (thread.IsAlive)
                        return;

                //Start checking mails
                myThreads = new List<Thread>();
                myAccounts = myDatabase.Get_SavedAccounts();
                foreach (Account account in myAccounts)
                {
                    string hostname_section = myCommon.Determine_HostName(account.Email);
                    account.Host = myIni.Get_SettingsValue(hostname_section, "host");
                    account.Port = int.Parse(myIni.Get_SettingsValue(hostname_section, "port"));
                    account.UseSsl = bool.Parse(myIni.Get_SettingsValue(hostname_section, "ssl"));
                    myThreads.Add(new Thread(new ThreadStart(account.Check_ForUnseenMails)));
                }
                foreach (Thread thread in myThreads)
                    thread.Start();  //let's rock!
            }
            catch (Exception e)
            {
                ToolTipMessage = e.Message;
                Set_TooltipMessage(ToolTipMessage);
            }
        }
        private void Process_UnseenMails()
        {
            try
            {
                ToolTipMessage = "";
                int UnseenMessages = 0;
                foreach (Account account in myAccounts)
                    if (account.Finished)
                        UnseenMessages += account.UnseenMessages.Count;
                if (UnseenMessages > 0)
                {
                    if (!IconNewMailIsSet)
                    {
                        notifyIcon.Icon = new System.Drawing.Icon(newMailIcon);
                        IconNewMailIsSet = true;
                    }
                    ToolTipMessage = myIni.Get_LanguageValue("messages", "new_message").Replace("%1", UnseenMessages.ToString());
                }
                else
                {
                    if (IconNewMailIsSet)
                    {
                        notifyIcon.Icon = new System.Drawing.Icon(normalIcon);
                        IconNewMailIsSet = false;
                    }
                    ToolTipMessage = myIni.Get_LanguageValue("messages", "no_message");
                }
                Set_TooltipMessage(ToolTipMessage);

                foreach (Account account in myAccounts)
                {
                    if (account.Finished)
                        foreach (Account.UnseenMessage message in account.UnseenMessages)
                            if (!myAlertedMessages.Contains(message.MessageId))
                            {
                                //Schedule unseen messages for alert
                                myAlertForm.DateFormat = myIni.Get_LanguageValue("settings", "date_format");
                                myAlertForm.AlertMessages.Add(new Account.UnseenMessage(message.MessageId, message.FromDisplayName, message.Subject, message.Body, message.AttachmentsCount, message.Date, false));
                                myAlertedMessages.Add(message.MessageId);
                            }
                    if (account.Status != "")
                    {
                        ToolTipMessage = account.Status;
                        Set_TooltipMessage(ToolTipMessage);
                    }
                }
            }
            catch (Exception e)
            {
                ToolTipMessage = e.Message;
                Set_TooltipMessage(ToolTipMessage);
            }
        }
        private void Set_TooltipMessage(string message)
        {
            if (message.Length > 63)
                message = message.Substring(0, 62);
            notifyIcon.Text = message;
        }
    }
}
