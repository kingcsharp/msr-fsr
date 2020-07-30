using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeletePart : Command
    {
        public int Id { get; set; }
    }
}
