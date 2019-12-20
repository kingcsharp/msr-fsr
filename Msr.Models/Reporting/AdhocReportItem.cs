using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Reporting
{
    [Serializable]
    public class AdHocReportItem
    {
        public string Title { get; set; }
        public string DataURL { get; set; }
        public List<object> Columns { get; set; }
    }
}
