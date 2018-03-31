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
    }
}
