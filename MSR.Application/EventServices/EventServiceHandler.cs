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
        private IQuoteService _quoteService;
        private IMessageHubClient _messageHub;
        private GeneralInformation _processorConfig;
        private IWorkOrderService _workOrderService;
        private IMapper _mapper;
        private ILogger _logger;

        public EventServiceHandler(
            ICustomerService customerService,
            ILocationService locationService,
            IPartService partService,
            IProcedureService procedureService,
            IQuoteService quoteService,
            GeneralInformation processorConfig,
            IWorkOrderService workOrderService,
            IMapper mapper,
            IMessageHubClient messageHub,
            ILogger<EventServiceHandler> logger)
        {
            _customerService = customerService;
            _locationService = locationService;
            _partService = partService;
            _procedureService = procedureService;
            _quoteService = quoteService;
            _processorConfig = processorConfig;
            _messageHub = messageHub;
            _workOrderService = workOrderService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task HandleAsync(ImportEvent handledEvent, CancellationToken cancellationToken = default)
        {
            Uri baseUri = new Uri(_processorConfig.APIURL);
            UriBuilder hubUri = new UriBuilder(baseUri.Scheme, baseUri.Host, baseUri.Port, "msg");
            await _messageHub.Connect(hubUri.ToString());
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
                _messageHub.SendNotification(CurrentUser.GetId().ToString(), new Toaster()
                {
                    Message = $"Import {Enum.GetName(handledEvent.MenuItem.GetType(), handledEvent.MenuItem)} " +
                              $"complete.  {count} items imported.",
                    Status = EnumToasterStatus.Success
                });
            }
            catch (Exception e)
            {
                _messageHub.SendNotification(CurrentUser.GetId().ToString(), new Toaster()
                {
                    Message = $"Import {Enum.GetName(handledEvent.MenuItem.GetType(), handledEvent.MenuItem)} " +
                              $"ERROR: {e.Message}",
                    Status = EnumToasterStatus.Error
                });
                throw;
            }
        }

        public async Task HandleAsync(WorkOrderCreateEvent handledEvent, CancellationToken cancellationToken = default)
        {
            try
            {
                Uri baseUri = new Uri(_processorConfig.APIURL);
                UriBuilder hubUri = new UriBuilder(baseUri.Scheme, baseUri.Host, baseUri.Port, "msg");
                await _messageHub.Connect(hubUri.ToString());

                var command = _mapper.Map<CreateWorkOrder>(handledEvent.purchaseInfo);
                command.ScheduledStartDate = DateTime.Now;

                ICollection<WorkOrderTaskModel> tasks = await _workOrderService.GetWorkOrderTasksAsync(command);
                command.WorkOrderTasks = tasks;

                List<WorkOrderPartModel> parts =
                    (await _workOrderService.GetWorkOrderPartsAsync(command))
                    .ToList();

                // Copy in the serial numbers entered at purchase time, if any.
                int workOrderPartIndex;
                for (workOrderPartIndex = 0;
                     workOrderPartIndex < handledEvent.serialNumbers.Count &&
                     workOrderPartIndex < parts.Count;
                     workOrderPartIndex += 1)
                {
                    if (handledEvent.serialNumbers[workOrderPartIndex] != null)
                    {
                        parts[workOrderPartIndex].SerialNumber =
                            handledEvent.serialNumbers[workOrderPartIndex];
                    }

                }
                command.WorkOrderParts = parts;

                foreach (var commandWorkOrderTask in command.WorkOrderTasks)
                {
                    commandWorkOrderTask.Title = commandWorkOrderTask.ProcedureStep.Title;
                    commandWorkOrderTask.StepText = commandWorkOrderTask.ProcedureStep.StepText;

                    foreach (var workOrderTaskMonitor in commandWorkOrderTask.WorkOrderTaskMonitors)
                    {
                        workOrderTaskMonitor.Description = workOrderTaskMonitor.ProcedureStepMonitor.Description;
                        workOrderTaskMonitor.MonitorListId = workOrderTaskMonitor.ProcedureStepMonitor.MonitorListId;
                        workOrderTaskMonitor.ShouldBe = workOrderTaskMonitor.ProcedureStepMonitor.ShouldBe;
                        workOrderTaskMonitor.HighTarget = workOrderTaskMonitor.ProcedureStepMonitor.HighTarget;
                        workOrderTaskMonitor.LowTarget = workOrderTaskMonitor.ProcedureStepMonitor.LowTarget;
                        workOrderTaskMonitor.TargetValue = workOrderTaskMonitor.ProcedureStepMonitor.TargetValue;
                        workOrderTaskMonitor.FaultHandling = workOrderTaskMonitor.ProcedureStepMonitor.FaultHandling;
                        workOrderTaskMonitor.SensorName = workOrderTaskMonitor.ProcedureStepMonitor.SensorName;
                    }
                }


                WorkOrderModel model = await _workOrderService.CreateWorkOrderAsync(command);
                string wonum = _workOrderService.GetWorkOrderItemNumber(model);

                _logger.LogInformation($"Finished creating WorkOrder: {wonum}");

                _messageHub.SendNotification(CurrentUser.GetId().ToString(), new Toaster()
                {
                    Message = $"Work Order Created: {wonum}",
                    Status = EnumToasterStatus.Success
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                string msg = "Work Order Creation FAILED. " +
                             $"ERROR: {e.Message}";
                _messageHub.SendNotification(CurrentUser.GetId().ToString(), new Toaster()
                {
                    Message = msg,
                    Status = EnumToasterStatus.Error
                });
                // propogate up to log the error
            }
        }
    }
}
