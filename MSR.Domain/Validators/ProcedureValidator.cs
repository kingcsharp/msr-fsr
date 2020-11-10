using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using AutoMapper;
using ExcelDataReader;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models;

namespace MSR.Domain.Validators
{
    // Helper class to hold import data that doesn't
    // directly map to Answer 3.0 classes
    public class CreateProcedureStepImport
    {
        public string COMMENT { get; set; }
        public string EXTRA_NOTE1 { get; set; }
        public string SERIALIZE { get; set; }
        public string SUCCESS_MONITOR { get; set; }
        public string INTERNAL_LOCATION { get; set; }
        public string LOC_TYPE { get; set; }
        public string REF_DOC_ID { get; set; }
    }

    // Helper class to hold import data that doesn't
    // directly map to Answer 3.0 classes
    public class CreateProcedureImport
    {
        public string ANS_ID { get; set; }
    }

    public class ParsedProcedureImport
    {
        public Dictionary<string, CreateProcedure> procedures;
        public Dictionary<string, CreateProcedureImport> procedureExtras;
        public Dictionary<string, List<CreateProcedureStep>> procedureSteps;
        public Dictionary<string, List<CreateProcedureStepImport>> procedureStepExtras;
    }

    public class ProcedureValidator : IValidateImportData
    {
        const int IMPORT_TABLE_COUNT = 2;
        const string PROC_STEP_EXPORT = "PROC_STEP_EXPORT";
        const string PROCEDURE_NAME_EXPORT = "PROCEDURE_NAME_EXPORT";
        const int PROCEDURESTEPS_COLUMNS_COUNT = 13;
        const int PROCEDURES_COLUMNS_COUNT = 4;

        private readonly IMapper _mapper;

        public ProcedureValidator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public bool ValidateImportData(byte[] binData, out IEnumerable<ImportError> importErrors)
        {
            ParsedProcedureImport import = ValidateAndReturnImportData(binData, out importErrors);
            return import != null;
        }

