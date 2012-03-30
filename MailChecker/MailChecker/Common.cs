using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.Win32;

namespace MailChecker
{
    public class Common
    {
        public string Get_ApplicationPath()
        {
            try
            {
                string return_value = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                return_value = return_value.Substring(6, return_value.Length - 6);
                return return_value;
            }
            catch
            {
                return "";
            }
        }
        public int ConvertMinute2Milliseconds(int minute)
        {
            try
            {
                return minute * 60 * 1000;
            }
            catch
            {
                return 0;
            }
        }
        public string Determine_HostName(string email)
        {
            try
            {
                string return_value = "";
                return_value = email.Substring(email.IndexOf(@"@") + 1, email.LastIndexOf(".") - email.IndexOf(@"@") - 1);
                return return_value;
            }
            catch
            {
                return "";
            }
        }
    }
}
