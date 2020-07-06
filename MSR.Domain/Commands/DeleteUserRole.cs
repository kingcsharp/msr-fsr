using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class DeleteUserRole: Command
    {
        public int UserRoleId { get; set; }
    }
}
