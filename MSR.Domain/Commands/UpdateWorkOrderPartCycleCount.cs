using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class UpdateWorkOrderPartCycleCount: Command
    {
        public string PartNumber { get; }
        public string SerialNumber { get; }
        public int CycleCount { get;  }

        public UpdateWorkOrderPartCycleCount(string partNumber, string serialNumber, int cycleCount)
        {
            PartNumber = partNumber;
            SerialNumber = serialNumber;
            CycleCount = cycleCount;
        }
    }
}
