using System.Linq;
using Msr.Models.Tasks;
using Msr.Models.Users;
using Msr.Repositories;

namespace Msr.Services.Orders
{
   public class TaskService
    {
        private readonly MsrDbContext _dbContext;

        public TaskService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<MonitorResult> GetCompanyQueryable()
        {
            return _dbContext.MonitorResults;
        }
    }
}
