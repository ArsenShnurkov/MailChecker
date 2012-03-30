namespace MailChecker
{
    partial class AlertForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlertForm));
            this.tmrFadeIn = new System.Windows.Forms.Timer(this.components);
            this.tmrFadeOut = new System.Windows.Forms.Timer(this.components);
            this.lblFromDisplayName = new System.Windows.Forms.Label();
            this.lblBody = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.tmrAlert = new System.Windows.Forms.Timer(this.components);
            this.lblSubject = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tmrFadeIn
            // 
            this.tmrFadeIn.Interval = 20;
            this.tmrFadeIn.Tick += new System.EventHandler(this.tmrFadeIn_Tick);
            // 
            // tmrFadeOut
            // 
            this.tmrFadeOut.Interval = 4000;
            this.tmrFadeOut.Tick += new System.EventHandler(this.tmrFadeOut_Tick);
            // 
            // lblFromDisplayName
            // 
            this.lblFromDisplayName.AutoSize = true;
            this.lblFromDisplayName.BackColor = System.Drawing.Color.Transparent;
            this.lblFromDisplayName.Font = new System.Drawing.Font("Calibri", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblFromDisplayName.ForeColor = System.Drawing.Color.Black;
            this.lblFromDisplayName.Location = new System.Drawing.Point(11, 9);
            this.lblFromDisplayName.Name = "lblFromDisplayName";
            this.lblFromDisplayName.Size = new System.Drawing.Size(156, 22);
            this.lblFromDisplayName.TabIndex = 0;
            this.lblFromDisplayName.Text = "From Display Name";
            // 
            // lblBody
            // 
            this.lblBody.AutoSize = true;
            this.lblBody.BackColor = System.Drawing.Color.Transparent;
            this.lblBody.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblBody.Location = new System.Drawing.Point(12, 54);
            this.lblBody.MaximumSize = new System.Drawing.Size(330, 90);
            this.lblBody.Name = "lblBody";
            this.lblBody.Size = new System.Drawing.Size(32, 14);
            this.lblBody.TabIndex = 1;
            this.lblBody.Text = "Body";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblDate.Location = new System.Drawing.Point(276, 16);
            this.lblDate.Name = "lblDate";
            this.lblDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblDate.Size = new System.Drawing.Size(31, 14);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Date";
            // 
            // tmrAlert
            // 
            this.tmrAlert.Enabled = true;
            this.tmrAlert.Tick += new System.EventHandler(this.tmrAlert_Tick);
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.BackColor = System.Drawing.Color.Transparent;
            this.lblSubject.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)
                            | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblSubject.ForeColor = System.Drawing.Color.Black;
            this.lblSubject.Location = new System.Drawing.Point(12, 31);
            this.lblSubject.MaximumSize = new System.Drawing.Size(340, 70);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(43, 14);
            this.lblSubject.TabIndex = 5;
            this.lblSubject.Text = "Subject";
            // 
            // AlertForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(350, 150);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblBody);
            this.Controls.Add(this.lblFromDisplayName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AlertForm";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "AlertForm";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer tmrFadeIn;
        private System.Windows.Forms.Timer tmrFadeOut;
        private System.Windows.Forms.Label lblFromDisplayName;
        private System.Windows.Forms.Label lblBody;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Timer tmrAlert;
        private System.Windows.Forms.Label lblSubject;
    }
}