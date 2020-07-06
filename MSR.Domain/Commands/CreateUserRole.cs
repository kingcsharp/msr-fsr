using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class CreateUserRole: Command
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public DateTime? CertificationFromDate { get; set; }
        public DateTime? CertificationToDate { get; set; }
    }
}
