using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Helpers;
using System;

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
            else
            {
                locationQuery = locationQuery.Where(i => i.IsActive);
            }
            
            if (command.ParentId.HasValue && command.ParentId.Value != 0)
            {
                locationQuery = locationQuery.Where(i => i.ParentId == command.ParentId);
            }

            var locations = await locationQuery.Include(i => i.Parent)
                                               .ToListAsync();

            var locationIds = locations.Select(i => i.Id);
            var locationApprovals = await _unitOfWork.LocationApprovals.Query()
                                                                       .Include(i => i.Status)
                                                                       .Where(i => i.LocationId.HasValue && locationIds.Contains(i.LocationId.Value)).ToListAsync();
            var ret = new List<LocationModel>();
            foreach(var location in locations)
            {
                var domlocation = _mapper.Map<LocationModel>(location);
                var siteId = location.ParentId;

                if(location.Parent != null && location.Parent.ParentId.HasValue)
                {
                    siteId = location.Parent.ParentId;
                }
                domlocation.Site = siteId;
                var locationApproval = locationApprovals.FirstOrDefault(i => i.LocationId == location.Id);
                if (locationApproval != null)
                {
                    domlocation.Status = locationApproval.Status.Name;
                }
                ret.Add(domlocation);
            }

            return ret;
        }

        public async Task<LocationModel> CreateLocationAsync(CreateLocation command, bool import = false)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            LocationModel retLocation;

            //Import is used here because if they are importing the data, it doesn't go through approvals.
            if (CurrentUser.CanApproveActivity(EnumApprovalTables.LocationApproval) || import)
            {
                var location = _mapper.Map<EntityFramework.Entities.Location>(command);
                location.IsActive = true;
                _unitOfWork.Locations.Add(location);

                await _unitOfWork.LogApprovalTransaction(location, location.Id);
                retLocation = _mapper.Map<LocationModel>(location);
            }
            else
            {
                var locationApproval = _mapper.Map<LocationApproval>(command);
                locationApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(locationApproval);
                locationApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(locationApproval.Workflow?.Id ?? 0);
                locationApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);
                locationApproval.IsActive = true;

                await _unitOfWork.LocationApprovals.AddAsync(locationApproval);
                await _unitOfWork.SaveChangesAsync();

                retLocation = _mapper.Map<LocationModel>(locationApproval);
            }

            return retLocation;
        } 

        public async Task<LocationModel> UpdateLocationAsync(UpdateLocation command, bool import = false)
        {
            var curLocation = await _unitOfWork.Locations.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(curLocation is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Location)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            LocationModel retLocation;

            //Import is used here because if they are importing the data, it doesn't go through approvals.
            if (CurrentUser.CanApproveActivity(EnumApprovalTables.LocationApproval) || import)
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
                locationApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

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


            if (CurrentUser.CanApproveActivity(EnumApprovalTables.LocationApproval))
            {
                curLocation.IsActive = false;
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

                locationApproval.IsActive = false;
                locationApproval.LocationId = curLocation.Id;
                locationApproval.Workflow = await _unitOfWork.GetWorkflowForEntityAsync(locationApproval);
                locationApproval.WorkflowGroup = await _unitOfWork.GetWorkFlowGroupForWorkFlow(locationApproval.Workflow?.Id ?? 0);
                locationApproval.Status = await _unitOfWork.Status.FirstOrDefaultAsync(false, i => i.Id == (int)ApprovalStatusEnum.Pending);

                await _unitOfWork.LocationApprovals.AddAsync(locationApproval);
                await _unitOfWork.SaveChangesAsync();

                retLocation = _mapper.Map<LocationModel>(locationApproval);
            }

            return retLocation;
        }

        public async Task<IEnumerable<LocationModel>> ImportLocations(string csvData)
        {
            var records = CSVHelper.ParseRecords<LocationImportItem>(csvData);
            var locations = new List<LocationModel>();

            if (!CurrentUser.HasPrivilege(EnumMenuItem.CustomersDepartments, EnumPrivilege.CanApprove))
            {
                throw new DomainException("Permission denied for import", DomainError.BadRequest);
            }

            foreach (var record in records)
            {
                try
                {
                    
                    var ret = await CreateLocationAsync(_mapper.Map<CreateLocation>(record), true);
                    locations.Add(ret);
                }
                catch(Exception ex)
                {
                    //If we get an error on a single import dump it and keep going. 
                }
            }

            return locations;

        }

        public async Task<IEnumerable<SensorModel>> GetSensorsForLocation(GetSensorsForLocation command)
        {
            var location = await _unitOfWork.Locations.Query().Include(i => i.Sensors).Include(i => i.Parent).FirstOrDefaultAsync(i => i.Id == command.LocationId);

            if(location is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Location)} not found with ID: {command.LocationId}", DomainError.NotFound);
            }

            var siteId = location.ParentId;

            var sensorList = location.Sensors.Any() ? location.Sensors.Select(i => _mapper.Map<SensorModel>(i)) : new List<SensorModel>();

            foreach(var sensor in sensorList)
            {
                if (location.Parent != null && location.Parent.ParentId.HasValue)
                {
                    siteId = location.Parent.ParentId;
                }
                sensor.AssignedLocation.Site = siteId;
            }

            return sensorList;
        }

        public async Task AddSensorToLocation(CreateLocationSensorMap command)
        {
            var location = await _unitOfWork.Locations.FirstOrDefaultAsync(false,i => i.Id == command.LocationId);
            var sensor = await _unitOfWork.Sensors.FirstOrDefaultAsync(false, i => i.Id == command.SensorItemId);
            
            if(location is null || sensor is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Sensor)} or {nameof(EntityFramework.Entities.Location)} not found", DomainError.BadRequest);
            }

            sensor.AssignedLocation = location;
            sensor.AssignedLocationId = location.Id;

            _unitOfWork.Sensors.Update(sensor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveSensorFromLocation(DeleteLocationSensorMap command)
        {
            var sensor = await _unitOfWork.Sensors.FirstOrDefaultAsync(false, i => i.Id == command.SensorItemId);

            if (sensor is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Sensor)} not found", DomainError.BadRequest);
            }

            sensor.AssignedLocationId = null;
            sensor.AssignedLocation = null;

            _unitOfWork.Sensors.Update(sensor);
            await _unitOfWork.SaveChangesAsync();

        }
    }
}
