using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Orders.Procedures
{
    public class MonitorTemplateMultiChoiceResult
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public bool? IsAnswer { get; set; }
    }
}
