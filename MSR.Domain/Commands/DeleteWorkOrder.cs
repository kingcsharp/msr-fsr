using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteWorkOrder : Command
    {
        public int Id { get; private set; }

        public DeleteWorkOrder(int id)
        {
            Id = id;
        }
    }
}
