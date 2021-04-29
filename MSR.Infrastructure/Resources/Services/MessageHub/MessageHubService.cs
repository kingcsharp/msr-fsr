using MSR.Domain.Abstractions.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System;
using MSR.Domain.Hub;
using MSR.Domain.Models;
using MSR.Domain.Models.Config;
using Microsoft.Extensions.Logging;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;

namespace MSR.Infrastructure.Resources.Services.MessageHub
{
    public class MessageHubService : IMessageHubClient
    {
        private HubConnection connection = null;
        private ILogger _logger;
        private string _url;
        private object _mutex = new object();
        private bool isConnecting = false;

        public MessageHubService(ILogger<MessageHubService> logger, GeneralInformation config)
        {
            Uri baseUri = new Uri(config.MessageURL);
            UriBuilder hubUri = new UriBuilder(baseUri.Scheme, baseUri.Host, baseUri.Port, "msg");
            _url = hubUri.ToString();
            _logger = logger;
        }

        private async Task Connect()
        {
            if (connection != null && connection.State != HubConnectionState.Disconnected)
            {
                return;
            }
            lock(_mutex) {
                if (isConnecting) {
                    return;
                }
                isConnecting = true;
            }

            connection = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl(_url, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(CurrentUser.GetAccessToken());
                })
                .Build();

            await connection.StartAsync();
        }

        public async Task SendNotification(Guid guid, PendingNotificationItem message)
        {
            try
            {
                await Connect();
                await connection.InvokeAsync("WorkflowMessage", guid, message);
            }
            catch(OperationCanceledException)
            {
                // ignore
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            finally {
                isConnecting = false;
            }
        }

        public async Task SendWorkOrderUpdate(WorkOrderStatusUpdate update)
        {
            try
            {
                await Connect();
                await connection.InvokeAsync("SendWorkOrderUpdate", update);
            }
            catch(OperationCanceledException)
            {
                // ignore
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            finally {
                isConnecting = false;
            }
        }

        public async Task SendNotification(string userId, Toaster message)
        {
            try
            {
                await Connect();
                await connection.InvokeAsync("SendMessage", userId, message);
            }
            catch(OperationCanceledException)
            {
                // ignore
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            finally {
                isConnecting = false;
            }
        }

        public void SendApprovalNotification(EnumApprovalTables approvalTable, int count = 1)
        {
            _ = SendNotification(Guid.NewGuid(), new PendingNotificationItem()
            {
                Table = (int) approvalTable,
                Count = count
            });
        }
    }
}
