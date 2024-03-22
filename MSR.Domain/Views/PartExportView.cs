using MSR.Domain.Commanding.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class PartExportView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SegregationType { get; set; }
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public bool IsKit { get; set; }
        public bool IsActive { get; set; }
        public int? MaximumCycles { get; set; }

    }
}
