using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
            var errorStrings = new List<string>();

            const int IMPORT_TABLE_COUNT = 2;
            const string PROCEDURESTEPIMPORT = "ProcedureSteps";
            const string PROCEDUREIMPORT = "Procedures";
            const int PROCEDURESTEPS_COLUMNS_COUNT = 12;
            const int PROCEDURES_COLUMNS_COUNT = 7;

            try 
            {
                var result = XLSHelper.ParseRecords(binData);
                var tables = result.Tables;

                if (tables.Count != IMPORT_TABLE_COUNT)
                {
                    errorStrings.Add($"Invalid table count {tables.Count} != 2");
                    errors.Add(new ImportError() { Errors = errorStrings });
                    importErrors = errors;
                    return false;
                }

                var procedures = tables[0];
                var procedureSteps = tables[1];

                if (!procedureSteps.TableName.ToUpper().Equals(PROCEDURESTEPIMPORT.ToUpper()))
                {
                    errorStrings.Add($"Invalid table name: {procedureSteps.TableName} != {PROCEDURESTEPIMPORT}");
                    errors.Add(new ImportError() { Errors = errorStrings });
                }

                if (!procedures.TableName.ToUpper().Equals(PROCEDUREIMPORT.ToUpper()))
                {
                    errorStrings.Add($"Invalid table name: {procedureSteps.TableName} != {PROCEDUREIMPORT}");
                    errors.Add(new ImportError() { Errors = errorStrings });
                }

                if (procedureSteps.Columns.Count != PROCEDURESTEPS_COLUMNS_COUNT)
                {
                    errorStrings.Add(
                        $"Invalid {PROCEDURESTEPIMPORT} row count " +
                        $"{procedureSteps.Columns.Count} != " +
                        PROCEDURESTEPS_COLUMNS_COUNT.ToString()
                    );
                    errors.Add(new ImportError() { Errors = errorStrings });
                }

                if (procedures.Columns.Count != PROCEDURES_COLUMNS_COUNT)
                {
                    errorStrings.Add(
                        $"Invalid {PROCEDURESTEPIMPORT} row " +
                        $"count {procedures.Columns.Count} != " +
                        PROCEDURES_COLUMNS_COUNT.ToString()
                    );
                    errors.Add(new ImportError() { Errors = errorStrings });
                }
            }
            catch (Exception e)
            {
                errors.Add(new ImportError() {
                    Line = 1,
                    Errors = new List<string>() { e.Message }
                });
            }
            importErrors = errors;
            return !errors.Any();
        }
    }
}
