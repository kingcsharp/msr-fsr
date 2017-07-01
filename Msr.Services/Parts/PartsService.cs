using Msr.Models.Parts;
using Msr.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace Msr.Services.Parts
{
    public class PartsService
    {
        private readonly MsrDbContext _dbContext;

        public PartsService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<PartsView> GetPartsQueryable()
        {
            return _dbContext.PartsViews;
        }

    }
}
