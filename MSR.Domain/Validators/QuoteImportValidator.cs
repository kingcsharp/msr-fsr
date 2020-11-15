using MSR.Domain.Abstractions.Services;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MSR.Domain.Validators
{
    public class QuoteImportValidator: IValidateImportData
    {
        public bool ValidateImportData(string csvData, out IEnumerable<ImportError> importErrors)
        {
            List<ImportError> errors = new List<ImportError>();
            int line = 0;
            IEnumerable records = null;

            try
            {
                records = CSVHelper.ParseRecords<QuoteImportItem>(csvData);
            }
            catch (Exception e)
            {
                var ie = new ImportError() { Line = line };
                ie.Errors.Add(e.Message);
                errors.Add(ie);
            }

            foreach (QuoteImportItem record in records)
            {
                line++;
                var importError = new ImportError();

                if (record.CustomerId == 0) {
                    importError.Errors.Add($"{nameof(record.CustomerId)} does not have a value");
                }
                if (string.IsNullOrWhiteSpace(record.QuoteJson) && string.IsNullOrWhiteSpace(record.CustomerRequirementJson))
                {
                    importError.Errors.Add($"{nameof(record.QuoteJson)} or {nameof(record.CustomerRequirementJson)} does not have a value");
                }

                if (importError.Errors.Any())
                {
                    importError.Line = line;
                    errors.Add(importError);
                }
            }

            if (!errors.Any() && line == 0) {
                var ie = new ImportError() { Line = line };
                ie.Errors.Add($"file does not have import data.");
                errors.Add(ie);
            }

            importErrors = errors.Any() ? errors : null;
            return !errors.Any();
        }
    }
}
