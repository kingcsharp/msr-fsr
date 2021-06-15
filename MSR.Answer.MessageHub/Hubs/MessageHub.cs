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
        // The set of connection IDs per key is stored in another ConcurrentDictionary
        // because there is no thread-safe HashSet.  The "byte" value is ignored.
        private readonly ConcurrentDictionary<T, ConcurrentDictionary<string, byte>> _connections =
            new ConcurrentDictionary<T, ConcurrentDictionary<string, byte>>();

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
            ConcurrentDictionary<string, byte> connections;
            if (!_connections.TryGetValue(key, out connections)) {
                connections = new ConcurrentDictionary<string, byte>();
                _connections.TryAdd(key, connections);
            }

            connections.TryAdd(connectionId, 1);
        }

        /// <summary>
        /// GetConnection
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<string> GetConnections(T key)
        {
            ConcurrentDictionary<string, byte> connections;
            if (_connections.TryGetValue(key, out connections)) {
                return connections.Keys;
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
            ConcurrentDictionary<string, byte> connections;
            if (!_connections.TryGetValue(key, out connections)) {
                return;
            }

            connections.TryRemove(connectionId, out _);
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
        private const string StatusGroup = "status";
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
        public async void SubscribeWorkOrderUpdate()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, StatusGroup);
        }

        public async void SendWorkOrderUpdate(WorkOrderStatusUpdate update)
        {
            await Clients.Group(StatusGroup).SendAsync("WorkOrderUpdate", update);
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
            if (name != null)
            {
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
                Connections.Remove(name, Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, StatusGroup);
            }
            await base.OnDisconnectedAsync(e);
        }
    }
}
