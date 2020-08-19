using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetTrainingCertification: Command
    {
        public int? Id { get; set; }
    }
}
