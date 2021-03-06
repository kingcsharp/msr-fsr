using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class GetWorkflowModel : PagingCommand
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int[]? MemberStages { get; set; }
        public int[]? ActivityMaps { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string LastUpdatedByName { get; set; }
    }
}
