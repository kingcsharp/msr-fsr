using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Orders
{
    public class WorkOrderImageView
    {
        public string FILE_NAME { get; set; }
        public string Path { get; set; }
    }
}
