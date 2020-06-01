using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Domain.Commands
{
    public class RemoveMenuRoleMap: Command
    {
        public int Id { get; set; }
    }
}
