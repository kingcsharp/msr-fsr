using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MSR.Application.Abstractions;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Application.ViewServices
{
    public class WorkOrderPartViewService: IWorkOrderPartViewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly IDocumentService _documentService;
        private readonly IFileService _fileService;

        public WorkOrderPartViewService(IUnitOfWork unitOfWork, IMapper mapper, IDocumentService documentService, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _documentService = documentService;
            _fileService = fileService;
        }

        public async Task<NCRPartWODetailView> GetNCRPartWODetail(int workOrderPartId)
        {
            var data = _unitOfWork.Context.SqlQuery<NCRPartWODetail>("dbo.sNCRPartWODetail @WorkOrderPartId", new SqlParameter("@WorkOrderPartId", workOrderPartId));
            var curTask = data[0].WorkOrderTaskId;
            var ncrPartWODetailView = new NCRPartWODetailView()
            {
                WorkOrderId = data[0].WorkOrderId,
                PartName = data[0].PartName,
                PartNumber = data[0].PartNumber,
                SerialNumber = data[0].SerialNumber,
                WorkOrderCompletedDate = data[0].WorkOrderCompletedDate,
                WorkOrderPartId = data[0].WorkOrderPartId,
                WorkOrderTasks = new List<NCRPartWOTaskView>()
            };
            var workOrderTask = new NCRPartWOTaskView()
            {
                WorkOrderTaskId = data[0].WorkOrderTaskId,
                WorkOrderTaskTitle = data[0].WorkOrderTaskTitle,
                WorkOrderTaskMonitors = new List<WorkOrderPartMonitorView>()
            };

            workOrderTask.Files = _fileService.ListFiles(nameof(WorkOrderTaskModel), data[0].WorkOrderTaskId).ToList();
            workOrderTask.Documents = await GetDocumentsForTask(data[0].WorkOrderTaskId);

            foreach (var record in data)
            {
                if(record.WorkOrderTaskId != curTask)
                {
                    curTask = record.WorkOrderTaskId;
                    ncrPartWODetailView.WorkOrderTasks.Add(workOrderTask);
                    workOrderTask = new NCRPartWOTaskView
                    {
                        WorkOrderTaskId = record.WorkOrderTaskId,
                        WorkOrderTaskTitle = record.WorkOrderTaskTitle,
                        WorkOrderTaskMonitors = new List<WorkOrderPartMonitorView>(),
                    };

                    workOrderTask.Files = _fileService.ListFiles(nameof(WorkOrderTaskModel), record.WorkOrderTaskId).ToList();
                    workOrderTask.Documents = await GetDocumentsForTask(record.WorkOrderTaskId);
                }

                var workOrderTaskMonitor = new WorkOrderPartMonitorView
                {
                    WorkOrderTaskMonitorId = record.WorkOrderTaskMonitorId,
                    WorkOrderTaskMonitorDescription = record.WorkOrderTaskMonitorDescription,
                    Result = record.Result,
                    Comment = record.Comment
                };
                workOrderTask.WorkOrderTaskMonitors.Add(workOrderTaskMonitor);
                
            }
            ncrPartWODetailView.WorkOrderTasks.Add(workOrderTask);




            return ncrPartWODetailView;
        }


        public async Task<List<DocumentView>> GetDocumentsForTask(int taskId)
        {
            //Get Prodecure Steps for Tasks
            var procedureStepIds = await _unitOfWork.WorkOrderTasks.Query().Where(i => i.Id == taskId).Select(i => i.ProcedureStepId).ToListAsync();
            //Now go to Document Entity Map and get them all
            var documentIds = await _unitOfWork.DocumentEntityMap.Query().Where(s =>
                procedureStepIds.Contains(s.EntityId) &&
                s.EntityTableName == nameof(ProcedureStep)).Select(i => i.DocumentId).ToListAsync();
            var documentViews = new List<DocumentView>();
            foreach (var id in documentIds)
            {
                var documentEntities = await _unitOfWork.Documents.Query().CreateDocumentQuery(new GetDocument()
                {
                    Id = id
                }).ToListAsync();

                documentViews = _mapper.Map<ICollection<DocumentView>>(documentEntities).ToList();

                foreach (var documentView in documentViews)
                {
                    documentView.ReferenceFiles = _fileService.ListFiles(nameof(Document), documentView.Id);
                }
            }

            return documentViews;
        }
    }
}
