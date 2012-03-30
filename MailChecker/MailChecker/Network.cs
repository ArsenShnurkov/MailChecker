using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Net;
using System.Web;

namespace MailChecker
{
    public class Network
    {
        [Flags]
        enum ConnectionState : int
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }
        private ConnectionState Description = 0;

        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        private static extern bool InternetGetConnectedState(ref ConnectionState lpdwFlags, int dwReserved);

        public bool Check_InternetAccess()
        {
            try
            {
                return InternetGetConnectedState(ref Description, 0);
            }
            catch
            {
                return false;
            }
        }
        public void Start_InDefaultBrowser(string url)
        {
            Process.Start(url);
        }
    }
}
