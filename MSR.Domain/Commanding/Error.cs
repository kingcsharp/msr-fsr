using System;

namespace MSR.Domain.Commanding
{
    public class Error
    {
        public Exception Exception { get; set; }
        public string Message { get; set; }
    }
}
