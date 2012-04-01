using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                return _Headers ?? (_Headers = HeaderDictionary.Parse(RawHeaders));
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
                return Headers["Content-Transfer-Encoding"]["charset"].NotEmpty(Headers["Content-Type"]["charset"]);
            }
        }

        public string Body { get; set; }

        internal void SetBody(string value)
        {
            if (ContentTransferEncoding.Is("quoted-printable"))
            {
                value = Utilities.DecodeQuotedPrintable(value, Utilities.ParseCharsetToEncoding(Charset));

            }
            else if (ContentTransferEncoding.Is("base64")
                //only decode the content if it is a text document
                  && ContentType.StartsWith("text/", StringComparison.OrdinalIgnoreCase)
                  && Utilities.IsValidBase64String(value))
            {
                string charset = Charset;
                if (Charset == "")
                    charset = System.Text.Encoding.Default.BodyName;
                value = System.Text.Encoding.GetEncoding(Charset).GetString(Convert.FromBase64String(value));

                ContentTransferEncoding = string.Empty;
            }

            Body = value;
        }
    }
}
