using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class UpdateWorkOrder : CreateWorkOrder
    {
        public int Id { get; set; }
    }
}
