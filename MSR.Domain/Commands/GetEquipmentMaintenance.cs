
using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetEquipmentMaintenance : Command
    {
        public int? Id { get; set; }
    }
}
