﻿using System;

namespace aenetmail_csharp
{
    public abstract class ObjectWHeaders
    {
        public string RawHeaders { get; internal set; }
        private HeaderDictionary _Headers;
        public HeaderDictionary Headers
        {
            get
            {
                return _Headers ?? (_Headers = HeaderDictionary.Parse(RawHeaders, _DefaultEncoding));
            }
            internal set
            {
                _Headers = value;
            }
        }

        public string ContentTransferEncoding
        {
            get { return Headers["Content-Transfer-Encoding"].Value ?? string.Empty; }
            internal set
            {
                Headers.Set("Content-Transfer-Encoding", new HeaderValue(value));
            }
        }

        public string ContentType
        {
            get { return Headers["Content-Type"].Value ?? string.Empty; }
            internal set
            {
                Headers.Set("Content-Type", new HeaderValue(value));
            }
        }

        public string Charset
        {
            get
            {
                return Headers["Content-Transfer-Encoding"]["charset"].NotEmpty(
                Headers["Content-Type"]["charset"]
                );
            }
        }

        System.Text.Encoding _DefaultEncoding;
        System.Text.Encoding _Encoding;
        public System.Text.Encoding Encoding
        {
            get
            {
                return _Encoding ?? (_Encoding = Utilities.ParseCharsetToEncoding(Charset, _DefaultEncoding));
            }
            set
            {
                _DefaultEncoding = value;
                if (_Encoding != null) //Encoding has been initialized from the specified Charset
                    _Encoding = value;
            }
        }

        public string Body { get; set; }

        internal void SetBody(string value)
        {
            if (ContentTransferEncoding.Is("quoted-printable"))
            {
                value = Utilities.DecodeQuotedPrintable(value, Encoding);

            }
            else if (ContentTransferEncoding.Is("base64")
                //only decode the content if it is a text document
                  && ContentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase)
                  && Utilities.IsValidBase64String(value))
            {
                var data = Convert.FromBase64String(value);
                using (var mem = new System.IO.MemoryStream(data))
                using (var str = new System.IO.StreamReader(mem, Encoding))
                    value = str.ReadToEnd();

                ContentTransferEncoding = string.Empty;
            }
            SetBodyParsed(value);

            Body = value;
        }
        public string BodyParsed { get; private set; }

        internal void SetBodyParsed(string value)
        {
            value = Utilities.ConvertHTML2Text(value);
            BodyParsed = value;
        }
    }
}