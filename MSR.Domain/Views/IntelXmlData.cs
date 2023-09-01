using System;
using System.Collections.Generic;

namespace MSR.Domain.Views
{
	public class IntelXmlData
	{
		public ICollection<IntelWorkOrderPartView> WorkOrderParts { get; set; }
		public ICollection<IntelWorkOrderMonitorView> WorkOrderMonitors { get; set; }
	}
}

