using Microsoft.EntityFrameworkCore;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MSR.Infrastructure.Resources.Queries
{
    public static class ProcedureStepTemplateQueries
    {
        public static IQueryable<ProcedureStepTemplate> CreateProcedureStepTemplateQuery(this IQueryable<ProcedureStepTemplate> query, GetProcedureStepTemplate command, bool forRowCount = false)
        {

            query = query.Include(x => x.ReferenceFiles).ThenInclude(x => x.FileObject).AsQueryable();

            query = query.Where(command.Id, s => s.Id == command.Id);
            query = query.Where(command.Text, s => s.StepText.Contains(command.Text));
            query = query.Where(command.Title, s => s.Title.Contains(command.Title));

            if (command.SortAscending.HasValue && !string.IsNullOrEmpty(command.Term))
            {
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureStepTemplateSortFields.Id), s => s.Id);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureStepTemplateSortFields.Title), s => s.Title);
                query = query.OrderBy(command, command.Term == EnumUtils.GetDescription(EnumProcedureStepTemplateSortFields.Text), s => s.StepText);
            }

            if (command.Skip.HasValue && command.Take.HasValue && !forRowCount)
            {
                query = query.Skip(command.Skip.Value).Take(command.Take.Value);
            }

            return query; 
        }
    }
}
