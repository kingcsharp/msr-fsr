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
    }
}
