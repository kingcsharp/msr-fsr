using AutoMapper;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MSR.Domain.Validators
{
    public class PartCSVRecord
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public string OEMPartNumber { get; set; }
        public string NickName { get; set; }
        public int MaximumCycles { get; set; }
    }

    public class PartValidator : IValidateImportData
    {
        private readonly IMapper _mapper;

        public PartValidator(IMapper mapper) {
            _mapper = mapper;
        }

        public bool ValidateImportData(string csvData, out IEnumerable<ImportError> importErrors)
        {
            IEnumerable<ImportError> errors;
            var parts = ReadImportData(csvData, out errors);
            importErrors = errors;
            return parts.Count > 0 && importErrors.Count() == 0;
        }
        public List<PartModel> ReadImportData(string csvData, out IEnumerable<ImportError> importErrors)
        {
            int line = 0;
            List<PartModel> parts = new List<PartModel>();
            List<ImportError> errors = new List<ImportError>();
            IEnumerable records = null;
            try {
                records = CSVHelper.ParseRecords<PartCSVRecord>(csvData);

            } catch (Exception e) {
                var ie = new ImportError() { Line = line };
                ie.Errors.Add(e.Message);
                errors.Add(ie);
            }

            // parse the file in-memory to ensure valid data
            foreach (PartCSVRecord record in records)
            {
                line += 1;
                try {
                    var part = _mapper.Map<PartModel>(record);
                    parts.Add(part);
                } catch (Exception e) {
                    var ie = new ImportError() { Line = line };
                    ie.Errors.Add(e.Message);
                    errors.Add(ie);
                }
            }

            importErrors = errors;

            return parts;
        }

    }
}
