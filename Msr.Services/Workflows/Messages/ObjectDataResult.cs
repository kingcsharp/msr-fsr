using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Workflows.Messages
{
    public class ObjectDataResult
    {
        public string Id { get; set; }

        public string Obj_Table { get; set; }

        public string Obj_Id { get; set; }

        public string Obj_Desc { get; set; }

        public DateTime Drcm { get; set; }

        public string Status { get; set; }

        public string Approval_Activity { get; set; }
    }
}
