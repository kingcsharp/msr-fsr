using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Infrastructure.Resources.Services.Help
{
    public class SupportRequestDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string ContactMethod { get; set; }
        public string Details { get; set; }
    }
}
