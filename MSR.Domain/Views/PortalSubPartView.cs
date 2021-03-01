

//[{id, serialNumber, partNumber, cycleCount, qty, name}]
namespace MSR.Domain.Views
{
    public class PortalSubPartView
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string PartNumber { get; set; }
        public int CycleCount { get; set; }
        public int Qty { get; set; }
        public string Name { get; set; }

    }
}
