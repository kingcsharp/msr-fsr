using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire.Annotations;

namespace Answer.Web.ViewModel.Wip
{
    public static class Location
    {
        public const string PhoneNumber = "xxx-xxx-xxxx";
    }

    public class AddNcrNotificationEmailViewModel
    {
        public DateTime DateReported { get; set; }
        public string Technician { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public string SerialNumber { get; set; } 
        public string WorkOrderNumber { get; set; }
        public string DescriptionOfNonCompliance { get; set; }
    }
}