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

        public static int pingCounterSend = 0;
        public static int pingCounterRcv = 0;

        public MessageHubService(ILogger<MessageHubService> logger, GeneralInformation config)
        {
            Uri baseUri = new Uri(config.MessageURL);
            UriBuilder hubUri = new UriBuilder(baseUri.Scheme, baseUri.Host, baseUri.Port, "msg");
            _url = hubUri.ToString();
            _logger = logger;
        }

        public async Task Connect()
        {
            try
            {
                if (connection != null)
                {
                    return;
                }
                connection = new HubConnectionBuilder()
                    .WithAutomaticReconnect()
                    .WithUrl(_url, options =>
                    {
                        options.AccessTokenProvider = () => Task.FromResult(CurrentUser.GetAccessToken());
                    })
                    .Build();

                connection.Closed += async (error) =>
                {
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await connection.StartAsync();
                };

                await connection.StartAsync();

                connection.On<Toaster>("ToasterMessage", (msg) =>
                {
                    if (_logger != null)
                    {
                        if (pingCounterRcv % 20 == 0)
                        {
                            _logger.LogDebug($"Received SignalR Heartbeat {msg.Message} " +
                                $"(repeated {pingCounterRcv} times)");
                            pingCounterRcv = 0;
                        }
                        pingCounterRcv += 1;
                    }
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task SendNotification(Guid guid, PendingNotificationItem message)
        {
            try
            {
                await Connect();
                await connection.InvokeAsync("WorkflowMessage", guid, message);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task SendWorkOrderUpdate(WorkOrderStatusUpdate update)
        {
            try
            {
                await Connect();
                await connection.InvokeAsync("SendWorkOrderUpdate", update);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }

        public async Task SendNotification(string userId, Toaster message)
        {
            try
            {
                await Connect();
                await connection.InvokeAsync("SendMessage", userId, message);

                // Log the message.  If it's a ping, show it only once
                // ever 20 times (approx. every 400 seconds).
                bool logit = true;
                int pc = 0;
                if (userId.Equals("ping")) {
                    if (pingCounterSend % 20 == 0) {
                        pc = pingCounterSend;
                        pingCounterSend = 0;
                    } else {
                        logit = false;
                    }
                    pingCounterSend += 1;
                }
                if (logit) {
                    _logger.LogDebug(
                        $"SendMessage userId={userId} message={message.Message} " +
                        $"(repeated {pc} times");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
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
