using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Events;
using MSR.Domain.Exceptions;
using MSR.Domain.SQSEventing.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MSR.Domain.Hub;
using MSR.Domain.Models.Config;
using MSR.Domain.Helpers;
using AutoMapper;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Text;
using MSR.Domain.Abstractions;

namespace MSR.Application.EventServices
{
    public class EventServiceHandler :
        IEventHandler<ImportEvent>,
        IEventHandler<WorkOrderCreateEvent>
    {
        private readonly ICustomerService _customerService;
        private readonly ILocationService _locationService;
        private readonly IPartService _partService;
        private readonly IProcedureService _procedureService;
        private readonly IQuoteService _quoteService;
        private readonly IMessageHubClient _messageHub;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IAnswerRestClient _restClient;

        public EventServiceHandler(
            ICustomerService customerService,
            ILocationService locationService,
            IPartService partService,
            IProcedureService procedureService,
            IQuoteService quoteService,
            GeneralInformation processorConfig,
            IMapper mapper,
            IMessageHubClient messageHub,
            ILogger<EventServiceHandler> logger,
            IAnswerRestClient restClient)
        {
            _customerService = customerService;
            _locationService = locationService;
            _partService = partService;
            _procedureService = procedureService;
            _quoteService = quoteService;
            _messageHub = messageHub;
            _mapper = mapper;
            _logger = logger;
            _restClient = restClient;
        }

        public async Task HandleAsync(ImportEvent handledEvent, CancellationToken cancellationToken = default)
        {
            int count;

            try
            {
                switch (handledEvent.MenuItem)
                {
                    case EnumMenuItem.CustomersDepartments:
                        var importedCustomers = await _customerService.ImportCustomers(
                            Encoding.UTF8.GetString(handledEvent.data)
                        );
                        count = importedCustomers.Count();
                        break;
                    case EnumMenuItem.Locations:
                        var importedLocations = await _locationService.ImportLocations(
                            Encoding.UTF8.GetString(handledEvent.data)
                        );
                        count = importedLocations.Count();
                        break;
                    case EnumMenuItem.Parts:
                        var importedParts = await _partService.ImportParts(
                            Encoding.UTF8.GetString(handledEvent.data)
                        );
                        count = importedParts.Count();
                        break;
                    case EnumMenuItem.RunnableProcedures:
                        var imported = await _procedureService.ImportProcedures(handledEvent.data);
                        count = imported.Count();
                        break;
                    case EnumMenuItem.QuotesProducts:
                        var importedQuotes = await _quoteService.ImportQuotes(
                            Encoding.UTF8.GetString(handledEvent.data)
                        );
                        count = importedQuotes.Count();
                        break;
                    default:
                        throw new DomainException("Import function not found for " +
                            Enum.GetName(handledEvent.MenuItem.GetType(), handledEvent.MenuItem));
                }
                // Note that the current user is set during the message envelope decoding process.
                // This means that the security hole of impersonating a user simply by setting the ID
                // in the SQS message is mitigated.
                await _messageHub.SendNotification(CurrentUser.GetId().ToString(), new Toaster()
                {
                    Message = $"Import {Enum.GetName(handledEvent.MenuItem.GetType(), handledEvent.MenuItem)} " +
                              $"complete.  {count} items imported.",
                    Status = EnumToasterStatus.Success
                });
            }
            catch (Exception e)
            {
                await _messageHub.SendNotification(CurrentUser.GetId().ToString(), new Toaster()
                {
                    Message = $"Import {Enum.GetName(handledEvent.MenuItem.GetType(), handledEvent.MenuItem)} " +
                              $"ERROR: {e}",
                    Status = EnumToasterStatus.Error
                });
                throw;
            }
        }

        public async Task HandleAsync(WorkOrderCreateEvent handledEvent, CancellationToken cancellationToken = default)
        {
            try
            {
                
                var command = _mapper.Map<CreateWorkOrder>(handledEvent.purchaseInfo);
                command.SerialNumbers = handledEvent.serialNumbers;
                command.CustomerLineNumbers = handledEvent.CustomerLineNumbers;
                command.ScheduledStartDate = DateTime.Now;
                command.Qty = handledEvent.purchaseInfo.Qty;

                var workOrderNumber = await _restClient.PostWorkOrderAsync(command, cancellationToken);
                _logger.LogInformation($"Finished creating WorkOrder: {workOrderNumber}");

                await _messageHub.SendNotification(CurrentUser.GetId().ToString(), new Toaster()
                {
                    Message = $"Work Order Created: {workOrderNumber}",
                    Status = EnumToasterStatus.Success
                });
            }
            catch (Exception e)
            {
                var msg = $"Work Order Creation FAILED. ERROR: {e.Message}";
                await _messageHub.SendNotification(CurrentUser.GetId().ToString(), new Toaster()
                {
                    Message = msg,
                    Status = EnumToasterStatus.Error
                });
                
                //We need errors to bubble up all the way so that it can handle sending to the DL queue
                throw;
            }
        }
    }
}
