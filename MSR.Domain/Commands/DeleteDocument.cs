using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class DeleteDocument : Command
    {
        public int Id { get; set; }
    }
}
