using MSR.Domain.Commanding;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class CreateRole: Command
    {
        public string Name { get; set; }
        public bool IsCertificationRole { get; set; }
        public ICollection<int> ParentRoleIds { get; set; }
    }
}
