using MSR.Domain.Commanding;
using MSR.Domain.Views;

namespace MSR.Domain.Commands
{
    public class TransmitIntelXmlDataByWorkOrder : Command
    {
        public IntelXmlData Data { get; set; }
    }
}

