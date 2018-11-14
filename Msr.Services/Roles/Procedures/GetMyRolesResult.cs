
using System;

namespace Msr.Services.Roles.Procedures
{
    public class GetMyRolesResult
    {
        public string Role_Id { get; set; }

        public string Role { get; set; }

        public string Person { get; set; }

        public string Status { get; set; }

        public string Role_Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
