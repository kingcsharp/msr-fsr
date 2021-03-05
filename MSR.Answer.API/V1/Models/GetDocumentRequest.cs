using System;

namespace MSR.Answer.API.V1.Models
{
    public class GetDocumentRequest: BaseApiModel
    {
        public int? Id { get; set; }
        public string Name { get;set;}
        public int? Revision { get;set;}
        public DateTime? LastUpdatedOn { get;set;}
        public string LastUpdatedFullName { get;set;}

    }
}
