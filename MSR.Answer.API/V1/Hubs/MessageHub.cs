using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace MSR.Application.Hubs
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        public int Count { get { return _connections.Count; } }

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

        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections)) {
                return connections;
            }

            return Enumerable.Empty<string>();
        }

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

    [Authorize]
    public class MessageHub : Hub
    {
        private readonly static ConnectionMapping<string> _connections = new
            ConnectionMapping<string>();

        public async Task SendMessage(string user, string message)
        {
            foreach (var client in _connections.GetConnections(user)) {
                await Clients.Clients(client).SendAsync("ReceiveMessage", message);
            }
        }

        public override async Task OnConnectedAsync()
        {
            string name = Context.User.Identity.Name;
            if (name != null) {
                await Groups.AddToGroupAsync(Context.ConnectionId, "answer");
                _connections.Add(name, Context.ConnectionId);
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception e)
        {
            string name = Context.User.Identity.Name;
            if (name != null) {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, "answer");
                _connections.Remove(name, Context.ConnectionId);
            }
            await base.OnDisconnectedAsync(e);
        }
    }
}
