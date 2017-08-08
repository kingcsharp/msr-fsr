using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Objects
{
    public class ObjectView
    {
        public string Id { get; set; }

        public string ObjectRefId { get; set; }

        public string ObjectTable { get; set; }

        public string ObjectId { get; set; }

        public string ObjectDesc { get; set; }

        public string Status { get; set; }

        public string CreatingCo { get; set; }

        public int? Rev { get; set; }

        public string CreatingCoName { get; set; }
    }
}
