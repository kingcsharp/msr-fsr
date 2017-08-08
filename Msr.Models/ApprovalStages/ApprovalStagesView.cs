using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.ApprovalStages
{
    public class ApprovalStagesView
    {
        public string Id { get; set; }

        public string WfGroupId { get; set; }

        public string GroupName { get; set; }

        public string CreatingCo { get; set; }

        public string StageName { get; set; }       

        public bool? Hide { get; set; }

        public bool? GroupHide { get; set; }
    }
}
