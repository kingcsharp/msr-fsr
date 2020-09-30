using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteRole: Command
    {
        public int Id { get; set; }
    }
}
