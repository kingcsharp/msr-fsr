using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Abstractions.Services
{
    public interface IValidateImportData
    {
        bool ValidateImportData(byte[] binData, out IEnumerable<ImportError> importErrors);
    }
}
