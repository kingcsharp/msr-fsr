using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MSR.Infrastructure.Resources.Projections
{
    public class PartProjections
    {
        public static Expression<Func<Part, dynamic>> PartExport => x => new
        {
            Id = x.Id,
            Name = x.Name,
            SegregationType = x.SegregationType,
            PartNumber = x.PartNumber,
            OEMPartNumber = x.OEMPartNumber,
            IsKit = x.IsKit,
            IsActive = x.IsActive,
            MaximumCycles = x.MaximumCycles
        };
    }
}
