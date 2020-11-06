using System;
using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class RolesUsersView
    {
        public RolesUsersView()
        {

        }
        public int Id { get; set; }
        public int RoleId { get; set; }

        public DateTime? CertificationFromDate { get; set; }

        public DateTime? CertificationToDate { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
