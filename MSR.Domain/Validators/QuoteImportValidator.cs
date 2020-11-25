using MSR.Domain.Abstractions.Services;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;

namespace MSR.Domain.Validators
{
    public class QuoteImportValidator: IValidateImportData
    {
        public bool ValidateImportData(string csvData, out IEnumerable<ImportError> importErrors)
        {
            List<ImportError> errors = new List<ImportError>();
            int line = 0;
            IEnumerable<QuoteImportItem> records = null;
            ImportError importError;

            try
            {
                records = CSVHelper.ParseRecords<QuoteImportItem>(csvData);
            }
            catch (Exception e)
            {
                importError = new ImportError() { Line = line };
                importError.Errors.Add(e.Message);
                errors.Add(importError);
            }

            if (!records.Any())
            {
                importError = new ImportError() { Line = line };
                importError.Errors.Add($"This file does not have records to import.");
                errors.Add(importError);
            }

            foreach (QuoteImportItem record in records)
            {
                line++;
                importError = new ImportError();

                if (record.CustomerId == 0)
                {
                    importError.Errors.Add($"{nameof(record.CustomerId)} does not have a value");
                }

                if (importError.Errors.Any())
                {
                    importError.Line = line;
                    errors.Add(importError);
                }
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
