using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Models.BaseModels
{
    public class CreatableModel: EntityModel
    {
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public User Created { get; set; }
    }
}
