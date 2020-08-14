using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Abstractions.Services
{
    public interface IImportValidatorFactory
    {
        IValidateImportData Create(EnumMenuItem menuItem);
    }
}
