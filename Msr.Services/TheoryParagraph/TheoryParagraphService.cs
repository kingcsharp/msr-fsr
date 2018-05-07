using Msr.Models.Comman;
using Msr.Models.TheoryParagraphs;
using Msr.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.TheoryParagraph
{
    public class TheoryParagraphService
    {
        private readonly MsrDbContext _dbContext;

        public TheoryParagraphService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<TheoryParagraphView> GetTheoryParagraphsQueryable()
        {
            return _dbContext.TheoryParagraphViews;
        }

        public IQueryable<TheoryParagraphView> GetTheoryApprovedQueryable(string ntLogin)
        {
            var sql =
                $"EXEC A_SP_THEORY_SELECT_FOR_THEORY_PARAGRAPHS ' (NAME LIKE ''%%'' OR NAME is NULL ) AND  (ROOT LIKE ''%%'' OR ROOT is NULL ) AND  (CREATING_CO LIKE ''%%'' OR CREATING_CO is NULL )',NULL,' ORDER BY STATUS,SPECIAL_ROOT','{ntLogin}'";

            var result = _dbContext.Database.SqlQuery<TheoryParagraphView>(sql).ToList().AsQueryable();

            return result;
        }

        public List<SelectFile> GetStepTheories(string procestepId)
        {
            var sql = $"SELECT t.NAME AS Name,t.ID AS Id FROM A_PROCEDURE_STEP_THEORY_LINK l, A_V_THEORY_APPROVED_DATA t  where l.PROC_STEP_ID = '{procestepId}' and t.ID = l.THEORY_ID ";

            var result = _dbContext.Database.SqlQuery<SelectFile>(sql).ToList();

            return result;
        }
    }
}
