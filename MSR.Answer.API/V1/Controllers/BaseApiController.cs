using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MSR.Application.Hubs;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;
using MSR.Domain.Models;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiController]
    [Authorize]
    public abstract class BaseApiController : ControllerBase
    {
        public int UserId => CurrentUser.GetId();

        public string Controller => this.ControllerContext.RouteData.Values["controller"].ToString();

        /// <summary>
        /// Sends message to the ui notifying of a pending approval
        /// </summary>
        /// <returns></returns>
        public async Task SendApprovalNotificationHubMessage(EnumApprovalTables approvalTable, IHubContext<MessageHub> messageHub, int count = 1)
        {
            if (!CurrentUser.CanApproveActivity(approvalTable))
            {
                await messageHub.Clients.All.SendAsync("WorkflowNotification", new Guid(), new PendingNotificationItem()
                {
                    Table = (int)approvalTable,
                    Count = count
                });
            }
        }
    }
}
