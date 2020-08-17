using System.Collections.Generic;

namespace MSR.Domain.Models
{
    public class ImportError
    {
        public ImportError()
        {
            Errors = new List<string>();
        }

        public int Line { get; set; }
        public List<string> Errors { get; set; }
    }
}
