using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.ApprovalWorkflows.Procedures
{
    [StoredProcedure("Portal_DeleteDocLinkByObjId")]
    public class DeleteDocumentProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 500, ParameterName = "ObjId")]
        public string ObjectId { get; set; }


    }
}
