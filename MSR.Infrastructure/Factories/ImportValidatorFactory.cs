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
            switch (menuItem)
            {
                case EnumMenuItem.CustomersDepartments:
                    return _serviceProvider.GetService<CustomerImportValidator>();
                case EnumMenuItem.Locations:
                    return _serviceProvider.GetService<LocationImportValidator>();
                default:
                    throw new NotImplementedException($"No Validator for Menu Item: {menuItem}");
            }
        }
    }
}
