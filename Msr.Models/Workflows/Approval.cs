using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Workflows
{
    public class Approval
    {
        public DateTime? ApprovalDate { get; set; }
        public string Status { get; set; }
        public string FullName { get; set; }
        public string ApproveDate { get; set; }
        public string Position { get; set; }
    }
}
