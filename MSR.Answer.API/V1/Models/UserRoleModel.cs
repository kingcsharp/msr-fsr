using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MSR.Answer.API.V1.Models
{
    public class UserRoleModel
    {
        [Required]
        public int UserRoleId { get; set; }
        public int? UserId { get; set; }
        public int? RoleId { get; set; }
        public DateTime? CertificationFromDate { get; set; }
        public DateTime? CertificationToDate { get; set; }
    }
}
