using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MSR.Domain.Hub;

namespace MSR.Domain.Abstractions.Services
{
    public interface IMessageHubClient
    {
        public Task Connect(string url);
        public void SendNotification(string userId, Toaster message);
    }
}
