using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IEquipmentMaintenanceService
    {
        Task<IEnumerable<EquipmentMaintenanceModel>> GetEquipmentMaintenancesAsync(GetEquipmentMaintenance command);
        Task<EquipmentMaintenanceModel> CreateEquipmentMaintenanceAsync(CreateEquipmentMaintenance command);
        Task<EquipmentMaintenanceModel> UpdateEquipmentMaintenanceAsync(UpdateEquipmentMaintenance command);
    }
}
