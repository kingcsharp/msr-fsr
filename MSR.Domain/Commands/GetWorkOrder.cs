using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetWorkOrder : Command
    {
        public int? Id { get; set; }
    }
}
