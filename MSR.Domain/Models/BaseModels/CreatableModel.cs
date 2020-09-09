using System;

namespace MSR.Domain.Models.BaseModels
{
    public class CreatableModel: EntityModel
    {
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public UserModel Created { get; set; }
    }
}
