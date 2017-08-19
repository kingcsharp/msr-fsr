using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.ApprovalWorkflows
{
    public class ApprovalWorkflowsView
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Object_Id { get; set; }

        public string Stamp_Id { get; set; }

        public string Stamp_Name { get; set; }

        public string Creating_Co { get; set; }

        public bool? Hide { get; set; }
    }
}
