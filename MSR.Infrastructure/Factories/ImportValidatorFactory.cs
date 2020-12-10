using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using System;
using Microsoft.Extensions.DependencyInjection;
using MSR.Domain.Validators;

namespace MSR.Infrastructure.Factories
{
    public class ImportValidatorFactory : IImportValidatorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ImportValidatorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IValidateImportData Create(EnumMenuItem menuItem)
        {
            using var scope = _serviceProvider.CreateScope();
            switch (menuItem)
            {
                case EnumMenuItem.CustomersDepartments:
                    return scope.ServiceProvider.GetService<CustomerImportValidator>();
                case EnumMenuItem.Locations:
                    return scope.ServiceProvider.GetService<LocationImportValidator>();
                case EnumMenuItem.Parts:
                    return scope.ServiceProvider.GetService<PartValidator>();
                case EnumMenuItem.RunnableProcedures:
                    return scope.ServiceProvider.GetService<ProcedureValidator>();
                case EnumMenuItem.QuotesProducts:
                    return scope.ServiceProvider.GetService<QuoteImportValidator>();
                default:
                    throw new NotImplementedException($"No Validator for Menu Item: {menuItem}");
            }
        }
    }
}
