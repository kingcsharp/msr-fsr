using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Events;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.SQSEventing.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.EventServices
{
    public class EventServiceHandler : IEventHandler<ImportEvent>
    {
        private ICustomerService _customerService;
        private ILocationService _locationService;
        private IMessageHubClient _messageHub;

        public EventServiceHandler(ICustomerService customerService, ILocationService locationService, IMessageHubClient messageHub)
        {
            _customerService = customerService;
            _locationService = locationService;
            _messageHub = messageHub;
        }

        public async Task HandleAsync(ImportEvent handledEvent, CancellationToken cancellationToken = default)
        {
            await _messageHub.Connect("https://localhost:44398/msg"); // TODO: hardcoded url

            try {
                switch(handledEvent.MenuItem)
                {
                    case EnumMenuItem.CustomersDepartments:
                        var importedCustomers = await _customerService.ImportCustomers(handledEvent.CsvData);
                        break;
                    case EnumMenuItem.Locations:
                        var importedLocations = await _locationService.ImportLocations(handledEvent.CsvData);
                        break;
                    // case EnumMenuItem.Parts: TODO
                    default:
                        throw new DomainException("Import function not found for " +
                            Enum.GetName(handledEvent.MenuItem.GetType(), handledEvent.MenuItem));
                }
            } catch (Exception e) {
                _messageHub.SendNotification(CurrentUser.GetId().ToString(), $"ERROR: {e.Message}");
            }

            //TODO: ADD IN SIGNALR STUFF HERE

        }
    }
}
