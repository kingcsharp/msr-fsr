using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace Msr.Services.Files.Procedures
{
    [StoredProcedure("A_SP_FILE_SAVE_UPLOAD_FILE")]
    public class SaveFileUploadProcedure
    {
        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "newID", Direction = ParameterDirection.Output)]
        public string NewId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 500, ParameterName = "msg", Direction = ParameterDirection.Output)]
        public string Message { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "docID")]
        public string DocId { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "oldDocID")]
        public string OldDocId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 4000, ParameterName = "NAME")]
        public string Name { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 4000, ParameterName = "Desc")]
        public string Desc { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 4000, ParameterName = "path")]
        public string Path { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 200, ParameterName = "contentType")]
        public string ContentType { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 50, ParameterName = "srcID")]
        public string SrcId { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 4000, ParameterName = "srcNAME")]
        public string SrcName { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 4000, ParameterName = "srcDesc")]
        public string SrcDesc { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 4000, ParameterName = "srcPath")]
        public string SrcPath { get; set; }

        [StoredProcedureParameter(SqlDbType.NVarChar, Size = 200, ParameterName = "srcContentType")]
        public string SrcContentType { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "srcChanged")]
        public string SrcChanged { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "docChanged")]
        public string DocChanged { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "dropSRC")]
        public string DropSrc { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, Size = 50, ParameterName = "strNTLogin")]
        public string NTLogin { get; set; }
    }
}
