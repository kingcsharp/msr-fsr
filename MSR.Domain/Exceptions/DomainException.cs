using MSR.Domain.Commanding.Emums;
using System;

namespace MSR.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message, DomainError errorType = DomainError.Unknown)
            :base(message)
        {
            ErrorType = errorType;
        }

        public DomainError ErrorType { get; set; }
    }
}
