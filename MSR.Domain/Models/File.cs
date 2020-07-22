using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public class File
    {
        public string Name { get; set; }
        public string Base64String { get; set; }
        public string ContentType { get; set; }
    }
}
