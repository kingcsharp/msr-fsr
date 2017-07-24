using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Regions
{
    public class RegionsView
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ModBy { get; set; }

        public DateTime? Drcm { get; set; }

        public string ObjectId { get; set; }

        public string ObjId { get; set; }

        public string LockedBy { get; set; }

        public string UnLockedBy { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string Root { get; set; }

        public string RevInfo { get; set; }

        public string CreatedCo { get; set; }

        public string Status { get; set; }

        public int Rev { get; set; }

        public string WfsId { get; set; }

        public string LockedByName { get; set; }

        public string CreatingCoName { get; set; }
    }
}
