using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net.Sockets;

namespace aenetmail_csharp
{
    public abstract class TextClient : IDisposable
    {
        protected TcpClient _Connection;
        protected Stream _Stream;

        public bool IsConnected { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public bool IsDisposed { get; private set; }
        public System.Text.Encoding Encoding { get; set; }

        public TextClient()
        {
            //Encoding = System.Text.Encoding.UTF8;
            Encoding = System.Text.Encoding.Default;
        }

        internal abstract void OnLogin(string username, string password);
        internal abstract void OnLogout();
        internal abstract void CheckResultOK(string result);

        protected virtual void OnConnected(string result)
        {
            CheckResultOK(result);
        }

        protected virtual void OnDispose() { }

        public void Connect(string host, int port, bool useSsl, bool skipSslValidation)
        {
            System.Net.Security.RemoteCertificateValidationCallback validateCertificate = null;
            if (skipSslValidation)
                validateCertificate = (sender, cert, chain, err) => true;
            try
            {
                _Connection = new TcpClient(host, port);
                _Stream = _Connection.GetStream();
                if (useSsl)
                {
                    System.Net.Security.SslStream sslStream;
                    if (validateCertificate != null)
                        sslStream = new System.Net.Security.SslStream(_Stream, false, validateCertificate);
                    else
                        sslStream = new System.Net.Security.SslStream(_Stream, false);
                    _Stream = sslStream;
                    sslStream.AuthenticateAsClient(host);
                }

                OnConnected(GetResponse());

                IsConnected = true;
            }
            catch (Exception)
            {
                IsConnected = false;
                Utilities.TryDispose(ref _Stream);
                throw;
            }
        }
        public void Login(string username, string password)
        {
            if (!IsConnected)
                throw new Exception("You must connect first!");

            IsAuthenticated = false;
            OnLogin(username, password);
            IsAuthenticated = true;
        }
        public void Logout()
        {
            IsAuthenticated = false;
            OnLogout();
        }

        protected void CheckConnectionStatus()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(this.GetType().Name);
            if (!IsConnected)
                throw new Exception("You must connect first!");
            if (!IsAuthenticated)
                throw new Exception("You must authenticate first!");
        }

        protected virtual void SendCommand(string command)
        {
            var bytes = System.Text.Encoding.Default.GetBytes(command + "\r\n");
            _Stream.Write(bytes, 0, bytes.Length);
        }

        protected string SendCommandGetResponse(string command)
        {
            SendCommand(command);
            return GetResponse();
        }

        protected virtual string GetResponse()
        {
            byte b;
            using (var mem = new System.IO.MemoryStream())
            {
                while (true)
                {
                    b = (byte)_Stream.ReadByte();
                    if ((b == 10 || b == 13))
                    {
                        if (mem.Length > 0 && b == 10)
                        {
                            return Encoding.GetString(mem.ToArray());
                        }
                    }
                    else
                    {
                        mem.WriteByte(b);
                    }
                }
            }
        }

        protected void SendCommandCheckOK(string command)
        {
            CheckResultOK(SendCommandGetResponse(command));
        }

        public void Disconnect()
        {
            if (IsAuthenticated)
                Logout();

            Utilities.TryDispose(ref _Stream);
            Utilities.TryDispose(ref _Connection);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Disconnect();

                try
                {
                    OnDispose();
                }
                catch (Exception) { }

                IsDisposed = true;
                _Stream = null;
                _Connection = null;
            }
        }
    }
}
