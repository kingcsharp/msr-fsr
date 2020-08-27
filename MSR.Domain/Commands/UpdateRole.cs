using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class UpdateRole: CreateRole
    {
        public int Id { get; set; }
    }
}
