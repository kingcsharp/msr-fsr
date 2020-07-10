using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class UpdateProcedure : Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ProcedureTypeId { get; set; }
        public int? Revision { get; set; }
        public double? Duration { get; set; }
        public string DurationType { get; set; }
    }
}
