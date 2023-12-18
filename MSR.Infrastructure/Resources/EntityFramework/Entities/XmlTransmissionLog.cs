using System;
using System.ComponentModel.DataAnnotations.Schema;
using MSR.Domain.Models;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(XMLTransmissionLog))]
    public class XMLTransmissionLog: Entity 
    {
        public string Result { get; set; }
        public DateTime SubmittedOn { get; set; }
        public string TransmissionDetail { get; set; }
        public int WorkOrderId { get; set; }
        public string XmlLink { get; set; }
        public int WorkOrderPartId { get; set; }
        public string PartName { get; set; }
        public string SerialNumber { get; set; }
    }
}

