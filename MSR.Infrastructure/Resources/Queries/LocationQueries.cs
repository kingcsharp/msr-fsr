using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class LocationQueries
    {
        public static IQueryable<LocationModel> CreateLocationQuery(this IQueryable<LocationModel> query, GetLocations command, bool forRowCount = false)
        {
            query = query.Where(command.Address1, s =>  !string.IsNullOrEmpty(s.Address1) && s.Address1.ToLower().Contains(command.Address1.ToLower()));
            query = query.Where(command.Address2, s => !string.IsNullOrEmpty(s.Address2) && s.Address2.ToLower().Contains(command.Address2.ToLower()));
            query = query.Where(command.City, s => !string.IsNullOrEmpty(s.City) && s.City.ToLower().Contains(command.City.ToLower()));
            query = query.Where(command.Country, s => !string.IsNullOrEmpty(s.Country) && s.Country.ToLower().Contains(command.Country.ToLower()));
            query = query.Where(command.CreatedFullName, s => s.Created != null && s.Created.FullName.ToLower().Contains(command.CreatedFullName.ToLower()));
            query = query.Where(command.CreatedOn, s => DateTime.Compare(s.CreatedOn.Date,command.CreatedOn.Value.Date) == 0);
            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.InternalAddress, s => !string.IsNullOrEmpty(s.InternalAddress) && s.InternalAddress.ToLower().Contains(command.InternalAddress.ToLower()));
            query = query.Where(command.Name, s => !string.IsNullOrEmpty(s.Name) && s.Name.ToLower().Contains(command.Name.ToLower()));
            query = query.Where(command.ParentId, s => s.ParentId == command.ParentId);
            query = query.Where(command.ParentName, s => s.Parent != null && s.Parent.Name.ToLower().Contains(command.ParentName.ToLower()));
            query = query.Where(command.Phone, s => !string.IsNullOrEmpty(s.Phone) && s.Phone.ToLower().Contains(command.Phone.ToLower()));
            query = query.Where(command.Postalcode, s => !string.IsNullOrEmpty(s.PostalCode) && s.PostalCode.ToLower().Contains(command.Postalcode.ToLower()));
            query = query.Where(command.State, s => !string.IsNullOrEmpty(s.State) && s.State.ToLower().Contains(command.State.ToLower()));
            query = query.Where(command.TimezoneDescription, s => s.TimeZone != null && !string.IsNullOrEmpty(s.TimeZone.Description) && s.TimeZone.Description.ToLower().Contains(command.TimezoneDescription.ToLower()));
            query = query.Where(command.Status, s =>  command.Status.Contains(s.Status));

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Address1), s => s.Address1);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Address2), s => s.Address2);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.City), s => s.City);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Country), s => s.Country);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.CreatedFullName), s => s.Created == null ? "" : s.Created.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.InternalAddress), s => s.InternalAddress);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.ParentName), s => s.Parent == null ? "" : s.Parent.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Phone), s => s.Phone);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Postalcode), s => s.PostalCode);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.State), s => s.State);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.TimezoneDescription), s => s.TimeZone == null ?  "" : s.TimeZone.Description);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Status), s => s.Status);
            }


            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
