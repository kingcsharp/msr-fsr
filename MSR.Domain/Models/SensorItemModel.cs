using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Models
{
    public class SensorItemModel: EntityModel
    {
        public string SensorName { get; set; }
        public string ItemId { get; set; }
        public int? LocationId { get; set; }
        public LocationModel AssignedLocation { get; set; }
    }
}
