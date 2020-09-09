using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IMessageHubClient
    {
        public Task Connect(string url);
        public void SendNotification(string userId, string message);
    }
}
