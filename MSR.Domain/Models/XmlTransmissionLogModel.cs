using System;
namespace MSR.Domain.Models
{
	public class XmlTransmissionLogModel
	{
        	public int? Id { get; set; }
	        public string Result { get; set; }
	        public DateTime SubmittedOn { get; set; }
	        public string TransmissionDetail { get; set; }
	        public int WorkOrderId { get; set; }
			public int WorkOrderPartId { get; set; }
			public string PartName { get; set; }
			public string SerialNumber { get; set; }
			public string XmlLink { get; set; }
   	}
}

