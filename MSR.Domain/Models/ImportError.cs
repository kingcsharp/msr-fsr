using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Domain.Models
{
    public class ImportError
    {
        public int Line { get; set; }
        public List<string> Errors { get; set; }
    }
}
