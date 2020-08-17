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

namespace MSR.Application.ApplicationServices
{
    public class FileAppService :
        ICommandHandler<GetFiles>,
        ICommandHandler<CreateFile>,
        ICommandHandler<DetachFile>,
        ICommandHandler<UploadFile>,
        ICommandHandler<ImportFile>
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IImportValidatorFactory _validationFactory;
        private readonly ISendSQSMessages _bus;
        //This is only here until we get the full Async lifecycle in
        private readonly ICustomerService _customerService;
        private readonly ILocationService _locationService;

        public FileAppService(
            IFileService fileService,
            IMapper mapper,
            IImportValidatorFactory validationFactory,
            ISendSQSMessages bus,
            ICustomerService customerService,
            ILocationService locationService
            )
        {
            _fileService = fileService;
            _mapper = mapper;
            _validationFactory = validationFactory;
            _bus = bus;
            _customerService = customerService;
            _locationService = locationService;
        }

        public Task<ICommandResponse> HandleAsync(GetFiles command, CancellationToken cancellationToken = default)
        {
            ICollection<FileModel> files = _fileService.ListFiles(
                command.entityName, command.entityId, command.fileId
            );

            ICommandResponse ret = new CommandResponse<ICollection<FileModel>>(files);
            return Task.FromResult(ret);
        }
        public async Task<ICommandResponse> HandleAsync(CreateFile command, CancellationToken cancellationToken = default)
        {
            var file = _mapper.Map<FileModel>(command);
            var ret = await _fileService.CreateFileAsync(command.EntityName, command.EntityId, file);
            return new CommandResponse<FileModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DetachFile command, CancellationToken cancellationToken = default)
        {
            var ret = await _fileService.DetachFilesAsync(command.entityName, command.entityId, command.fileId);
            return new CommandResponse<int>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UploadFile command, CancellationToken cancellationToken = default)
        {
            var ret = await _fileService.UploadHelpFile(command);
            return new CommandResponse<UploadResponse>(ret);

        }

        public async Task<ICommandResponse> HandleAsync(ImportFile command, CancellationToken cancellationToken = default)
        {
            var base64File = Base64Helper.Parse(command.Base64Data);
            var csvData = Encoding.UTF8.GetString(base64File.FileContents).Replace("\r", "").Trim();
            if (csvData.StartsWith(Base64Helper.ByteOrderMarkUtf8, StringComparison.Ordinal))
            {
                csvData = csvData.Remove(0, Base64Helper.ByteOrderMarkUtf8.Length);
            }

            var validator = _validationFactory.Create(command.MenuItem);

            if (!validator.ValidateImportData(csvData, out var importErrors))
            {
                return new CommandResponse<IEnumerable<ImportError>>(importErrors);
            }

            //var importEvent = new ImportEvent()
            //{
            //    CsvData = csvData,
            //    TokenData = "",
            //    MenuItem = command.MenuItem
            //};

            switch (command.MenuItem)
            {
                case EnumMenuItem.CustomersDepartments:
                    var importedCustomers = await _customerService.ImportCustomers(csvData);
                    break;
                case EnumMenuItem.Locations:
                    var importedLocations = await _locationService.ImportLocations(csvData);
                    break;
            }

            //var envelope = new MessageEnvelope(importIntegrationEvent.GetType().Name, importIntegrationEvent);

            //await _bus.SendMessage(envelope);

            return new CommandResponse<IEnumerable<ImportError>>((IEnumerable<ImportError>)null);
        }
    }
}
