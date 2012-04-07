using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aenetmail_csharp;

namespace MailChecker
{
    public class Account
    {
        public class UnseenMessage
        {
            public string MessageId { get; set; }
            public string FromDisplayName { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public int AttachmentsCount { get; set; }
            public DateTime Date { get; set; }
            public bool Alerted { get; set; }

            public UnseenMessage(string message_id, string from_display_name, string subject, string body, int attachment_count, DateTime date)
            {
                MessageId = message_id;
                FromDisplayName = from_display_name;
                Subject = subject;
                Body = body;
                AttachmentsCount = attachment_count;
                Date = date;
            }
        }

        public string Host { get; set; }
        public int Port { get; set; }
        public bool UseSsl { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public bool Finished { get; set; }
        public List<UnseenMessage> UnseenMessages { get; set; }

        public Account(string email, string username, string password)
        {
            Email = email;
            Username = username;
            Password = password;
        }
        public void Check_ForUnseenMails()
        {
            try
            {
                //Connect
                Status = MainForm.myIni.Get_LanguageValue("messages", "connect_to").Replace("%1", Host);
                ImapClient myImapClient = new ImapClient(Host, Port, Username, Password, UseSsl, !UseSsl, ImapClient.AuthMethods.Login);
                try
                {
                    myImapClient.Connect();
                }
                catch
                {
                    Status = MainForm.myIni.Get_LanguageValue("messages", "host_unavailable").Replace("%1", Host);
                    return;
                }

                //Login
                try
                {
                    myImapClient.Login();
                }
                catch
                {
                    Status = MainForm.myIni.Get_LanguageValue("messages", "login_failed").Replace("%1", Host);
                    return;
                }

                //Retrieve unseen messages
                Status = MainForm.myIni.Get_LanguageValue("messages", "checking_mails").Replace("%1", Email);
                UnseenMessages = new List<UnseenMessage>();
                List<MailMessage> myMessages = Fetch_UnseenMails(myImapClient);
                foreach (MailMessage message in myMessages)
                {
                    string from_display_name = message.From.DisplayName;
                    string subject = message.Subject;
                    string body = message.Body;

                    UnseenMessages.Add(new UnseenMessage(message.MessageID, from_display_name, subject, body, message.Attachments.Count, message.Date));
                }

                Status = "";
                Finished = true;
            }
            catch (Exception e)
            {
                Status = e.Message;
            }
        }
        private List<MailMessage> Fetch_UnseenMails(ImapClient client)
        {
            try
            {
                List<MailMessage> myMsg = new List<MailMessage>();

                client.SelectMailbox("INBOX");
                System.Lazy<MailMessage>[] myMessages = client.SearchMessages(aenetmail_csharp.Imap.SearchCondition.Unseen());
                foreach (System.Lazy<MailMessage> message in myMessages)
                    myMsg.Add(message.Value);
                return myMsg;
            }
            catch
            {
                List<MailMessage> myMessages = new List<MailMessage>();
                return myMessages;
            }
        }
    }
}
