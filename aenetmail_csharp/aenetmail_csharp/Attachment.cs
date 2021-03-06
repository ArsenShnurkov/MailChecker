﻿using System;
using System.Text;

namespace aenetmail_csharp
{
    public class Attachment : ObjectWHeaders
    {
        public string Filename
        {
            get { return Headers["Content-Disposition"]["filename"].NotEmpty(Headers["Content-Disposition"]["name"]); }
        }

        private string _ContentDisposition;
        private string ContentDisposition
        {
            get { return _ContentDisposition ?? (_ContentDisposition = Headers["Content-Disposition"].Value.ToLower()); }
        }

        public bool OnServer { get; internal set; }

        internal bool IsAttachment
        {
            get
            {
                return ContentDisposition == "attachment" || ContentDisposition == "inline";
            }
        }

        public void Save(string filename)
        {
            using (var file = new System.IO.FileStream(filename, System.IO.FileMode.Create))
                Save(file);
        }

        public void Save(System.IO.Stream stream)
        {
            var data = GetData();
            stream.Write(data, 0, data.Length);
        }

        public byte[] GetData()
        {
            byte[] data;
            if (ContentTransferEncoding.Is("base64") && Utilities.IsValidBase64String(Body))
            {
                try
                {
                    data = Convert.FromBase64String(Body);
                }
                catch (Exception)
                {
                    data = Encoding.GetBytes(Body);
                }
            }
            else
            {
                data = Encoding.GetBytes(Body);
            }
            return data;
        }

    }
}