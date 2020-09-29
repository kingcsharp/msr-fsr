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
        ICommandHandler<DeleteDocument>
    {
        private readonly IDocumentService _documentService;
        private readonly IMapper _mapper;
        private readonly ISendSQSMessages _bus;

        public DocumentAppService(IDocumentService documentService, IMapper mapper)
        {
            _documentService = documentService;
            _mapper = mapper;
        }

        public async Task<ICommandResponse> HandleAsync(GetDocument command, CancellationToken cancellationToken = default)
        {
            var docs = await _documentService.GetDocuments(command.Id);
            return new CommandResponse<ICollection<DocumentView>>(docs);
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
    }
}
