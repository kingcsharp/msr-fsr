using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class CreateHelpPageRoleRequest
    {
        public int HelpPageId { get; set; }
        public int RoleId { get; set; }
    }
}
