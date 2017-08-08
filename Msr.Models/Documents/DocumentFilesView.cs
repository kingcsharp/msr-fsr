using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Documents
{
    public class DocumentFilesView
    {
        [Key]
        public string DOC_ID { get; set; }
        public string DOC_NAME { get; set; }
        public string TYPE { get; set; }
        public string OBJECT_ID { get; set; }
    }
}
