using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models
{
    public class NoteModel
    {
        public string Name { get; set; }
        public string Note { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
