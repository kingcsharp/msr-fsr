using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Events;
using MSR.Domain.Exceptions;
using MSR.Domain.Helpers;
using MSR.Domain.SQSEventing.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.EventServices
{
    public class EventServiceHandler : IEventHandler<ImportEvent>
    {
        private ICustomerService _customerService;
        private ILocationService _locationService;
        private IPartService _partService;
        private IMessageHubClient _messageHub;

        public EventServiceHandler(
            ICustomerService customerService,
            ILocationService locationService,
            IPartService partService,
            IMessageHubClient messageHub)
        {
            _customerService = customerService;
            _locationService = locationService;
            _partService = partService;
            _messageHub = messageHub;
        }

        public async Task HandleAsync(ImportEvent handledEvent, CancellationToken cancellationToken = default)
        {
            await _messageHub.Connect("https://localhost:44398/msg"); // TODO: hardcoded url
            int count;

            try {
                switch(handledEvent.MenuItem)
                {
                    case EnumMenuItem.CustomersDepartments:
                        var importedCustomers = await _customerService.ImportCustomers(handledEvent.CsvData);
                        count = importedCustomers.Count();
                        break;
                    case EnumMenuItem.Locations:
                        var importedLocations = await _locationService.ImportLocations(handledEvent.CsvData);
                        count = importedLocations.Count();
                        break;
                    case EnumMenuItem.Parts:
                        var importedParts = await _partService.ImportLocations(handledEvent.CsvData);
                        count = importedParts.Count();
                        break;
                    default:
                        throw new DomainException("Import function not found for " +
                            Enum.GetName(handledEvent.MenuItem.GetType(), handledEvent.MenuItem));
                }

                _messageHub.SendNotification(handledEvent.UserId,
                    $"Import {Enum.GetName(handledEvent.MenuItem.GetType(), handledEvent.MenuItem)} " +
                    $"complete.  {count} items imported.");

            } catch (Exception e) {
                _messageHub.SendNotification(handledEvent.UserId,
                    $"Import {Enum.GetName(handledEvent.MenuItem.GetType(), handledEvent.MenuItem)} " +
                    $"ERROR: {e.Message}");
            }
        }
    }
}
