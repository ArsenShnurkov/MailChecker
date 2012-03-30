using System;

namespace aenetmail_csharp.Imap
{
    public class MessageEventArgs : EventArgs
    {
        public int MessageCount { get; set; }
        internal ImapClient Client { get; set; }
    }
}
