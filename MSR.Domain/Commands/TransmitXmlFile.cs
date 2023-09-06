using System;
using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
	public class TransmitXmlFile : Command
	{
		public int TransmissionId { get; set; }
	}
}