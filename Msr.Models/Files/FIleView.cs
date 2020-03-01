using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Files
{
    public class FileView
    {
        public string Id { get; set; }

        public string SortId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string SrcName { get; set; }

        public string SrcId { get; set; }

        public string CreatorId { get; set; }

        public string Boss { get; set; }
    }

    public class LatestFileView
    {

        [Key]
        public int ID { get; set; }

        public string LINKED_DOC_ID { get; set; }

        public string NAME { get; set; }

        public string CONTENTTYPE { get; set; }

        public string SERVER_PATH { get; set; }

        public int RN { get; set; }
    }
}
