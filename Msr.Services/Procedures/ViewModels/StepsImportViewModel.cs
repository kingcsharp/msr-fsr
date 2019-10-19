using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Procedures.ViewModels
{
    public class StepsImportViewModel
    {
        public StepsImportViewModel()
        {
            Messages = new List<string>();
        }

        public string PROCEDURE_ID { get; set; }

        public string STEP_TEXT { get; set; }

        public string PRINT_ORDER { get; set; }

        public string REF_DOC_ID { get; set; }

        public string COMMENT { get; set; }

        public int STEP_TIME { get; set; }

        public string EXTRA_NOTE1 { get; set; }

        public string SERIALIZE { get; set; }

        public string DefaultRoleId { get; set; }

        public string SUCCESS_MONITOR { get; set; }

        public string INTERNAL_LOCATION { get; set; }

        public string Title { get; set; }

        public string LOC_TYPE { get; set; }

        public List<string> Messages { get; set; }

        public static List<string> GetHeaderColumns()
        {
            return new List<string> { "PROCEDURE_ID", "STEP_TEXT", "PRINT_ORDER", "REF_DOC_ID", "COMMENT", "STEP_TIME", "EXTRA_NOTE1", "DEFAULT_ROLE_ID", "SERIALIZE", "SUCCESS_MONITOR", "INTERNAL_LOCATION", "LOC_TYPE", "Title" };
        }
    }
}
