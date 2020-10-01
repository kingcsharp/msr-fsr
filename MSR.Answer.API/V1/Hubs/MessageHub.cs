using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using MSR.Domain.Hub;

namespace MSR.Application.Hubs
{
    /// <summary>
    /// Connection mapping
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

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
            lock (_connections) {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections)) {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections) {
                    connections.Add(connectionId);
                }
            }
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
            lock (_connections) {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections)) {
                    return;
                }

                lock (connections) {
                    connections.Remove(connectionId);
                    if (connections.Count == 0) {
                        _connections.Remove(key);
                    }
                }
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
            foreach (var client in Connections.GetConnections(user)) {
                await Clients.Clients(client).SendAsync("ToasterMessage", message);
            }
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
