using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Parts.Messages
{
    public class AddPartTypesTaskRequest
    {
        public string Message { get; set; }
        public string NewObjID { get; set; }
    }
}
