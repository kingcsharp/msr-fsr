using MSR.Domain.Abstractions.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System;
using MSR.Domain.Hub;
using Microsoft.Extensions.Logging;

namespace MSR.Application.ApplicationServices
{
    public class MessageHubAppService : IMessageHubClient
    {
        private HubConnection connection = null;
        private ILogger _logger;

        public static int pingCounterSend = 0;
        public static int pingCounterRcv = 0;

        public async Task Connect(string url, ILogger logger = null)
        {
            try
            {
                if (connection != null)
                {
                    return;
                }
                connection = new HubConnectionBuilder()
                    .WithUrl(url)
                    .Build();

                connection.Closed += async (error) =>
                {
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await connection.StartAsync();
                };

                await connection.StartAsync();

                _logger = logger;
                connection.On<Toaster>("ToasterMessage", (msg) =>
                {
                    if (_logger != null)
                    {
                        if (pingCounterRcv % 20 == 0)
                        {
                            _logger.LogInformation($"Received SignalR Heartbeat {msg.Message} " +
                                $"(repeated {pingCounterRcv} times)");
                            pingCounterRcv = 0;
                        }
                        pingCounterRcv += 1;
                    }
                });
            }
            catch(Exception ex)
            {
                //If this fails swallow the error
                _logger.LogError(ex, ex.Message);
            }

        }

        public void SendNotification(string userId, Toaster message)
        {
            try
            {
                connection.InvokeAsync("SendMessage", userId, message);

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
                    _logger.LogInformation(
                        $"SendMessage userId={userId} message={message.Message} " +
                        $"(repeated {pc} times");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
