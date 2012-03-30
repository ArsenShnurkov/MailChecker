using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MailChecker
{
    public class Language
    {
        public string SavedLanguage { get; set; }

        private List<string> Get_LanguageFiles(string language_dir)
        {
            try
            {
                List<string> return_value = new List<string>();
                string[] files = Directory.GetFiles(language_dir, "*.ini");
                for (int i = 0; i < files.Length; i++)
                {
                    return_value.Add(files[i].Substring(files[i].LastIndexOf(@"\") + 1, files[i].LastIndexOf(".ini") - files[i].LastIndexOf(@"\") - 1));
                }
                return_value.Sort();
                return return_value;
            }
            catch
            {
                return null;
            }
        }
        public List<ToolStripMenuItem> Create_LanguageMenuItems(ref ToolStripSeparator sep, ref ToolStripMenuItem menuItem, string language_dir)
        {
            try
            {
                List<ToolStripMenuItem> return_value = new List<ToolStripMenuItem>();
                List<string> language_files = Get_LanguageFiles(language_dir);
                if (language_files != null)
                {
                    if (language_files.Count > 0)
                    {
                        sep.Visible = true;
                        menuItem.Visible = true;
                    }
                    foreach (string language in language_files)
                        return_value.Add((menuItem.DropDownItems.Add(language) as ToolStripMenuItem));
                }
                return return_value;
            }
            catch
            {
                return null;
            }
        }
        public void Set_SavedLanguage(ref ToolStripMenuItem menuItem, string saved_language)
        {
            try
            {
                foreach (ToolStripMenuItem item in menuItem.DropDownItems)
                    item.Checked = (item.Text == saved_language);
                SavedLanguage = saved_language;
            }
            catch
            {
            }
        }
    }
}
