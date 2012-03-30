using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aenetmail_csharp
{
    public interface IMailClient : IDisposable
    {
        int GetMessageCount();
        MailMessage GetMessage(int index, bool headersonly = false);
        MailMessage GetMessage(string uid, bool headersonly = false);
        void DeleteMessage(string uid);
        void DeleteMessage(MailMessage msg);
        void Disconnect();
    }
}
