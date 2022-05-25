using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class FileContentView
    {
        public byte[] data { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
