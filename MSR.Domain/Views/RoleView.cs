
using System.Collections.Generic;
using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Views
{
    public class RoleView: TrackableModel
    {
        public string Name { get; set; }
        public bool? IsCertificationRole { get; set; }
        public ICollection<RoleView> ParentRoles { get; set; }
        public bool HasAssignedUsers { get; set; }
    }
}
