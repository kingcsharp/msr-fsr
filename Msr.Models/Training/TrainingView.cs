using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Training
{
    public class TrainingView
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string PositionName { get; set; }
        public string TrainingIdRev { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; }
    }
}
