using System.Collections.Generic;

namespace Msr.Services.Orders.Messaging
{
    public class AddUserMessageResponse : BaseNotification
    {
        public string UserId { get; set; }
    }
}
