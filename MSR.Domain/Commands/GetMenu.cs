using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    public class GetMenu: Command<IEnumerable<MenuItem>>
    {
        public User User { get; set; }
    }
}
