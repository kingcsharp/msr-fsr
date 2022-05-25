using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Linq.Expressions;

namespace MSR.Infrastructure.Resources.Projections
{
    public static class ProcedureProjections
    {
        public static Expression<Func<Procedure, dynamic>> ProcedureWithUsedProductCountView => x => new
        {
            Id = x.Id,
            Name = x.Name,
            ProcedureTypeId = x.ProcedureTypeId,
            Revision = x.Revision,
            Duration = x.Duration,
            DurationType = x.DurationType,
            ProcedureTypeName = x.ProcedureType.Name,
            CountProductsUsing = x.Products.Count,
            LastUpdatedById = x.LastUpdatedBy,
            LastUpdatedByFirstName = x.LastUpdated.FirstName,
            LastUpdatedByLastName = x.LastUpdated.LastName,
            LastUpdatedOn = x.LastUpdatedOn,
            CreatedById = x.CreatedBy,
            CreatedByFirstName = x.Created.FirstName,
            CreatedByLastName = x.Created.LastName,
            CreatedOn = x.CreatedOn
        };

        public static Expression<Func<Procedure, dynamic>> ProcedureExport => x => new
        {
            Id = x.Id,
            Name = x.Name,
            ProcedureTypeId = x.ProcedureTypeId,
            Revision = x.Revision,
            Duration = x.Duration,
            DurationType = x.DurationType,
            Comments = x.Comments
        };
    }


}
