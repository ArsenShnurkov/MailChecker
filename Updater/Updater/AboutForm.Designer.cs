namespace Updater
{
    partial class AboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
            this.lblVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnCheckForUpdates = new System.Windows.Forms.Button();
            this.linkLblSupport = new System.Windows.Forms.LinkLabel();
            this.lblSupport = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tmrStatusRefresh = new System.Windows.Forms.Timer(this.components);
            this.lblHome = new System.Windows.Forms.Label();
            this.linkLblHome = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Calibri", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblVersion.Location = new System.Drawing.Point(165, 41);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(40, 13);
            this.lblVersion.TabIndex = 0;
            this.lblVersion.Text = "0.0.0.0";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(142, 65);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTitle.Location = new System.Drawing.Point(160, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(161, 29);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Program name";
            // 
            // btnCheckForUpdates
            // 
            this.btnCheckForUpdates.Location = new System.Drawing.Point(12, 83);
            this.btnCheckForUpdates.Name = "btnCheckForUpdates";
            this.btnCheckForUpdates.Size = new System.Drawing.Size(142, 23);
            this.btnCheckForUpdates.TabIndex = 7;
            this.btnCheckForUpdates.Text = "Check for udpates";
            this.btnCheckForUpdates.UseVisualStyleBackColor = true;
            this.btnCheckForUpdates.Click += new System.EventHandler(this.btnCheckForUpdates_Click);
            // 
            // linkLblSupport
            // 
            this.linkLblSupport.AutoSize = true;
            this.linkLblSupport.Location = new System.Drawing.Point(64, 144);
            this.linkLblSupport.Name = "linkLblSupport";
            this.linkLblSupport.Size = new System.Drawing.Size(111, 13);
            this.linkLblSupport.TabIndex = 9;
            this.linkLblSupport.TabStop = true;
            this.linkLblSupport.Text = "support@support.com";
            // 
            // lblSupport
            // 
            this.lblSupport.AutoSize = true;
            this.lblSupport.Location = new System.Drawing.Point(11, 144);
            this.lblSupport.Name = "lblSupport";
            this.lblSupport.Size = new System.Drawing.Size(47, 13);
            this.lblSupport.TabIndex = 10;
            this.lblSupport.Text = "Support:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Calibri", 8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStatus.Location = new System.Drawing.Point(12, 109);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 11;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(276, 182);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // tmrStatusRefresh
            // 
            this.tmrStatusRefresh.Tick += new System.EventHandler(this.tmrStatusRefresh_Tick);
            // 
            // lblHome
            // 
            this.lblHome.AutoSize = true;
            this.lblHome.Location = new System.Drawing.Point(25, 131);
            this.lblHome.Name = "lblHome";
            this.lblHome.Size = new System.Drawing.Size(33, 13);
            this.lblHome.TabIndex = 14;
            this.lblHome.Text = "Web:";
            // 
            // linkLblHome
            // 
            this.linkLblHome.AutoSize = true;
            this.linkLblHome.Location = new System.Drawing.Point(64, 131);
            this.linkLblHome.Name = "linkLblHome";
            this.linkLblHome.Size = new System.Drawing.Size(46, 13);
            this.linkLblHome.TabIndex = 13;
            this.linkLblHome.TabStop = true;
            this.linkLblHome.Text = "Website";
            this.linkLblHome.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblHome_LinkClicked);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 208);
            this.Controls.Add(this.lblHome);
            this.Controls.Add(this.linkLblHome);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblSupport);
            this.Controls.Add(this.linkLblSupport);
            this.Controls.Add(this.btnCheckForUpdates);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblVersion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AboutForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnCheckForUpdates;
        private System.Windows.Forms.LinkLabel linkLblSupport;
        private System.Windows.Forms.Label lblSupport;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Timer tmrStatusRefresh;
        private System.Windows.Forms.Label lblHome;
        private System.Windows.Forms.LinkLabel linkLblHome;

    }
}