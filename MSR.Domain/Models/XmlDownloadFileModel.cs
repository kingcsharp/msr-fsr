using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSR.Domain.Models
{
    public class XmlDownloadFileModel
    {
        public string Name { get; set; }
        public Stream FileContent { get; set; }
    }
}
