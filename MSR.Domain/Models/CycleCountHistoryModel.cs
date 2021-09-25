using MSR.Domain.Models.BaseModels;

namespace MSR.Domain.Models
{
    public class CycleCountHistoryModel : EntityModel
    {
        public string PartNumber { get; set; }
        public string SerialNumber { get; set; }
        public int CycleCount { get; set; }
    }
}
