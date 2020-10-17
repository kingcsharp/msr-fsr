
namespace MSR.Domain.Commands
{
    public class UpdateWorkOrderTask : CreateWorkOrderTask
    {
        public int Id { get; set; }
        public string Status { get; set; }
    }
}
