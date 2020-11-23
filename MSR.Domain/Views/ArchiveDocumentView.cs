using System;

namespace MSR.Domain.Views
{
    public class ArchiveDocumentView
    {
        public string FileName { get; set; }
        public string DownloadURL { get; set; }
        public float FileSize { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
