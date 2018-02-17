using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Procedures
{
    public class ProcedureView
    {
        public string SpecialRoot { get; set; }
        public string SpecialId { get; set; }
        public string Id { get; set; }
        public string ObjectId { get; set; }
        public string Verb { get; set; }
        public string Name { get; set; }
        public string SecurityLevel { get; set; }
        public string LockedBy { get; set; }
        public string CreatedBy { get; set; }
        public string Root { get; set; }
        public string CreatingCo { get; set; }
        public string Status { get; set; }
        public int? Rev { get; set; }
        public string LockedByName { get; set; }
        public string CreatingCoName { get; set; }
        public string VerbName { get; set; }
        public string VerbId { get; set; }
        public string ObjId { get; set; }
        public string CreatingDept { get; set; }
        public string DeptName { get; set; }
        public string SecurityName { get; set; }
        public Int16? IsSystem { get; set; }
        public string Comments { get; set; }
        public decimal? StepInAp { get; set; }
        public decimal? WipMsg { get; set; }
        public double? Duration { get; set; }
        public string DurationType { get; set; }
        public string SystemId { get; set; }
        public int? Threshold { get; set; }
        public bool IsActive { get; set; }
    }
}
