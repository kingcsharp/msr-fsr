using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Events;
using MSR.Domain.SQSEventing.Abstractions;
using System.Threading;
using System.Threading.Tasks;

namespace MSR.Application.EventServices
{
    public class EventServiceHandler : IEventHandler<ImportEvent>
    {
        ICustomerService _customerService;
        ILocationService _locationService;

        public EventServiceHandler(ICustomerService customerService, ILocationService locationService)
        {
            _customerService = customerService;
            _locationService = locationService;
        }

        public async Task HandleAsync(ImportEvent handledEvent, CancellationToken cancellationToken = default)
        {

            switch(handledEvent.MenuItem)
            {
                case EnumMenuItem.CustomersDepartments:
                    var importedCustomers = await _customerService.ImportCustomers(handledEvent.CsvData);
                    break;
                case EnumMenuItem.Locations:
                    var importedLocations = await _locationService.ImportLocations(handledEvent.CsvData);
                    break;
            }

            //TODO: ADD IN SIGNALR STUFF HERE
            
        }
    }
}
