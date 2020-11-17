using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Domain.Validators
{
    public class LocationImportValidator: IValidateImportData
    {
        public bool ValidateImportData(string csvData, out IEnumerable<ImportError> importErrors)
        {
            var records = CSVHelper.ParseRecords<LocationImportItem>(csvData);
            var errors = new List<ImportError>();
            int line = 1;


            foreach (var record in records)
            {
                var importError = new ImportError();

                if (string.IsNullOrWhiteSpace(record.Name))
                {
                    importError.Errors.Add($"{nameof(record.Name)} does not have a value");
                }

                if (importError.Errors.Any())
                {
                    importError.Line = line;
                    errors.Add(importError);
                }

                line++;
            }

            importErrors = errors.Any() ? errors : null;
            return !errors.Any();
        }

        public bool ValidateImportData(byte[] binData, out IEnumerable<ImportError> importErrors)
        {
            return ValidateImportData(Encoding.UTF8.GetString(binData), out importErrors);
        }
    }
}
