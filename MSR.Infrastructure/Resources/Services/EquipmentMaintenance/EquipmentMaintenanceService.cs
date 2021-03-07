using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.Queries;

namespace MSR.Infrastructure.Resources.Services.EquipmentMaintenance
{
    public class EquipmentMaintenanceService : IEquipmentMaintenanceService

    {
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public EquipmentMaintenanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EquipmentMaintenanceModel>> GetEquipmentMaintenancesAsync(GetEquipmentMaintenance command)
        {
            
            var equipmentMaintenanceEntities = _unitOfWork.EquipmentMaintenances.Query().CreateEquipmentMaintainanceQuery(command).ToListAsync();

            var equipmentMaintenanceModels = new List<EquipmentMaintenanceModel>();
            foreach (var equipmentMaintenanceEntity in await equipmentMaintenanceEntities)
            {
                equipmentMaintenanceModels.Add(_mapper.Map<EquipmentMaintenanceModel>(equipmentMaintenanceEntity));
            }

            return equipmentMaintenanceModels.AsEnumerable();
        }

        public async Task<EquipmentMaintenanceModel> CreateEquipmentMaintenanceAsync(CreateEquipmentMaintenance command)
        {
            var em = _mapper.Map<EntityFramework.Entities.EquipmentMaintenance>(command);

            // Save the new equipment maintenance entry
            await _unitOfWork.EquipmentMaintenances.AddAndSaveChangesAsync(em);

            em = await _unitOfWork.EquipmentMaintenances.Query()
                            .Include(i => i.Location)
                            .Include(i => i.Status)
                            .Include(i => i.AssignedTo)
                            .SingleOrDefaultAsync(i => i.Id == em.Id);

            var retEm = _mapper.Map<EquipmentMaintenanceModel>(em);

            return retEm;
        }


        public async Task<EquipmentMaintenanceModel> UpdateEquipmentMaintenanceAsync(UpdateEquipmentMaintenance command)
        {
            // Retreive equipment maintenance to update
            var em = await _unitOfWork.EquipmentMaintenances
                                .Query()
                                .FirstOrDefaultAsync(i => i.Id == command.Id);

            if (em is null)
            {
                throw new DomainException($"{nameof(EquipmentMaintenance)} not found with ID: {command.Id}");
            }

            em.LocationId = command.LocationId ?? em.LocationId;
            em.AssignedToId = command.AssignedToId ?? em.AssignedToId;
            em.TroubleState = command.TroubleState ?? em.TroubleState;
            em.MaintenanceTask = command.MaintenanceTask ?? em.MaintenanceTask;
            em.PemLastCompletedDate = command.PemLastCompletedDate.HasValue ? command.PemLastCompletedDate : em.PemLastCompletedDate;
            em.FrequencyField = command.FrequencyField ?? em.FrequencyField;
            em.Comments = command.Comments ?? em.Comments;

            // Save equipment maintenance changes
            await _unitOfWork.EquipmentMaintenances.UpdateAndSaveChangesAsync(em);

            em = await _unitOfWork.EquipmentMaintenances.Query()
                            .Include(i => i.Location)
                            .Include(i => i.Status)
                            .Include(i => i.AssignedTo)
                            .SingleOrDefaultAsync(i => i.Id == em.Id);

            var retEm = _mapper.Map<EquipmentMaintenanceModel>(em);

            return retEm;
        }

        public async Task<EquipmentMaintenanceModel> DeleteEquipmentMaintenanceAsync(DeleteEquipmentMaintenance command)
        {
            var equipmentMaintenanceEntity = await _unitOfWork.EquipmentMaintenances
                .Query()
                .FirstOrDefaultAsync(i => i.Id == command.Id);

            if (equipmentMaintenanceEntity is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.EquipmentMaintenance)} not found with ID: {command.Id}");
            }

            _unitOfWork.EquipmentMaintenances.Delete(false, equipmentMaintenanceEntity);
            await _unitOfWork.SaveChangesAsync();

            var equipmentMaintenanceModel = _mapper.Map<EquipmentMaintenanceModel>(equipmentMaintenanceEntity);

            return equipmentMaintenanceModel;
        }

        public async Task<int> GetEquipmentMaintenanceTotalRows(GetEquipmentMaintenance command)
        {
            var totalRows = await _unitOfWork.EquipmentMaintenances.Query().CreateEquipmentMaintainanceQuery(command, true).CountAsync();
            return totalRows;
        }
    }
}
