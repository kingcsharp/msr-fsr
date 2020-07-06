using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Infrastructure.Extensions;
using MSR.Infrastructure.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Extensions;

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

        public async Task<ICollection<Domain.Models.Location>> GetLocationsAsync(GetLocations command)
        {
            var locations = _unitOfWork.Locations.Query().Where(x => x.ParentId == command.ParentId).ToList();
            var ret = locations.Select(x => _mapper.Map<Domain.Models.Location>(x)).OrderBy(x=>x.Name).ToList();

            return ret;
        }

        public async Task<Domain.Models.Location> CreateLocationAsync(CreateLocation command)
        {
            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.Location retLocation;

            if (user.CanApprove(EnumMenuItem.Locations))
            {
                var location = _mapper.Map<EntityFramework.Entities.Location>(command);
                _unitOfWork.Locations.Add(location);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.LogApprovalTransaction(location, location.Id);
                retLocation = _mapper.Map<Domain.Models.Location>(location);
            }
            else
            {
                var locationApproval = _mapper.Map<LocationApproval>(command);
                _unitOfWork.LocationApprovals.Add(locationApproval);
                await _unitOfWork.SaveChangesAsync();

                retLocation = _mapper.Map<Domain.Models.Location>(locationApproval);
            }

            return retLocation;
        } 

        public async Task<Domain.Models.Location> UpdateLocationAsync(UpdateLocation command)
        {
            var curLocation = await _unitOfWork.Locations.FirstOrDefaultAsync(false, i => i.Id == command.Id);

            if(curLocation is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Location)} not found with ID: {command.Id}", DomainError.NotFound);
            }

            var user = await _unitOfWork.GetLoggedInUserAsync();
            Domain.Models.Location retLocation;


            if (user.CanApprove(EnumMenuItem.Locations))
            {
                var location = _mapper.Map<EntityFramework.Entities.Location>(command);
                _unitOfWork.Locations.Update(location);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.LogApprovalTransaction(location, location.Id);
                retLocation = _mapper.Map<Domain.Models.Location>(location);
            }
            else
            {
                var locationApproval = _mapper.Map<LocationApproval>(command);
                _unitOfWork.LocationApprovals.Add(locationApproval);
                await _unitOfWork.SaveChangesAsync();

                retLocation = _mapper.Map<Domain.Models.Location>(locationApproval);
            }

            return retLocation;

        }

        public async Task DeactivateLocationAsync(DeactivateLocation command)
        {
            var curLocation = await _unitOfWork.Locations.FirstOrDefaultAsync(false, i => i.Id == command.LocationId);

            if (curLocation is null)
            {
                throw new DomainException($"{nameof(EntityFramework.Entities.Location)} not found with ID: {command.LocationId}", DomainError.NotFound);
            }

            _unitOfWork.Locations.Delete(false, curLocation);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
