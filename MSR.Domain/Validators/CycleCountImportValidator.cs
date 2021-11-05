using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSR.Domain.Validators
{

    public class CycleCountImportValidator : IValidateImportData
    {
        private readonly IMapper _mapper;

        public CycleCountImportValidator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public bool ValidateImportData(string csvData, out IEnumerable<ImportError> importErrors)
        {
            IEnumerable<ImportError> errors;
            var cycleCounts = ReadImportData(csvData, out errors);
            importErrors = errors;
            return cycleCounts.Count > 0 && importErrors.Count() == 0;
        }
        public List<CycleCountHistoryModel> ReadImportData(string csvData, out IEnumerable<ImportError> importErrors)
        {
            int line = 0;
            List<CycleCountHistoryModel> cycleCounts = new List<CycleCountHistoryModel>();
            List<ImportError> errors = new List<ImportError>();
            IEnumerable records = null;
            try
            {
                records = CSVHelper.ParseRecords<CycleCountHistoryImportItem>(csvData);
            }
            catch (Exception e)
            {
                var importError = new ImportError() { Line = line };
                importError.Errors.Add(e.Message);
                errors.Add(importError);
            }

            foreach (CycleCountHistoryImportItem record in records)
            {
                line += 1;

                if (String.IsNullOrEmpty(record.PartNumber) && String.IsNullOrEmpty(record.SerialNumber) && String.IsNullOrEmpty(record.CycleCount)) continue;
                
                try
                {
                    var cycleCount = _mapper.Map<CycleCountHistoryModel>(record);
                    if (int.TryParse(record.CycleCount.ToString(), out var cycleCountValue))
                    {
                        cycleCount.CycleCount = cycleCountValue;
                    }
                    cycleCounts.Add(cycleCount);
                }
                catch (Exception e)
                {
                    var ie = new ImportError() { Line = line };
                    ie.Errors.Add(e.Message);
                    errors.Add(ie);
                }
            }

            importErrors = errors;

            return cycleCounts;
        }

        public bool ValidateImportData(byte[] binData, out IEnumerable<ImportError> importErrors)
        {
            return ValidateImportData(Encoding.UTF8.GetString(binData), out importErrors);
        }
    }
}
