using MSR.Domain.Abstractions.Services;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using System;
using Amazon.Runtime.Internal;

namespace MSR.Application.ApplicationServices
{
    public class MessageHubAppService : IMessageHubClient
    {
        private HubConnection connection = null;

        public async Task Connect(string url)
        {
            if (connection != null) {
                return;
            }
            connection = new HubConnectionBuilder()
                .WithUrl(url)
                .Build();

            connection.Closed += async (error) => {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            await connection.StartAsync();
        }

        public void SendNotification(string userId, string message)
        {
            connection.InvokeAsync("SendMessage", userId, message);

            // TODO: it would be useful to hook in additional message logging
            // here to capture all import messages
        }
    }
}
