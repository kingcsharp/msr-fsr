using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class UpdateUserRoleRequest
    {
        [Required]
        public int UserRoleId { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CertificationFromDate { get; set; }
        public DateTime? CertificationToDate { get; set; }
    }
}
