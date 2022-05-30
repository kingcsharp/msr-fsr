using AutoMapper;
using MSR.Application.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models.Query;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System;
using System.Threading.Tasks;
using MSR.Infrastructure.Resources.Projections;
using MSR.Infrastructure.Resources.Queries;
using System.Linq;
using ExcelDataReader;
using System.Data;
using MSR.Domain.Helpers;
using ClosedXML.Excel;
using System.IO;

namespace MSR.Application.ViewServices
{
    public class ProcedureViewService : IProcedureViewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public ProcedureViewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FileContentView> ExportProcedures(ProcedureExportQueryFilters filters)
        {
            var procedures = (await _unitOfWork.Query<Procedure>().ExportProcedures(ProcedureProjections.ProcedureExport, filters)).Select(i => new ProcedureExportView()
            {
                Comments = i.Comments,
                Duration = i.Duration,
                DurationType = i.DurationType,
                Id = i.Id,
                Name = i.Name,
                ProcedureType = i.ProcedureTypeId,
                Revision = i.Revision
            }).ToList();

            var procedureIds = procedures.Select(i => i.Id).ToList();

            var procedureSteps = _unitOfWork.ProcedureSteps.Query().Where(i => procedureIds.Contains(i.ProcedureId.Value)).Select(j => new ProcedureStepExportView()
            {
                Id = j.Id,
                ProcedureId = j.ProcedureId.Value,
                EquipmentTime = j.EquipmentTime,
                LaborTime = j.LaborTime,
                PrintOrder = j.PrintOrder,
                ProcedureName = null,
                ProcedureStepTypeId = j.ProcedureStepTypeId.Value,
                RepacementCost = j.ReplacementCost,
                StepText = j.StepText,
                Title = j.Title,
                UsefulLife = j.UsefulLife,
                Utilization = j.Utilization
            }).ToList();

            var procedureDataTable = procedures.ToDataTable("Procedures");
            var procedureStepDataTable = procedureSteps.ToDataTable("ProcedureSteps");

            var workbook = new XLWorkbook();
            workbook.AddWorksheet(procedureDataTable, "Procedures");
            workbook.AddWorksheet(procedureStepDataTable, "ProcedureSteps");
            using var memoryStream = new MemoryStream();
            workbook.SaveAs(memoryStream);
            return new FileContentView()
            {
                data = memoryStream.ToArray(),
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileName = $"FilteredProcedures_{DateTime.UtcNow.ToString("yyyyMMdd")}.xlsx"
            };
        }
    }
}
