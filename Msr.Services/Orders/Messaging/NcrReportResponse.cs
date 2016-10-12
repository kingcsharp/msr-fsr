
namespace Msr.Services.Orders.Messaging
{
    public class NcrReportResponse : BaseNotification
    {
        public NcrReportResponse()
        {
            Details = new NcrDetails();
        }

        public NcrDetails Details { get; set; } 
    }
}
