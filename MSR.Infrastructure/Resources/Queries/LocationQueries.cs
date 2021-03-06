using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class LocationQueries
    {
        public static IQueryable<Location> CreateLocationQuery(this IQueryable<Location> query, GetLocations command, bool forRowCount = false)
        {


            query = query.Where(command.Address1, s => s.Address1.Contains(command.Address1));
            query = query.Where(command.Address2, s => s.Address2.Contains(command.Address2));
            query = query.Where(command.City, s => s.City.Contains(command.City));
            query = query.Where(command.Country, s => s.Country.Contains(command.Country));
            query = query.Where(command.CreatedFullName, s => s.Created.FullName.Contains(command.CreatedFullName));
            query = query.Where(command.CreatedOn, s => DateTime.Compare(s.CreatedOn.Date,command.CreatedOn.Value.Date) == 0);
            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.InternalAddress, s => s.InternalAddress.Contains(command.InternalAddress));
            query = query.Where(command.Name, s => s.Name.Contains(command.Name));
            query = query.Where(command.ParentId, s => s.ParentId == command.ParentId);
            query = query.Where(command.ParentName, s => s.Parent.Name.Contains(command.ParentName));
            query = query.Where(command.Phone, s => s.Phone.Contains(command.Phone));
            query = query.Where(command.Postalcode, s => s.PostalCode.Contains(command.Postalcode));
            query = query.Where(command.State, s => s.State.Contains(command.State));
            query = query.Where(command.TimezoneDescription, s => s.TimeZone.Description.Contains(command.TimezoneDescription));

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Address1), s => s.Address1);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Address2), s => s.Address2);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.City), s => s.City);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Country), s => s.Country);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.CreatedFullName), s => s.Created.FirstName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.InternalAddress), s => s.InternalAddress);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Name), s => s.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.ParentName), s => s.Parent.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Phone), s => s.Phone);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.Postalcode), s => s.PostalCode);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.State), s => s.State);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumLocationSortFields.TimezoneDescription), s => s.TimeZone.Description);

            }


            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
