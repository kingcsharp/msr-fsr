using System;
using System.Security.AccessControl;

namespace Msr.Models.Orders
{
    public class ApprovedPeopleView
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string SystemStatus { get; set; }
      
    }
}
