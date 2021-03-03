using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class GetRoles : PagingCommand
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool? IsCertificationRole { get; set; }
        public List<int>? ParentRoles { get; set; }
        public List<int>? AssignedUsers { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public string LastUpdatedByName { get; set; }
    }
}
