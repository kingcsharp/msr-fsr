using System;
using System.Collections.Generic;
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
}
