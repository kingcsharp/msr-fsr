using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Models.Config;
using MSR.Domain.Helpers;

namespace MSR.Infrastructure.Resources.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LocationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ICollection<LocationModel>> GetLocationsAsync(GetLocations command)
        {
            var locationQuery = _unitOfWork.Locations.Query();
            if(command.Id.HasValue && command.Id.Value != 0)
            {
                locationQuery = locationQuery.Where(i => i.Id == command.Id);
            }
            
            if (command.ParentId.HasValue && command.ParentId.Value != 0)
            {
                locationQuery = locationQuery.Where(i => i.ParentId == command.ParentId);
            }

            var locations = await locationQuery.ToListAsync();
            var locationIds = locations.Select(i => i.Id);
            var locationApprovals = await _unitOfWork.LocationApprovals.Query()
                                                                       .Include(i => i.Status)
                                                                       .Where(i => locationIds.Contains(i.LocationId)).ToListAsync();
            var ret = new List<LocationModel>();
            foreach(var location in locations)
            {
                var domlocation = _mapper.Map<LocationModel>(location);
                var locationApproval = locationApprovals.FirstOrDefault(i => i.LocationId == location.Id);
                if (locationApproval != null)
                {
                    domlocation.Status = locationApproval.Status.Name;
                }
                ret.Add(domlocation);
            }

            return ret;
        }

        public async Task<LocationModel> CreateLocationAsync(CreateLocation command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            LocationModel retLocation;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.LocationApproval))
            {
                var location = _mapper.Map<EntityFramework.Entities.Location>(command);
                _unitOfWork.Locations.Add(location);

                await _unitOfWork.LogApprovalTransaction(location, location.Id);
                retLocation = _mapper.Map<LocationModel>(location);
            }
            else
            {
                var locationApproval = _mapper.Map<LocationApproval>(command);
                locationApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(locationApproval);
                locationApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(locationApproval.WorkflowGroup?.Id ?? 0);
                locationApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatus.Pending);
                
                await _unitOfWork.LocationApprovals.AddAsync(locationApproval);
                await _unitOfWork.SaveChangesAsync();

                retLocation = _mapper.Map<LocationModel>(locationApproval);
            }

            return retLocation;
        } 

        public async Task<LocationModel> UpdateLocationAsync(UpdateLocation command)
        {
            var curLocation = await _unitOfWork.Locations.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(curLocation is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Location)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            LocationModel retLocation;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.LocationApproval))
            {
                var location = _mapper.Map(command,curLocation);
                
                _unitOfWork.Locations.Update(location);
                await _unitOfWork.LogApprovalTransaction(location, location.Id);

                retLocation = _mapper.Map<LocationModel>(location);
            }
            else
            {
                var locationApproval = _mapper.Map<LocationApproval>(curLocation);
                _mapper.Map(command, locationApproval);
                locationApproval.LocationId = curLocation.Id;
                locationApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(locationApproval);
                locationApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(locationApproval.Workflow?.Id ?? 0);
                locationApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatus.Pending);

                await _unitOfWork.LocationApprovals.AddAsync(locationApproval);
                await _unitOfWork.SaveChangesAsync();
                
                retLocation = _mapper.Map<LocationModel>(locationApproval);
            }

            return retLocation;

        }

        public async Task<LocationModel> DeactivateLocationAsync(DeactivateLocation command)
        {
            var curLocation = await _unitOfWork.Locations.FirstOrDefaultAsync(false, i => i.Id == command.LocationId);

            if (curLocation is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Location)} not found with ID: {command.LocationId}", DomainError.NotFound);
            }

            LocationModel retLocation;

            curLocation.IsActive = false;

            if (CurrentUser.CanApproveActivity(EnumApprovalTables.LocationApproval))
            {

                _unitOfWork.Locations.Update(curLocation);

                foreach(var location in await _unitOfWork.Locations.Query().Where(i => i.ParentId == curLocation.Id).ToListAsync())
                {
                    location.IsActive = false;
                    _unitOfWork.Locations.Update(location);
                }

                await _unitOfWork.LogApprovalTransaction(curLocation, curLocation.Id);

                retLocation = _mapper.Map<LocationModel>(curLocation);
            }
            else
            {
                var locationApproval = _mapper.Map<LocationApproval>(curLocation);
                locationApproval.LocationId = curLocation.Id;
                locationApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(locationApproval);
                locationApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(locationApproval.WorkflowGroup?.Id ?? 0);
                locationApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatus.Pending);

                await _unitOfWork.LocationApprovals.AddAsync(locationApproval);
                await _unitOfWork.SaveChangesAsync();

                retLocation = _mapper.Map<LocationModel>(locationApproval);
            }

            return retLocation;
        }
    }
}
