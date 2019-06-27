using Msr.Models.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Documents
{
    public class DocHistory
    {
        public int Rev { get; set; }
        public string Rev_Info { get; set; }
        public DateTime? Approval_Date { get; set; }
    }
}
