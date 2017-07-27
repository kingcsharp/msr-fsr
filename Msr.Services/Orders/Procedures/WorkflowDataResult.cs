using System;

namespace Msr.Services.Orders.Procedures
{
    public class WorkflowDataResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Hide { get; set; }
        public DateTime DRCM { get; set; }
        public string ModBy { get; set; }
        public string Object_Id { get; set; }
        public string Stamp_Name { get; set; }
        public string Stamp_Id { get; set; }
    }
}
