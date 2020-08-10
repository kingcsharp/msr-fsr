using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Domain.Abstractions.Services
{
    public interface IValidateImportData
    {
        bool ValidateImportData(string csvData, out IEnumerable<ImportError> importErrors);
    }
}
