using MSR.Domain.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class UploadFile: Command
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public string Base64String { get; set; }
    }
}
