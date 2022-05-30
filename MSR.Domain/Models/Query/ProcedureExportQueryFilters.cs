using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models.Query
{
    public class ProcedureExportQueryFilters
    {
        public int? Id { get;}
        public string Name { get;}
        public string ProcedureTypeName { get;}
        public int? Duration { get; }
        public string DurationType { get; }
        public int? Revision { get;}
        public string CreatedFullName { get; }
        public DateTime? CreatedOn { get;}
        public string LastUpdatedFullName { get; }
        public DateTime? LastUpdatedOn { get;}

        public ProcedureExportQueryFilters(int? id, string name, string procedureTypeName, int? duration, string durationType, int? revision, 
                                            string createdFullName, DateTime? createdOn, string lastUpdatedFullName, DateTime? lastUpdatedOn)
        {
            Id = id;
            Name = name;
            ProcedureTypeName = procedureTypeName;
            Duration = duration;
            DurationType = durationType;
            Revision = revision;
            CreatedFullName = createdFullName;
            CreatedOn = createdOn;
            LastUpdatedFullName = lastUpdatedFullName;
            LastUpdatedOn = lastUpdatedOn;
        }
    }
}
