using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Models.BaseModels
{
    public class DeletableModel : TrackableModel
    {
        public bool IsActive { get; set; }
    }
}
