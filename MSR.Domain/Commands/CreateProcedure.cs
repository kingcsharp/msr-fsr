using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class CreateProcedure : Command
    {
        public string Name { get; set; }
        public int? ProcedureTypeId { get; set; }
        public int? Revision { get; set; }
        public double? Duration { get; set; }
        public string DurationType { get; set; }
    }
}
