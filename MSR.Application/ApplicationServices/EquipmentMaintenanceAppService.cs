using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.ApplicationServices
{
    public class EquipmentMaintenanceAppService :
        ICommandHandler<GetEquipmentMaintenance>,
        ICommandHandler<CreateEquipmentMaintenance>,
        ICommandHandler<UpdateEquipmentMaintenance>
    {

        private readonly IEquipmentMaintenanceService _equipmentMaintenanceService;
        private readonly IMapper _mapper;

        public EquipmentMaintenanceAppService(IEquipmentMaintenanceService equipmentMaintenanceService, IMapper mapper)
        {
            _equipmentMaintenanceService = equipmentMaintenanceService;
            _mapper = mapper;
        }

        public async Task<ICommandResponse> HandleAsync(GetEquipmentMaintenance command, CancellationToken cancellationToken = default)
        {
            var ret = await _equipmentMaintenanceService.GetEquipmentMaintenancesAsync(command);
            return new CommandResponse<IEnumerable<EquipmentMaintenanceModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateEquipmentMaintenance command, CancellationToken cancellationToken = default)
        {
            var ret = await _equipmentMaintenanceService.CreateEquipmentMaintenanceAsync(command);
            return new CommandResponse<EquipmentMaintenanceModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateEquipmentMaintenance command, CancellationToken cancellationToken = default)
        {
            var ret = await _equipmentMaintenanceService.UpdateEquipmentMaintenanceAsync(command);
            return new CommandResponse<EquipmentMaintenanceModel>(ret);
        }
    }
}
