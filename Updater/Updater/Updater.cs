using System;
using System.Reflection;
using System.Collections.Generic;

namespace Updater
{
    public class Updater
    {
        public class Configurator
        {
            public class ExternalDll
            {
                public ExternalDll(string name, string version)
                {
                    Name = name;
                    Version = version;
                }

                public string Name { get; set; }
                public string Version { get; set; }
            }

            private Version _version;

            public Configurator()
            {
                ExternalDlls = new List<ExternalDll>();
                UpdateInterval = 10;  //checks for update in every 10 minutes (default)
            }

            public List<ExternalDll> ExternalDlls { get; set; }
            public Version Version
            {
                get { return _version; }
                set
                {
                    _version = value;
                    VerMajor = value.Major;
                    VerMinor = value.Minor;
                    VerBuild = value.Build;
                    VerRevision = value.Revision;
                }
            }
            public string ApplicationName { get; set; }
            public string Homepage { get; set; }
            public string SupportEmail { get; set; }
            public string UpdateUrl { get; set; }
            public int VerMajor { get; private set; }
            public int VerMinor { get; private set; }
            public int VerBuild { get; private set; }
            public int VerRevision { get; private set; }
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
