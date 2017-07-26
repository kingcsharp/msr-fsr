using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Procedure
{
    public class ProcedureTypesView
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public string ObjectId { get; set; }
        public string LockedBy { get; set; }
        public string UnLockedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string Root { get; set; }
        public string RevInfo { get; set; }
        public string CreatingCo { get; set; }
        public string Status { get; set; }
        public int? Revision { get; set; }
        public string WFSID { get; set; }
        public string LockedByName { get; set; }
        public string CreatingCoName { get; set; }
        public string ApprovalActivity { get; set; }
        public string ObjId { get; set; }
        public string VerbType { get; set; }
        public string VerbTypeName { get; set; }
    }
}
