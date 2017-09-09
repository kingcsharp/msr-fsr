using Msr.Models.Monitor;
using Msr.Repositories;
using System.Linq;

namespace Msr.Services.monitor
{
   public class MonitorService
    {
        private readonly MsrDbContext _dbContext;

        public MonitorService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<MonitorView> GetMonitorsQueryable()
        {
            return _dbContext.MonitorViews;
        }
    }
}
