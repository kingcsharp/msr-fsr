using MSR.Answer.API.V1.Models.Paging;
using System;

namespace MSR.Answer.API.V1.Models
{
    public class ProcedureExportRequest: BaseApiModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string ProcedureTypeName { get; set; }
        public int? Duration { get; set; }
        public string DurationType { get; set; }
        public int? Revision { get; set; }
        public string CreatedFullName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string LastUpdatedFullName { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
