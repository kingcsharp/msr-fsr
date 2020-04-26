using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class UpdateUser: CreateUser
    {
        public int Id { get; set; }
    }
}
