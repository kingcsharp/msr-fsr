using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSR.Domain.Hub;
using MSR.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Concurrent;

namespace MSR.Answer.MessageHub.Hubs
{
    /// <summary>
    /// Connection mapping
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConnectionMapping<T>
    {
        private readonly ConcurrentDictionary<T, HashSet<string>> _connections =
            new ConcurrentDictionary<T, HashSet<string>>();

        /// <summary>
        /// Count
        /// </summary>
        public int Count { get { return _connections.Count; } }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="key"></param>
        /// <param name="connectionId"></param>
        public void Add(T key, string connectionId)
        {
            HashSet<string> connections;
            if (!_connections.TryGetValue(key, out connections)) {
                connections = new HashSet<string>();
                _connections.TryAdd(key, connections);
            }

            connections.Add(connectionId);
        }

        /// <summary>
        /// GetConnection
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections)) {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

        /// <summary>
        /// Remove connection
        /// </summary>
        /// <param name="key"></param>
        /// <param name="connectionId"></param>
        public void Remove(T key, string connectionId)
        {
            HashSet<string> connections;
            if (!_connections.TryGetValue(key, out connections)) {
                return;
            }

            // FIXME: remove from status list

            connections.Remove(connectionId);
            if (connections.Count == 0) {
                _connections.TryRemove(key, out _);
            }
        }
    }

    /// <summary>
    /// SignalR MessageHub
    /// </summary>
    [Authorize]
    public class MessageHub : Hub
    {
        private static readonly ConnectionMapping<string> Connections = new
            ConnectionMapping<string>();

        /// <summary>
        /// Send message to user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, Toaster message)
        {
            IEnumerable<string> connections;
            connections = Connections.GetConnections(user).ToList();
            foreach (var client in connections) {
                await Clients.Clients(client).SendAsync("ToasterMessage", message);
            }
        }

        /// <summary>
        /// Subscribe this connection to work order update messages.
        /// </summary>
        /// <returns></returns>
        public void SubscribeWorkOrderUpdate()
        {
            Connections.Add("status", Context.ConnectionId);
        }

        public void SendWorkOrderUpdate(WorkOrderStatusUpdate update)
        {
            IEnumerable<string> connections;
            connections = Connections.GetConnections("status").ToList();
            foreach (var client in connections)
            {
                _ = Clients.Clients(client).SendAsync("WorkOrderUpdate", update);
            }
        }

        /// <summary>
        /// Send workflow messages to all clients
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task WorkflowMessage(Guid guid, PendingNotificationItem message)
        {
            await Clients.All.SendAsync("WorkflowNotification", guid, message);
        }

        /// <summary>
        /// Log the connection
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;
            if (name == null)
            {
                // heartbeat
                await Groups.AddToGroupAsync(Context.ConnectionId, "answer");
                Connections.Add("ping", Context.ConnectionId);
            }
            else
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, "answer");
                Connections.Add(name, Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// Remove the connection
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception e)
        {
            var name = Context.User.Identity.Name;
            if (name != null) {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "answer");
                Connections.Remove(name, Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(e);
        }
    }
}