        public ParsedProcedureImport ValidateAndReturnImportData(byte[] binData, out IEnumerable<ImportError> importErrors)
        {
            var errors = new List<ImportError>();
            List<string> errorStrings = new List<string>();
            var stream = new MemoryStream(binData);
            var reader = ExcelReaderFactory.CreateReader(stream);

            var result = reader.AsDataSet();
            DataTableCollection tables = result.Tables;

            if (tables.Count != IMPORT_TABLE_COUNT)
            {
                errorStrings.Add($"Invalid table count {tables.Count} != 2");
                errors.Add(new ImportError() { Errors = errorStrings });
                importErrors = errors;
                return null;
            }

            DataTable procedureSteps = tables[0];
            DataTable procedures = tables[1];

            if (!procedureSteps.TableName.ToUpper().Equals(PROC_STEP_EXPORT))
            {
                errorStrings.Add($"Invalid table name: {procedureSteps.TableName} != {PROC_STEP_EXPORT}");
                errors.Add(new ImportError() { Errors = errorStrings });
                importErrors = errors;
                return null;
            }

            if (!procedures.TableName.ToUpper().Equals(PROCEDURE_NAME_EXPORT))
            {
                errorStrings.Add($"Invalid table name: {procedureSteps.TableName} != {PROCEDURE_NAME_EXPORT}");
                errors.Add(new ImportError() { Errors = errorStrings });
                importErrors = errors;
                return null;
            }

            if (procedureSteps.Columns.Count != PROCEDURESTEPS_COLUMNS_COUNT)
            {
                errorStrings.Add(
                    $"Invalid {PROC_STEP_EXPORT} row count " +
                    $"{procedureSteps.Columns.Count} != " +
                    PROCEDURESTEPS_COLUMNS_COUNT.ToString()
                );
                errors.Add(new ImportError() { Errors = errorStrings });
                importErrors = errors;
                return null;
            }

            if (procedures.Columns.Count != PROCEDURES_COLUMNS_COUNT)
            {
                errorStrings.Add(
                    $"Invalid {PROC_STEP_EXPORT} row " +
                    $"count {procedures.Columns.Count} != " +
                    PROCEDURES_COLUMNS_COUNT.ToString()
                );
                errors.Add(new ImportError() { Errors = errorStrings });
                importErrors = errors;
                return null;
            }

            bool isHeader;
            Dictionary<string, int> fieldMap;
            int lineNumber = 1;

            //
            // procedure import
            //
            isHeader = true;
            fieldMap = new Dictionary<string, int>();
            Dictionary<string, CreateProcedure> newProcs =
                new Dictionary<string, CreateProcedure>();
            Dictionary<string, CreateProcedureImport> newProcsExtra =
                new Dictionary<string, CreateProcedureImport>();
            foreach (DataRow proc in procedures.Rows)
            {
                string procedureIdString = "";
                try
                {
                    if (isHeader)
                    {
                        int i = 0;
                        foreach (string item in proc.ItemArray)
                        {
                            fieldMap.Add(item, i);
                            i += 1;
                        }
                        isHeader = false;
                        lineNumber += 1;
                        continue;
                    }

                    CreateProcedure newProc = new CreateProcedure();
                    CreateProcedureImport newProcExtra =
                        new CreateProcedureImport();
                    procedureIdString = proc.ItemArray[fieldMap["PROCEDURE_ID"]].ToString();
                    newProc.Name = proc.ItemArray[fieldMap["PROCEDURE_NAME"]].ToString();
                    newProcExtra.ANS_ID = proc.ItemArray[fieldMap["ANS_ID"]].ToString();
                    newProc.ProcedureTypeId = Convert.ToInt32((double) proc.ItemArray[fieldMap["PROC_TYPE_ID"]]);

                    // Store the new procedure data in memory.
                    if (newProcs.ContainsKey(procedureIdString))
                    {
                        errorStrings = new List<string>();
                        errorStrings.Add($"Import ERROR: duplicate key '{procedureIdString}'");
                        errors.Add(new ImportError() { Errors = errorStrings });
                    }
                    newProcs.Add(procedureIdString, newProc);
                    newProcsExtra.Add(procedureIdString, newProcExtra);
                }
                catch (InvalidCastException e)
                {
                    errorStrings = new List<string>();
                    errorStrings.Add($"PROC({procedureIdString}) Parse Error: {e.Message}");
                    errors.Add(new ImportError()
                    {
                        Errors = errorStrings,
                            Line = lineNumber
                    });
                }
                lineNumber += 1;
            }

            //
            // procedure step import
            //
            isHeader = true;
            fieldMap = new Dictionary<string, int>();
            Dictionary<string, List<CreateProcedureStep>> newSteps =
                new Dictionary<string, List<CreateProcedureStep>>();
            Dictionary<string, List<CreateProcedureStepImport>> newStepsExtra =
                new Dictionary<string, List<CreateProcedureStepImport>>();
            foreach (DataRow step in procedureSteps.Rows)
            {
                string procedureIdString = "";
                try
                {
                    if (isHeader)
                    {
                        int i = 0;
                        foreach (string item in step.ItemArray)
                        {
                            fieldMap.Add(item, i);
                            i += 1;
                        }
                        isHeader = false;
                        lineNumber += 1;
                        continue;
                    }

                    CreateProcedureStep newStep = new CreateProcedureStep();
                    CreateProcedureStepImport newStepExtra =
                        new CreateProcedureStepImport();

                    procedureIdString = step.ItemArray[fieldMap["PROCEDURE_ID"]].ToString();
                    newStep.StepText = step.ItemArray[fieldMap["STEP_TEXT"]].ToString();
                    newStep.PrintOrder = Convert.ToInt32((double) step.ItemArray[fieldMap["PRINT_ORDER"]]);
                    newStepExtra.COMMENT = step.ItemArray[fieldMap["COMMENT"]].ToString();
                    newStep.LaborTime = Convert.ToInt32(((double) step.ItemArray[fieldMap["STEP_TIME"]]));
                    newStepExtra.EXTRA_NOTE1 = step.ItemArray[fieldMap["EXTRA_NOTE1"]].ToString();
                    newStep.Roles = ((double) step.ItemArray[fieldMap["DEFAULT_ROLE_ID"]]).ToString();
                    newStepExtra.SERIALIZE = step.ItemArray[fieldMap["SERIALIZE"]].ToString();
                    newStepExtra.SUCCESS_MONITOR = step.ItemArray[fieldMap["SUCCESS_MONITOR"]].ToString();
                    newStepExtra.INTERNAL_LOCATION = step.ItemArray[fieldMap["INTERNAL_LOCATION"]].ToString();
                    newStepExtra.LOC_TYPE = step.ItemArray[fieldMap["LOC_TYPE"]].ToString();
                    newStep.Title = step.ItemArray[fieldMap["Title"]].ToString();

                    // Reference documents are stored by string.  The IDs will need
                    // to be fetched from database later
                    newStepExtra.REF_DOC_ID = step.ItemArray[fieldMap["REF_DOC_ID"]].ToString();

                    // The IDs used in the spreadsheet will need to be collated at creation
                    // time, so for now we just store a separate mapping.
                    List<CreateProcedureStep> psmImport;
                    if (newSteps.ContainsKey(procedureIdString))
                    {
                        psmImport = newSteps[procedureIdString];
                    }
                    else
                    {
                        psmImport = new List<CreateProcedureStep>();
                        newSteps.Add(procedureIdString, psmImport);
                    }
                    psmImport.Add(newStep);

                    List<CreateProcedureStepImport> psmiImport;
                    if (newStepsExtra.ContainsKey(procedureIdString))
                    {
                        psmiImport = newStepsExtra[procedureIdString];
                    }
                    else
                    {
                        psmiImport = new List<CreateProcedureStepImport>();
                        newStepsExtra.Add(procedureIdString, psmiImport);
                    }
                    psmiImport.Add(newStepExtra);
                }
                catch (InvalidCastException e)
                {
                    errorStrings = new List<string>();
                    errorStrings.Add($"STEP({procedureIdString}) Parse Error: {e.Message}");
                    errors.Add(new ImportError()
                    {
                        Errors = errorStrings,
                            Line = lineNumber
                    });
                }
                lineNumber += 1;
            }

            importErrors = errors;
            return new ParsedProcedureImport()
            {
                procedures = newProcs,
                    procedureExtras = newProcsExtra,
                    procedureSteps = newSteps,
                    procedureStepExtras = newStepsExtra
            };
        }
    }
}
