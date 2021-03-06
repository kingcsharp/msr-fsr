using Microsoft.EntityFrameworkCore;
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
    public static class EquipmentMaintenanceQueries
    {
        public static IQueryable<EquipmentMaintenance> CreateEquipmentMaintainanceQuery(this IQueryable<EquipmentMaintenance> query, GetEquipmentMaintenance command, bool forRowCount = false)
        {

            query = query.Include(i => i.Location).Include(i => i.Status).Include(i => i.AssignedTo).AsQueryable();

            query = query.Where(command.AssignedToFullName, s => s.AssignedTo.FirstName.Contains(command.AssignedToFullName));
            query = query.Where(command.Comments, s => s.Comments.Contains(command.Comments));
            query = query.Where(command.CreatedFullName, s => s.Created.FullName.Contains(command.CreatedFullName));
            query = query.Where(command.CreatedOn, s => DateTime.Compare(s.CreatedOn.Date,command.CreatedOn.Value.Date) == 0);
            query = query.Where(command.FrequencyField, s => s.FrequencyField == command.FrequencyField);
            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.LocationName, s => s.Location.Name.Contains(command.LocationName));
            query = query.Where(command.MaintenanceTask, s => s.MaintenanceTask.Contains(command.MaintenanceTask));
            query = query.Where(command.PemLastCompletedDate, s => s.PemLastCompletedDate == command.PemLastCompletedDate);
            query = query.Where(command.TroubleState, s => s.TroubleState == command.TroubleState);
            query = query.Where(command.StatusName, s => s.Status.Name == command.StatusName);

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumEquipmentMaintenanceSoftFields.AssignedToFullName), s => s.AssignedTo.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumEquipmentMaintenanceSoftFields.Comments), s => s.Comments);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumEquipmentMaintenanceSoftFields.CreatedFullName), s => s.Created.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumEquipmentMaintenanceSoftFields.CreatedOn), s => s.CreatedOn);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumEquipmentMaintenanceSoftFields.FrequencyField), s => s.FrequencyField);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumEquipmentMaintenanceSoftFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumEquipmentMaintenanceSoftFields.LocationName), s => s.Location.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumEquipmentMaintenanceSoftFields.MaintenanceTask), s => s.MaintenanceTask);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumEquipmentMaintenanceSoftFields.PemLastCompletedDate), s => s.PemLastCompletedDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumEquipmentMaintenanceSoftFields.StatusName), s => s.Status.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumEquipmentMaintenanceSoftFields.TroubleState), s => s.TroubleState);
            }


            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
