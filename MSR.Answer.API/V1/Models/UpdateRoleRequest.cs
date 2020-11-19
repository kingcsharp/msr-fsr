
using System.Collections.Generic;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateRoleRequest : CreateRoleRequest
    {
        public int Id { get; set; }
    }
}
