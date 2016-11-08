using System.Data.SqlClient;
using System.Linq;
using Msr.Models.Tasks;
using Msr.Repositories;
using Msr.Services.Orders.Messaging;
using Msr.Services.Tasks.Messaging;

namespace Msr.Services.Orders
{
   public class TaskService
    {
        private readonly MsrDbContext _dbContext;

        public TaskService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<MonitorResult> GetTaskWithMonitors()
        {
            return _dbContext.MonitorResults;
        }

       public int? CheckHasMonitors(string fillId)
       {
            var fileIdParm = new SqlParameter("@fillID", fillId);

            var tasks = _dbContext.Database.SqlQuery<GetTaskWithMonitorsResult>("Portal_GetTaskWithMonitors @fillID", fileIdParm).ToList();

           if (tasks.Any())
           {
               return 1;
           }

           return null;
       }

        public int CheckHasNcr(string partId)
        {
            var fileIdParm = new SqlParameter("@partId", partId);

            var result = _dbContext.Database.SqlQuery<int>("PortalHasNcr @partId", fileIdParm).Single();

            return result;
        }


        public MonitorHistoryResponse GetTaskWithMonitors(string fileId)
        {
            var response = new MonitorHistoryResponse();

            var fileSearch = _dbContext.FileSearchView.Single(x => x.Id == fileId);

            response.SupName = fileSearch.SupName;
            response.FillObjDesc = fileSearch.FillObjDesc;
            response.PurchItemId = fileSearch.PurchItemId;

            var fileIdParm = new SqlParameter("@fillID", fileId);

            var tasks = _dbContext.Database.SqlQuery<GetTaskWithMonitorsResult>("Portal_GetTaskWithMonitors @fillID", fileIdParm).ToList();

            if (tasks.Any())
            {
                foreach (var task in tasks)
                {
                    var item = new MonitorItem();
                    item.TaskId = task.TaskId;
                    item.Description = task.Description;

                    var monitors = _dbContext.MonitorResults.Where(x => x.TaskId == task.TaskId).ToList();

                    item.MonitorResults.AddRange(monitors);

                    response.MonitorItem.Add(item);
                }
            }

            return response;
        }
    }
}
