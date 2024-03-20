using MSR.Domain.Commanding.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models.Query
{
    public class PartExportQueryFilters
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public EnumSegregationType[]? SegregationType { get; set; }
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public bool? IsKit { get; set; }
        public bool? IsActive { get; set; }
        public int? MaximumCycles { get; set; }

        public PartExportQueryFilters(int? id, string name, EnumSegregationType[]? segregationType, string partNumber, string oemPartNumber, bool? isKit, bool? isActive, int? maximumCycles)
        {
            Id = id;
            Name = name;
            SegregationType = segregationType;
            PartNumber = partNumber;
            OEMPartNumber = oemPartNumber;
            IsKit = isKit;
            IsActive = isActive;
            MaximumCycles = maximumCycles;
        }
    }
}
