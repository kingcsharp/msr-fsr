using System;
using System.Collections.Generic;
using System.Data;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.Models;

namespace MSR.Domain.Validators
{
    public class ProcedureValidator : IValidateImportData
    {
        public ProcedureValidator() { }

        public bool ValidateImportData(byte[] binData, out IEnumerable<ImportError> importErrors)
        {
            var errors = new List<ImportError>();
            DataSet result = null;
            try {
                result = XLSHelper.ParseRecords(binData);
            }
            catch (Exception e)
            {
                errors.Add(new ImportError() {
                    Line = 1,
                    Errors = new List<string>() { e.Message }
                });
            }
            importErrors = errors;
            return result != null;
        }
    }
}
