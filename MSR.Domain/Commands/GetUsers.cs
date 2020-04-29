using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class GetUsers: Command<ICollection<User>>
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Title { get; set; }
        public int? Supervisor { get; set; }
        public string PrimaryPhone { get; set; }
        public string Email { get; set; }
    }
}
