using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Roles
{
    public class RolesView
    {
        public string Id { get; set; }

        [Key]
        public string ObjectId { get; set; }

        public string SecurityLevel { get; set; }

        public string SecurityLevelName { get; set; }

        public string Comments { get; set; }

        public string TrainingIdRev { get; set; }

        public string Root { get; set; }

        public string RoleName { get; set; }

        public string Status { get; set; }

        public string LockedBy { get; set; }

        public string UnLockedBy { get; set; }

        public string CreatedBy { get; set; }

        public string CreatingCo { get; set; }

        public int? Revision { get; set; }

        public string WFSID { get; set; }

        public decimal Hidden { get; set; }

        public string Source { get; set; }

        public string LockedByName { get; set; }
    }
}
