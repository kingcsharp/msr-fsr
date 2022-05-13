using MSR.Domain.Models.BaseModels;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class Procedure : TrackableModel
    {
        public string Name { get; set; }
        public int ProcedureTypeId { get; set; }
        public bool IsRelatedToAProduct { get; set; }
        public string CreatorCompany { get; set; }
        public string CreatedByDepartmentName { get; set; }
        public int Revision { get; set; }
        public string Comments { get; set; }
        public double Duration { get; set; }
        public string DurationType { get; set; }
        public ProcedureType ProcedureType { get; set; }
        public List<FileModel> ReferenceFiles { get; set; }
        public string ApprovalStatus { get; set; }
        public string TagType { get; set; }


    }
}
