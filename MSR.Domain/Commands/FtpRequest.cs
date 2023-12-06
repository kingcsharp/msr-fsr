using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class FtpRequest: Command
    {
        public int workOrderId { get; set; }

        public string XmlContent { get; set; }

        public string XmlLink { get; set; }
    }
}
