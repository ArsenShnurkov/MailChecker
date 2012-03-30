using System;
using System.Collections.Generic;
using System.Net;
using System.ComponentModel;

namespace Updater
{
    public class SourceForge
    {
        public class SourceForgeFolder
        {
            public string Url { get; set; }
            public string FileName { get; set; }
            public string DestinationFileName { get; set; }
            public bool IsFolder { get; set; }
            public bool DownloadInProgress { get; set; }
            public bool DownloadCompleted { get; set; }
            public int DownloadProgress { get; set; }

            public SourceForgeFolder()
            {
            }
            public SourceForgeFolder(string url, string file_name, bool is_folder)
            {
                Url = url;
                IsFolder = is_folder;
                FileName = file_name;
            }
            public void Start_Download()
            {
                DownloadInProgress = true;
                WebClient webClient = new WebClient();
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Download_Completed);
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(Download_InProgress);
                webClient.DownloadFileAsync(new Uri(Url), this.@DestinationFileName);
            }
            private void Download_InProgress(object sender, DownloadProgressChangedEventArgs e)
            {
                DownloadInProgress = true;
                DownloadProgress = e.ProgressPercentage;
            }
            private void Download_Completed(object sender, AsyncCompletedEventArgs e)
            {
                DownloadInProgress = false;
                DownloadCompleted = true;
            }
        }

        public string RootFolderName { get; set; }
        public int VerMajor { get; set; }
        public int VerMinor { get; set; }
        public int VerBuild { get; set; }
        public int VerRevision { get; set; }

        private static string pattern_folder = "folder warn";
        private static string pattern_file = "file warn";
        private static string pattern_title = "title=";

        public SourceForge(string url)
        {
            RootFolderName = "";
            VerMajor = 0;
            VerMinor = 0;
            VerBuild = 0;
            VerRevision = 0;

            //Initialize root
            string pageContent = Download_Content(url);
            bool FoundRoot = false;
            int start_pos = 0;
            int i = 0;
            while ((i < pageContent.Length - pattern_folder.Length) && (!FoundRoot))
            {
                i++;
                if (pageContent.Substring(i, pattern_title.Length) == pattern_title)
                    start_pos = i + pattern_title.Length + 1;
                if (pageContent.Substring(i, pattern_folder.Length) == pattern_folder)
                {
                    int length = (i - 9) - start_pos;
                    string version = pageContent.Substring(start_pos, length);
                    string[] sub_versions = version.Split('.');

                    //Test loop
                    bool evaluate = true;
                    if (sub_versions == null)
                        evaluate = false;
                    else
                        for (int j = 0; j < sub_versions.Length; j++)
                        {
                            int test;
                            if (!int.TryParse(sub_versions[j], out test))
                                evaluate = false;
                        }
                    if (evaluate && !FoundRoot)
                    {
                        for (int j = 0; j < sub_versions.Length; j++)
                        {
                            if (j == 0)
                                VerMajor = int.Parse(sub_versions[j]);
                            if (j == 1)
                                VerMinor = int.Parse(sub_versions[j]);
                            if (j == 2)
                                VerBuild = int.Parse(sub_versions[j]);
                            if (j == 3)
                                VerRevision = int.Parse(sub_versions[j]);
                        }
                        RootFolderName = VerMajor + "." + VerMinor + "." + VerBuild + "." + VerRevision;
                        FoundRoot = true;
                    }
                }
            }
        }
        public void Get_Folders(string url, ref List<SourceForgeFolder> files, bool include_subdirectories, bool files_only)
        {
            string pageContent = Download_Content(url);
            int start_pos = 0;
            int i = 0;
            while (i < pageContent.Length - pattern_folder.Length)
            {
                i++;
                if (pageContent.Substring(i, pattern_title.Length) == pattern_title)
                    start_pos = i + pattern_title.Length + 1;
                if (pageContent.Substring(i, pattern_folder.Length) == pattern_folder)
                {
                    int length = (i - 9) - start_pos;
                    string folder = pageContent.Substring(start_pos, length);
                    if (!files_only)
                        files.Add(new SourceForgeFolder(url + folder + "/", "", true));
                    if (include_subdirectories)
                        Get_Folders(url + folder + "/", ref files, include_subdirectories, files_only);  //recursive search
                }
                if (pageContent.Substring(i, pattern_file.Length) == pattern_file)
                {
                    int length = (i - 9) - start_pos;
                    string file = pageContent.Substring(start_pos, length);
                    files.Add(new SourceForgeFolder(url + file + "/download", file, false));
                }
            }
        }

        private string Download_Content(string url)
        {
            try
            {
                WebClient client = new WebClient();
                string content = client.DownloadString(url);
                return content;
            }
            catch
            {
                return "";
            }
        }
    }
}
