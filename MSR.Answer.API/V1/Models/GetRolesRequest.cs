using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class GetRolesRequest: BaseApiModel
    {
        public int? Id { get;set;}
        public string Name { get;set;}
        public bool? IsCertificationRole { get;set;}
        public List<int>? ParentRoles { get; set; }
        public List<int>? AssignedUsers { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedByName { get;set;}
        public DateTime? LastUpdatedOn { get; set; }
        public string LastUpdatedByName { get; set; }


    }
}
