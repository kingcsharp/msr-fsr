using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Reporting
{

    public class WorkInProcess
    {
        [Key]
        public Int64 Id { get; set; }
        public string WoItem { get; set; }
        public DateTime? DueDate { get; set; }
        public string Details { get; set; }

    }
}