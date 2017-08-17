using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Administration.Messages
{
    public class UpdateAssignRoleToJobRequest
    {
        public UpdateAssignRoleToJobRequest()
        {
            RoleToJobItems = new List<UpdateAssignRoleToJobItem>();
        }

        public List<UpdateAssignRoleToJobItem> RoleToJobItems { get; set; }
    }

    public class UpdateAssignRoleToJobItem
    {
        public int? RoleId { get; set; }
        public int? CoId { get; set; }
        public string Job { get; set; }
    }
}
