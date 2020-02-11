using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire.Annotations;
using Msr.Services.Orders.Procedures;

namespace Answer.Web.ViewModel.Wip
{

    public class AddNcrNotificationEmailViewModel
    {
        public DateTime DateReported { get; set; }
        public string Technician { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string SerialNumber { get; set; } 
        public string WorkOrderNumber { get; set; }

        public List<NcrLocationViewModel> NcrLocationViewModels { get; set; }

        public List<MonitorTemplateResult> MonitorTemplateResults { get; set; }
    }
}