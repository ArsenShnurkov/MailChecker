using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace MailChecker
{
    public partial class AlertForm : Form
    {
        public double Speed { get; set; }
        public int FadeInInterval { get; set; }
        public int FadeOutInterval { get; set; }
        public double MaxOpacity { get; set; }
        public string DateFormat { get; set; }
        public List<Account.UnseenMessage> AlertMessages { get; set; }
        
        private bool AlertInProgress;

        public AlertForm()
        {
            InitializeComponent();

            AlertMessages = new List<Account.UnseenMessage>();
            Speed = 0.05;
            FadeInInterval = 20;
            FadeOutInterval = 4000;
            MaxOpacity = 0.85;
            DateFormat = "yyyy-MM-dd";

            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - Size.Width - 5,
                                      workingArea.Bottom - Size.Height);
        }

        private void tmrFadeIn_Tick(object sender, EventArgs e)
        {
            Opacity += Speed;
            if (Opacity >= MaxOpacity)
            {
                tmrFadeIn.Enabled = false;
                tmrFadeOut.Enabled = true;
            }
        }
        private void tmrFadeOut_Tick(object sender, EventArgs e)
        {
            tmrFadeOut.Enabled = false;
            AlertInProgress = false;
            Hide();
        }
        private void tmrAlert_Tick(object sender, EventArgs e)
        {
            foreach (Account.UnseenMessage message in AlertMessages)
                if (!AlertInProgress)
                    if (!message.Alerted)
                    {
                        message.Alerted = true;
                        AlertInProgress = true;
                        lblFromDisplayName.Text = message.FromDisplayName;
                        lblSubject.Text = message.Subject;
                        lblBody.Text = message.Body;
                        lblDate.Text = message.Date.ToString(DateFormat);

                        //Play alert sound
                        SoundPlayer beep = new SoundPlayer(MainForm.beepSound);
                        beep.Play();

                        Show();
                        tmrFadeOut.Interval = FadeOutInterval;
                        tmrFadeIn.Interval = FadeInInterval;
                        tmrFadeIn.Enabled = true;
                    }
        }
    }
}
