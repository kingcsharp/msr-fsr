using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Archive
{
    public class ArchiveData
    {
        public string FileName { get; set; }
        public string DownloadURL { get; set; }
        public float FileSize { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
