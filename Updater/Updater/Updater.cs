using System;
using System.Collections.Generic;

namespace Updater
{
    public class Updater
    {
        public class Configurator
        {
            public Configurator()
            {
                UpdateInterval = 10;  //checks for update in every 10 minutes (default)
            }

            public string ApplicationName { get; set; }
            public string Homepage { get; set; }
            public string SupportEmail { get; set; }
            public string UpdateUrl { get; set; }
            public int VerMajor { get; set; }
            public int VerMinor { get; set; }
            public int VerBuild { get; set; }
            public int VerRevision { get; set; }
            public int UpdateInterval { get; set; }
            public bool ConfirmDownload { get; set; }
        }
        public Configurator UpdaterConfigurator { get; set; }

        private static UpdateForm myUpdateForm;
        private static AboutForm myAboutForm;

        public Updater(Configurator myConfig)
        {
            UpdaterConfigurator = myConfig;
        }
        public void Start()
        {
            myUpdateForm = new UpdateForm(this);
            myUpdateForm.Start_Update();
        }
        public void Stop()
        {
            myUpdateForm.Stop_Update();
        }
        public void Show_AboutForm()
        {
            myAboutForm = new AboutForm(this);
            myAboutForm.ShowDialog();
        }
    }
}
