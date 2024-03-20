using AutoMapper;
using ClosedXML.Excel;
using MSR.Application.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Helpers;
using MSR.Domain.Models.Query;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.Projections;
using MSR.Infrastructure.Resources.Queries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSR.Application.ViewServices
{
    public class PartViewService : IPartViewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public PartViewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FileContentView> ExportParts(PartExportQueryFilters filters)
        {
            var parts = (await _unitOfWork.Query<Part>().ExportParts(PartProjections.PartExport, filters)).Select(i => new PartExportView()
            {
                Id = i.Id,
                Name = i.Name,
                PartNumber = i.PartNumber,
                OEMPartNumber = i.OEMPartNumber,
                IsKit = i.IsKit,
                IsActive = i.IsActive == true,
                MaximumCycles = i.MaximumCycles,
                SegregationType = i.SegregationType.ToString()
            }).ToList();

            var partsDataTable = parts.ToDataTable("Parts");

            var workbook = new XLWorkbook();
            workbook.AddWorksheet(partsDataTable, "Parts");
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return new FileContentView()
            {
                data = memoryStream.ToArray(),
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileName = $"FilteredParts_{DateTime.UtcNow.ToString("yyyyMMdd")}.xlsx"
            };
        }
    }
}
