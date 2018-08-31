using System;
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

        public NotificationViewModel GetNotifications()
        {
            var notificationViewModel = new NotificationViewModel();

            try
            {
                var items = _dbContext.NotificationItemViews.ToList();

                notificationViewModel.Items = items.OrderByDescending(x=>x.ItemCount).Take(3);
                notificationViewModel.Total = items.Count;
            }
            catch (Exception e)
            {
                ////log
            }

            return notificationViewModel;
        }
    }
}