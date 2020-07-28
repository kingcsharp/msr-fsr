using MSR.Domain.Commanding;

namespace MSR.Domain.Commands
{
    public class GetParts : Command
    {
        public int? partID;

        // If true, this will query from the storage facility (e.g. AWS)
        // the access URL for each file in the part.  This makes the
        // query take much longer.
        public bool attachFiles = false;
    }
}
