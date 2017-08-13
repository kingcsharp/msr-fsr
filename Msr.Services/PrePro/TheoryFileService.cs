using Msr.Models.PrePro;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.PrePro
{
   public class TheoryFileService
    {
        private readonly MsrDbContext _dbContext;

        public TheoryFileService()
        {
            _dbContext = new MsrDbContext();
        }


        //public List<TheoryFile> GetTheorySelectedFiles(string id, string type)
        //{
        //    var objID = new SqlParameter("@objID", id == null ? "0" : id);
        //    var selecttype = new SqlParameter();
        //    if (type == null)
        //    {
        //        selecttype = new SqlParameter("@type", DBNull.Value);
        //    }
        //    else
        //    {
        //        selecttype = new SqlParameter("@type", type);
        //    }
        //    //need to be dynamic
        //    var NTLogin = new SqlParameter("@strNTLogin", "1618");

        //    var result = _dbContext.Database.SqlQuery<SelectFile>("EXEC A_SP_THEORY_SELECT_FOR_THEORY_PARAGRAPHS  @objID, @type, @strNTLogin", objID, selecttype, NTLogin).ToList();

        //    return result;
        //}
    }
}
