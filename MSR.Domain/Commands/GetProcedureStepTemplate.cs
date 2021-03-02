using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetProcedureStepTemplate : PagingCommand
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
