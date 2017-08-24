using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Msr.Models.Common;
using Msr.Repositories;

namespace Msr.Services.Documents
{
    public class DocumentFilesService
    {
        private readonly MsrDbContext _dbContext;

        public DocumentFilesService()
        {
            _dbContext = new MsrDbContext();
        }
        public List<SelectFile> GetSelectedFiles(string id, string type,string ntlogin)
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
            //need to be dynamic
            var NTLogin = new SqlParameter("@strNTLogin", ntlogin);

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_FILES_SHOW_FOR_OBJECT  @objID, @type, @strNTLogin", objID, selecttype, NTLogin).ToList();

            return result;
        }
    }
}
