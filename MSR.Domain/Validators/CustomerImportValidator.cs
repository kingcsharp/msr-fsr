using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Validators
{
    public class CustomerImportValidator: IValidateImportData
    {
        public bool ValidateImportData(string csvData, out IEnumerable<ImportError> importErrors)
        {
            importErrors = null;
            return true;
        }
    }
}
