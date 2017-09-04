using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Procedures.Messages
{
   public class ProcedureEditObjectView
    {
        public string ID { get; set; }

        public string PROCEDURE_ID { get; set; }

        public string STEP_ID { get; set; }

        public string APPROVED_OBJECT_ID { get; set; }

        public Double? QTY { get; set; }

        public string QTY_TYPE { get; set; }

        public string RELATIONSHIP { get; set; }

        public string OBJ_REF_ID { get; set; }

        public string OBJ_TABLE { get; set; }

        public string OBJ_ID { get; set; }
  
        public string OBJ_DESC { get; set; }

        public string LABOR_ROLE { get; set; }

       public string APPROVED_PROC_ID { get; set; }

        public string PROC_HIST_ID { get; set; }

        public string PROC_OBJ_ID { get; set; }

        public string PROC_STEP_ID { get; set; }

        public string strNTLogin { get; set; }
    }
}
