using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System.Collections.Generic;

namespace MSR.Domain.Commands
{
    /// <summary>
    /// GetWorkOrderViewRequest
    /// </summary>
    public class GetWorkOrderView: Command
    {
        /// <summary>
        /// If true, return only NOT in progress or waiting
        /// </summary>
        public bool IsHistory { get; set; }
    }
}
