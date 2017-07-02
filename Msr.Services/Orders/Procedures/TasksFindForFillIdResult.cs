using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Services.Orders.Procedures
{
    public class TasksFindForFillIdResult
    {
        public string Step_Text_Html { get; set; }
        public int? Print_Order { get; set; }
        public string Step_Id { get; set; }
        public IEnumerable<string> MonitorsDescription { get; set; }
    }
}
