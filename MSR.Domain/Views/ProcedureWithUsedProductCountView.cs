using System;
using System.Collections.Generic;

namespace MSR.Domain.Views
{
    public class ProcedureWithUsedProductCountView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProcedureTypeId { get; set; }
        public int Revision { get; set; }
        public double Duration { get; set; }
        public string DurationType { get; set; }
        public string ProcedureTypeName { get; set; }
        public int CountProductsUsing { get; set; }

        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByFirstName { get; set; }
        public string CreatedByLastName { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public int LastUpdatedById { get; set; }
        public string LastUpdatedByFirstName { get; set; }
        public string LastUpdatedByLastName { get; set; }
    }
}
