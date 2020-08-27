
using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    public class CreateRoleRequest
    {
        public string Name { get; set; }
        public bool IsCertificationRole { get; set; }
        public ICollection<int> ParentRoleIds { get; set; }
    }
}
