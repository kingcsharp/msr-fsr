using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Msr.Models.Common;
using Msr.Models.PrePro;
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
            var objId = new SqlParameter("@objID", id ?? "0");
            var selecttype = type == null ? new SqlParameter("@type", DBNull.Value) : new SqlParameter("@type", type);
            var ntLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_FILES_SHOW_FOR_OBJECT  @objID, @type, @strNTLogin", objId, selecttype, ntLogin).ToList();

            return result;
        }

        public List<DocFile> GetSelectedRefFiles(string id, string type, string ntlogin)
        {
            var objId = new SqlParameter("@objID", id ?? "0");

            var selecttype = string.IsNullOrWhiteSpace(type) ? new SqlParameter("@type", DBNull.Value) : new SqlParameter("@type", type);

            var ntLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<DocFile>("EXEC Portal_SpFilesShowForObject  @objID, @type, @strNTLogin", objId, selecttype, ntLogin).ToList();

            return result;
        }
        public DocFile GetSelectedRefFile(string id)
        {
            var result = _dbContext.Database.SqlQuery<DocFile>($"select NAME AS SHOW,DOC_ID AS VALUE,SERVER_PATH as ServerPath, Name, FileUrl, FileKey,ContentType from A_DOCUMENTS where DOC_ID={id}").SingleOrDefault();

            return result;
        }

        public List<DocFile> GetProcedureSelectedRefFiles(string id, string ntlogin)
        {
            var objId = new SqlParameter("@procStepID", id ?? "0");

            var ntLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<DocFile>("EXEC Portal_ProcedureStepGetRefFilesDialog @procStepID, @strNTLogin", objId, ntLogin).ToList();

            return result;
        }
        public List<DocLink> GetDocByObjectId(string id)
        {
            if (id == null) return new List<DocLink>();

            var result = _dbContext.Database.SqlQuery<DocLink>($"SELECT * FROM dbo.A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = '{id}'").ToList();

            return result;
        }

        public List<DocLink> GetDocByObjectId(string id, string type)
        {
            if (id == null) return new List<DocLink>();

            var result = _dbContext.Database.SqlQuery<DocLink>($"SELECT * FROM dbo.A_V_DOCUMENTS_WITH_LINKED_ITEM WHERE OBJECT_ID = '{id}' AND TYPE ='{type}'").ToList();

            return result;
        }
        public List<PreProDockLink> GetPreProRefFileDocLinks(string id, string ntlogin)
        {
            var objectId = id ?? "0";

            var result = _dbContext.Database.SqlQuery<PreProDockLink>($"SELECT *,NAME AS SHOW, DOC_ID AS VALUE,DOC_TYPE AS TYPE  FROM A_V_PROCEDURE_STEP_DOCUMENT_DATA WHERE STEP_ID = {objectId}").ToList();

            return result;
        }
    }
}
