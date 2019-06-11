using System.Collections.Generic;

namespace Msr.Services.Procedures.ViewModels
{
    public class ProcedureImportViewModel
    {
        public ProcedureImportViewModel()
        {
            Messages = new List<string>();
        }

        public string ProcedureId { get; set; }

        public string ProcedureName { get; set; }

        public string AnsId { get; set; }

        public string ProcType { get; set; }

        public string NtLogin { get; set; }

        public bool Processed { get; set; }

        public List<string> Messages { get; set; }

        public static List<string> GetHeaderColumns()
        {
            return new List<string> { "PROCEDURE_ID", "PROCEDURE_NAME", "ANS_ID", "PROC_TYPE_ID" };
        }
    }
}
