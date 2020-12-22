using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Domain.Abstractions.Services;

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
        protected void SendApprovalNotificationHubMessage(EnumApprovalTables approvalTable, IMessageHubClient messageHub, int count = 1)
        {
            messageHub.SendNotification(Guid.NewGuid(), new PendingNotificationItem()
            {
                Table = (int)approvalTable,
                Count = count
            });
        }
    }
}
