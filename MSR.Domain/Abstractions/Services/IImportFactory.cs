using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Abstractions.Services
{
    public interface IImportFactory
    {
        IImportCsvData Create(EnumMenuItem menuItem);
    }
}
