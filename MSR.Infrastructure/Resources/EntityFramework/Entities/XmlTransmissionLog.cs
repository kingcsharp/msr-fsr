using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MSR.Infrastructure.Resources.EntityFramework.Entities
{
    [Table(nameof(XmlTransmissionLog))]
    public class XmlTransmissionLog: Entity 
    {
        public string Result { get; set; }
        public DateTime SubmittedOn { get; set; }
        public string TransmissionDetail { get; set; }
        public int WorkOrderId { get; set; }
        public string XmlLink { get; set; }
    }
}

