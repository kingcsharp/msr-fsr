using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetProcedureType : PagingCommand
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
