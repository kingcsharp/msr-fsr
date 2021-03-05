using AutoMapper;
using MSR.Answer.Domain.Models;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using MSR.Domain.SQSEventing.Abstractions;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Views;

namespace MSR.Application.ApplicationServices
{
    public class DocumentAppService :
        ICommandHandler<GetDocument>,
        ICommandHandler<CreateDocument>,
        ICommandHandler<UpdateDocument>,
        ICommandHandler<DeleteDocument>,
        ICommandHandler<GetArchiveDocument>
    {
        private readonly IDocumentService _documentService;

        public DocumentAppService(IDocumentService documentService)
        {
            _documentService = documentService;
        }

        public async Task<ICommandResponse> HandleAsync(GetDocument command, CancellationToken cancellationToken = default)
        {
            var docs = await _documentService.GetDocuments(command);
            int totalRows = await _documentService.GetDocumentTotalRows(command);
            return new PagingCommandResponse<ICollection<DocumentView>>(docs, totalRows, command.Term, command.PageNumber, command.PageSize, command.SortAscending);
        }

        public async Task<ICommandResponse> HandleAsync(CreateDocument command, CancellationToken cancellationToken = default)
        {
            var docs = await _documentService.CreateDocumentAsync(command);
            return new CommandResponse<DocumentView>(docs);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateDocument command, CancellationToken cancellationToken = default)
        {
            var docs = await _documentService.UpdateDocumentAsync(command);
            return new CommandResponse<DocumentView>(docs);
        }

        public async Task<ICommandResponse> HandleAsync(DeleteDocument command, CancellationToken cancellationToken = default)
        {
            await _documentService.DeleteDocumentAsync(command);
            return CommandResponse.SuccessCommand;
        }

        public async Task<ICommandResponse> HandleAsync(GetArchiveDocument command, CancellationToken cancellationToken = default)
        {
            var docs = await _documentService.GetArchiveDocumentAsync(command);
            return new CommandResponse<ICollection<ArchiveDocumentView>>(docs);
        }
    }
}
