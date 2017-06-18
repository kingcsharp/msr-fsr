using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Notes.Messages
{
    public class AddNoteToTaskRequest
    {
        public string Comment { get; set; }
        public string TaskId { get; set; }
    }
}
