using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteEquipmentMaintenance: Command
    {
        public int Id { get; set; }
    }
}
