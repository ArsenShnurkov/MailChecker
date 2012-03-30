using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailChecker
{
    public class Ini
    {
        public string SettingsFile { get; set; }
        public string LanguageFile { get; set; }
        
        public Ini()
        {
        }
        public Ini(string settings_file)
        {
            SettingsFile = settings_file;
        }

        private string Get_Value(string file_name, string section, string key)
        {
            try
            {
                IniParser myIniParser = new IniParser(file_name);
                string value = myIniParser.IniReadValue(section, key);
                if (value == null)
                    return "!missing_ini_key: " + key + "!";
                return value;
            }
            catch (Exception e)
            {
                return "err: " + e.Message;
            }
        }

        public string Get_SettingsValue(string section, string key)
        {
            return Get_Value(SettingsFile, section, key);
        }
        public string Get_LanguageValue(string section, string key)
        {
            return Get_Value(LanguageFile, section, key);
        }
    }
}
