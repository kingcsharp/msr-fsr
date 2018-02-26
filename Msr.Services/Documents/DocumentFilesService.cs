using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Msr.Models.Common;
using Msr.Repositories;
using Msr.Services.Documents.ViewModels;

namespace Msr.Services.Documents
{
    public class DocumentFilesService
    {
        private readonly MsrDbContext _dbContext;

        public DocumentFilesService()
        {
            _dbContext = new MsrDbContext();
        }
        public List<SelectFile> GetSelectedFiles(string id, string type, string ntlogin)
        {
            var objID = new SqlParameter("@objID", id == null ? "0" : id);
            var selecttype = new SqlParameter();
            if (type == null)
            {
                selecttype = new SqlParameter("@type", DBNull.Value);
            }
            else
            {
                selecttype = new SqlParameter("@type", type);
            }
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_FILES_SHOW_FOR_OBJECT  @objID, @type, @strNTLogin", objID, selecttype, NTLogin).ToList();

            return result;
        }

        public List<DocFile> GetSelectedRefFiles(string id, string type, string ntlogin)
        {
            var objID = new SqlParameter("@objID", id == null ? "0" : id);

            SqlParameter selecttype = new SqlParameter();

            if (string.IsNullOrWhiteSpace(type))
            {
                selecttype = new SqlParameter("@type", DBNull.Value);
            }
            else
            {
                selecttype = new SqlParameter("@type", type);
            }

            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<DocFile>("EXEC Portal_SpFilesShowForObject  @objID, @type, @strNTLogin", objID, selecttype, NTLogin).ToList();

            return result;
        }
        public DocFile GetSelectedRefFile(string id)
        {
            var result = _dbContext.Database.SqlQuery<DocFile>($"select NAME AS SHOW,DOC_ID AS VALUE,SERVER_PATH as ServerPath from A_DOCUMENTS where DOC_ID={id}").SingleOrDefault();

            return result;
        }

        public List<DocFile> GetProcedureSelectedRefFiles(string id, string ntlogin)
        {
            var objId = new SqlParameter("@procStepID", id ?? "0");

            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<DocFile>("EXEC Portal_ProcedureStepGetRefFilesDialog @procStepID, @strNTLogin", objId, NTLogin).ToList();

            return result;
        }
    }
}
