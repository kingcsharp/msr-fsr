using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Models.Common;
using Msr.Repositories;
using System.Data.Entity;


namespace Msr.Infrastructure.Common
{
    public class DocumentFilesService
    {
        private readonly MsrDbContext _dbContext;

        public DocumentFilesService()
        {
            _dbContext = new MsrDbContext();
        }
        public List<SelectFile> GetSelectedFiles(string id, string type)
        {
            var objID = new SqlParameter("@objID", id);
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
            var NTLogin = new SqlParameter("@strNTLogin", "1618");

            var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_FILES_SHOW_FOR_OBJECT  @objID, @type, @strNTLogin", objID, selecttype, NTLogin).ToList();

            return result;
        }
    }
}
