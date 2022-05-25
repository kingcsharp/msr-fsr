using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class ProcedureExportView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProcedureType { get; set; }
        public int Revision { get; set; }
        public double Duration { get; set; }
        public string DurationType { get; set; }
        public string Comments { get; set; }
    }
}
