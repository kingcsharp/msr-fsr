using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Validators
{
    public class PartValidator : IValidateImportData
    {
        public bool ValidateImportData(string csvData, out IEnumerable<ImportError> importErrors)
        {
            throw new System.NotImplementedException();
        }
    }
}
