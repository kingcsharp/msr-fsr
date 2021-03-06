
using System;

namespace MSR.Answer.API.V1.Models
{
    public class GetWorkflowStageRequest: BaseApiModel
    {
        public int? Id { get; set; }
        public bool? IsActive { get;set;}
        public string Name { get;set;}
        public DateTime? CreatedOn { get;set;}
        public string CreatedByName { get;set;}
        public DateTime? LastUpdatedOn { get;set;}
        public string LastUpdatedByName { get;set;}

    }
}
