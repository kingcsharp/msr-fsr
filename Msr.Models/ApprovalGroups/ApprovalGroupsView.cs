using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.ApprovalGroups
{
    public class ApprovalGroupsView
    {
        public string Id { get; set; }

        public string ObjectTable { get; set; }

        public string ObjId { get; set; }

        public string ObjDesc { get; set; }

        public DateTime? Drcm { get; set; }

        public string ModBy { get; set; }

        public string CoPartNum { get; set; }

        public string PartType { get; set; }

        public string PartCo { get; set; }

        public string LockedBy { get; set; }

        public string UnLockedBy { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public string Root { get; set; }

        public string RevInfo { get; set; }

        public string CreatingCo { get; set; }

        public string Status { get; set; }

        public int? Rev { get; set; }

        public string WfsId { get; set; }

        public string Name { get; set; }

        public bool? Hide { get; set; }
    }
}
