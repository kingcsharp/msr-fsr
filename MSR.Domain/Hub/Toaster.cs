using System;
using MSR.Domain.Commanding.Enums;

namespace MSR.Domain.Hub
{
    public class Toaster
    {
        public Guid MessageId => Guid.NewGuid();
        public string Message { get; set; }
        public EnumToasterStatus Status { get; set; }
    }
}
