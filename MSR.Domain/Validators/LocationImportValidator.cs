using MSR.Domain.Abstractions.Services;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Validators
{
    public class LocationImportValidator: IValidateImportData
    {
        public bool ValidateImportData(string csvData, out IEnumerable<ImportError> importErrors)
        {
            importErrors = null;
            return true;
        }
    }
}
