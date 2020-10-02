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
                        _logger.LogInformation($"Received SignalR Heartbeat {msg.Message}");
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
                _logger.LogInformation($"SendMessage userId={userId} message={message.Message}");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
    }
}
