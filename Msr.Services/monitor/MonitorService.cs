using Msr.Models.Monitor;
using Msr.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.monitor
{
   public class MonitorService
    {
        private readonly MsrDbContext _dbContext;

        public MonitorService()
        {
            _dbContext = new MsrDbContext();
        }
        public IQueryable<MonitorView> GetLocationsQueryable()
        {
            return _dbContext.MonitorViews;
        }
    }
}
