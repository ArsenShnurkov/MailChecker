namespace MailChecker
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolSMLanguage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSSeperator = new System.Windows.Forms.ToolStripSeparator();
            this.toolSMCheckMailNow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSMSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSMAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSMExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tmrCheckAccounts = new System.Windows.Forms.Timer(this.components);
            this.tmrThreadListener = new System.Windows.Forms.Timer(this.components);
            this.listAccounts = new System.Windows.Forms.ListBox();
            this.lblCheckForNewMails = new System.Windows.Forms.Label();
            this.comboMinutes = new System.Windows.Forms.ComboBox();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "MailChecker";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.notifyIcon_DoubleClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSMLanguage,
            this.toolSSeperator,
            this.toolSMCheckMailNow,
            this.toolSMSettings,
            this.toolSMAbout,
            this.toolSMExit});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(160, 142);
            // 
            // toolSMLanguage
            // 
            this.toolSMLanguage.Image = ((System.Drawing.Image)(resources.GetObject("toolSMLanguage.Image")));
            this.toolSMLanguage.Name = "toolSMLanguage";
            this.toolSMLanguage.Size = new System.Drawing.Size(159, 22);
            this.toolSMLanguage.Text = "Language";
            this.toolSMLanguage.Visible = false;
            // 
            // toolSSeperator
            // 
            this.toolSSeperator.Name = "toolSSeperator";
            this.toolSSeperator.Size = new System.Drawing.Size(156, 6);
            // 
            // toolSMCheckMailNow
            // 
            this.toolSMCheckMailNow.Image = ((System.Drawing.Image)(resources.GetObject("toolSMCheckMailNow.Image")));
            this.toolSMCheckMailNow.Name = "toolSMCheckMailNow";
            this.toolSMCheckMailNow.Size = new System.Drawing.Size(159, 22);
            this.toolSMCheckMailNow.Text = "Check mail now";
            this.toolSMCheckMailNow.Click += new System.EventHandler(this.toolSMCheckMailNow_Click);
            // 
            // toolSMSettings
            // 
            this.toolSMSettings.Image = ((System.Drawing.Image)(resources.GetObject("toolSMSettings.Image")));
            this.toolSMSettings.Name = "toolSMSettings";
            this.toolSMSettings.Size = new System.Drawing.Size(159, 22);
            this.toolSMSettings.Text = "Settings";
            this.toolSMSettings.Click += new System.EventHandler(this.toolSMSettings_Click);
            // 
            // toolSMAbout
            // 
            this.toolSMAbout.Image = ((System.Drawing.Image)(resources.GetObject("toolSMAbout.Image")));
            this.toolSMAbout.Name = "toolSMAbout";
            this.toolSMAbout.Size = new System.Drawing.Size(159, 22);
            this.toolSMAbout.Text = "About";
            this.toolSMAbout.Click += new System.EventHandler(this.toolSMAbout_Click);
            // 
            // toolSMExit
            // 
            this.toolSMExit.Image = ((System.Drawing.Image)(resources.GetObject("toolSMExit.Image")));
            this.toolSMExit.Name = "toolSMExit";
            this.toolSMExit.Size = new System.Drawing.Size(159, 22);
            this.toolSMExit.Text = "Exit";
            this.toolSMExit.Click += new System.EventHandler(this.toolSMExit_Click);
            // 
            // tmrCheckAccounts
            // 
            this.tmrCheckAccounts.Tick += new System.EventHandler(this.tmrCheckAccounts_Tick);
            // 
            // tmrThreadListener
            // 
            this.tmrThreadListener.Enabled = true;
            this.tmrThreadListener.Interval = 1000;
            this.tmrThreadListener.Tick += new System.EventHandler(this.tmrThreadListener_Tick);
            // 
            // listAccounts
            // 
            this.listAccounts.FormattingEnabled = true;
            this.listAccounts.Location = new System.Drawing.Point(12, 34);
            this.listAccounts.Name = "listAccounts";
            this.listAccounts.Size = new System.Drawing.Size(237, 121);
            this.listAccounts.TabIndex = 1;
            // 
            // lblCheckForNewMails
            // 
            this.lblCheckForNewMails.AutoSize = true;
            this.lblCheckForNewMails.Location = new System.Drawing.Point(9, 13);
            this.lblCheckForNewMails.Name = "lblCheckForNewMails";
            this.lblCheckForNewMails.Size = new System.Drawing.Size(142, 13);
            this.lblCheckForNewMails.TabIndex = 2;
            this.lblCheckForNewMails.Text = "Check for new mails in every";
            // 
            // comboMinutes
            // 
            this.comboMinutes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboMinutes.FormattingEnabled = true;
            this.comboMinutes.Items.AddRange(new object[] {
            "1",
            "5",
            "10",
            "15",
            "30",
            "45",
            "60"});
            this.comboMinutes.Location = new System.Drawing.Point(161, 10);
            this.comboMinutes.Name = "comboMinutes";
            this.comboMinutes.Size = new System.Drawing.Size(39, 21);
            this.comboMinutes.TabIndex = 3;
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(206, 13);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(43, 13);
            this.lblMinutes.TabIndex = 4;
            this.lblMinutes.Text = "minutes";
            // 
            // btnAdd
            // 
            this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
            this.btnAdd.Location = new System.Drawing.Point(255, 34);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(23, 23);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.Image")));
            this.btnRemove.Location = new System.Drawing.Point(255, 63);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(23, 23);
            this.btnRemove.TabIndex = 6;
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 161);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(58, 23);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 188);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.listAccounts);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.comboMinutes);
            this.Controls.Add(this.lblCheckForNewMails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MailChecker";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolSMLanguage;
        private System.Windows.Forms.ToolStripMenuItem toolSMCheckMailNow;
        private System.Windows.Forms.ToolStripMenuItem toolSMExit;
        private System.Windows.Forms.ToolStripMenuItem toolSMSettings;
        private System.Windows.Forms.ToolStripSeparator toolSSeperator;
        private System.Windows.Forms.ToolStripMenuItem toolSMAbout;
        private System.Windows.Forms.Timer tmrCheckAccounts;
        private System.Windows.Forms.Timer tmrThreadListener;
        private System.Windows.Forms.ListBox listAccounts;
        private System.Windows.Forms.Label lblCheckForNewMails;
        private System.Windows.Forms.ComboBox comboMinutes;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnClose;
    }
}

