using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Documents
{
    public class DocumentView
    {
        public string SpecialRoot { get; set; }

        public string SpecialID { get; set; }

        public string Id { get; set; }

        public string ObjectId { get; set; }

        public string SecurityLevel { get; set; }

        public string CreatingDept { get; set; }

        public string ObjId { get; set; }

        public string LockedBy { get; set; }

        public string CreatedBy { get; set; }

        public string Root { get; set; }

        public string CreatingCo { get; set; }

        public string Name { get; set; }

        public string CreatingCoName { get; set; }

        public string DeptName { get; set; }

        public int? Rev { get; set; }

        public string Status { get; set; }

        public string LockedByName { get; set; }

        public string SecurityName { get; set; }

        public DateTime? ApprovalDate { get; set; }

        public string Comments { get; set; }

        public string ReferenceFiles { get; set; }
    }
}
