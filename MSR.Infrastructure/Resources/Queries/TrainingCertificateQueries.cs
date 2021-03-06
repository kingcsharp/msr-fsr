using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class TrainingCertificateQueries
    {
        public static IQueryable<UserRole> CreateTrainingCertificationQuery(this IQueryable<UserRole> query, GetTrainingCertification command, bool forRowCount = false)
        {
            query = query.Include(i => i.User)
                .Include(i => i.Role)
                .Where(i => i.Role.IsCertificationRole.HasValue && i.Role.IsCertificationRole.Value).AsQueryable();

            
            query = query.Where(command.UserId, s => s.UserId == command.UserId);
            query = query.Where(command.CertificationFromDate, s => DateTime.Compare(s.LastUpdatedOn.Value.Date, command.CertificationFromDate.Value.Date) > 0);
            query = query.Where(command.CertificationName, s => s.Role.Name.Contains(command.CertificationName));
            query = query.Where(command.CertificationToDate, s => DateTime.Compare(s.LastUpdatedOn.Value.Date, command.CertificationFromDate.Value.Date) < 0);
            query = query.Where(command.EmployeeName, s => s.User.FullName.Contains(command.EmployeeName));
            query = query.Where(command.Status, s => (s.CertificationToDate.HasValue ? DateTime.Compare(s.CertificationToDate.Value, DateTime.UtcNow) <= 0 ? "Expired" : "Active" : "Active").Contains(command.Status));


            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumTrainingCertificationsSortFields.Id), s => s.UserId);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumTrainingCertificationsSortFields.CertificationFromDate), s => s.CertificationFromDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumTrainingCertificationsSortFields.CertificationName), s => s.Role.Name);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumTrainingCertificationsSortFields.CertificationToDate), s => s.CertificationToDate);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumTrainingCertificationsSortFields.EmployeeName), s => s.User.FullName);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumTrainingCertificationsSortFields.Status), s => s.CertificationToDate.Value);

            }
            
            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query;
        }
    }
}
