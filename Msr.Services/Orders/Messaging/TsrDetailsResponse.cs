using System.Collections.Generic;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class TsrDetailsResponse
    {
        public List<TsrTaskResult> TsrTaskResults { get; set; }

        public TsrDetailsResponse()
        {
           TsrTaskResults = new List<TsrTaskResult>();
        }
    }
}
