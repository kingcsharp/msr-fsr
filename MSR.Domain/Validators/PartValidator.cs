using AutoMapper;
using ClosedXML.Excel;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace MSR.Domain.Validators
{

    public class PartValidator : IValidateImportData
    {
        private readonly IMapper _mapper;

        public PartValidator(IMapper mapper) {
            _mapper = mapper;
        }

        public bool ValidateImport(byte[] excelData, out IEnumerable<ImportError> importErrors)
        {
            IEnumerable<ImportError> errors;
            var parts = ReadImportData(excelData, out errors);
            importErrors = errors;
            return parts.Count > 0 && importErrors.Count() == 0;
        }

        public List<PartModel> ReadImportData(byte[] excelData, out IEnumerable<ImportError> importErrors)
        {
            int line = 0;
            List<PartModel> parts = new List<PartModel>();
            List<ImportError> errors = new List<ImportError>();
            try
            {
                var parsedRecords = XLSHelper.ParseRecords(excelData);

                parsedRecords.Tables[0].Rows.Cast<DataRow>().ToList().ForEach(r =>
                {
                    EnumSegregationType segregationType;
                    Enum.TryParse(r["SegregationType"].ToString(), out segregationType);

                    var part = new PartModel
                    {
                        Name = r["Name"].ToString(),
                        PartNumber = r["PartNumber"].ToString(),
                        OEMPartNumber = r["OEMPartNumber"].ToString(),
                        IsKit = Convert.ToBoolean(r["IsKit"]),
                        IsActive = Convert.ToBoolean(r["IsActive"]),
                        NickName = r["NickName"].ToString(),
                    };

                    if (r["Id"] != DBNull.Value && !string.IsNullOrWhiteSpace(r["Id"].ToString()))
                    {
                        part.Id = Convert.ToInt32(r["Id"]);
                    }

                    if (Enum.IsDefined(typeof(EnumSegregationType), segregationType))
                    {
                        part.SegregationType = segregationType;
                    }

                    if (r["MaximumCycles"] != DBNull.Value && !string.IsNullOrWhiteSpace(r["MaximumCycles"].ToString()))
                    { 
                        part.MaximumCycles = Convert.ToInt32(r["MaximumCycles"]);
                    }
                    parts.Add(part);
                });
            }
            catch (Exception e)
            {
                var ie = new ImportError() { Line = line };
                ie.Errors.Add(e.Message);
                errors.Add(ie);
            }

            importErrors = errors;

            return parts;
        }

        public bool ValidateImportData(byte[] binData, out IEnumerable<ImportError> importErrors)
        {
            return ValidateImport(binData, out importErrors);
        }
    }
}
