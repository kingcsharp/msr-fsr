using System.Collections.Generic;

namespace Msr.Services.Orders.Messaging
{
    public class CheckLoginResult
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
