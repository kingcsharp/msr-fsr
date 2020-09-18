using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Events;
using MSR.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using MSR.Domain.SQSEventing.Abstractions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MSR.Domain.Hub;
using MSR.Domain.Models.Config;
using MSR.Domain.Helpers;

namespace MSR.Application.EventServices
{
    public class EventServiceHandler : IEventHandler<ImportEvent>
    {
        private ICustomerService _customerService;
        private ILocationService _locationService;
        private IPartService _partService;
        private IMessageHubClient _messageHub;
        private GeneralInformation _processorConfig;

        public EventServiceHandler(
            ICustomerService customerService,
            ILocationService locationService,
            IPartService partService,
            GeneralInformation processorConfig,
            IMessageHubClient messageHub)
        {
            _customerService = customerService;
            _locationService = locationService;
            _partService = partService;
            _processorConfig = processorConfig;
            _messageHub = messageHub;
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
                    Status = EnumToasterStatus.Success
                });
            }
        }
    }
}
