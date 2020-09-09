using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Models
{
    public class SensorModel: CreatableModel
    {
        public string SensorName { get; set; }
        public virtual LocationModel Site { get; set; }
        public virtual LocationModel AssignedLocation { get; set; }

    }
}
