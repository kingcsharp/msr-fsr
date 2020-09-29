using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Resources.Services.Invoices
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
            var emList = new List<EquipmentMaintenanceModel>();
            var ems = _unitOfWork.EquipmentMaintenances.Query()
                                                .Include(i => i.Location)
                                                .Include(i => i.Status)
                                                .Include(i => i.AssignedTo)
                                                .AsQueryable();

            if (command.Id.HasValue)
            {
                ems = ems.Where(i => i.Id == command.Id);
            }

            foreach (var ep in await ems.ToListAsync())
            {
                emList.Add(_mapper.Map<EquipmentMaintenanceModel>(ep));
            }

            return emList.AsEnumerable();
        }

        public async Task<EquipmentMaintenanceModel> CreateEquipmentMaintenanceAsync(CreateEquipmentMaintenance command)
        {
            var em = _mapper.Map<EquipmentMaintenance>(command);

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

    }
}
