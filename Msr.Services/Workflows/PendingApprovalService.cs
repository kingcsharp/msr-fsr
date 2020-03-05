using System;
using System.Data.SqlClient;
using System.Linq;
using Msr.Models.Workflows;
using Msr.Repositories;
using Msr.Services.Workflows.ViewModels;

namespace Msr.Services.Workflows
{
    public class PendingApprovalService
    {
        private readonly MsrDbContext _dbContext;

        public PendingApprovalService()
        {
            _dbContext = new MsrDbContext();
        }

        public IQueryable<PendingApprovalView> GetPedningApprovalsQueryable()
        {
            return _dbContext.PendingApprovalViews;
        }

        public NotificationViewModel GetUserNotifications(string userId)
        {
            var notificationViewModel = new NotificationViewModel();

            try
            {
                var fileIdParm = new SqlParameter("@userId", userId);

                var items = _dbContext.Database.SqlQuery<NotificationItemView>("Portal_GetUserNotifications @userId", fileIdParm).ToList();

                notificationViewModel.Items = items.OrderByDescending(x => x.ItemCount).Take(3);
                notificationViewModel.Total = items.Sum(x => x.ItemCount);
            }
            catch (Exception)
            {
                ////log
            }

            return notificationViewModel;
        }
    }
}