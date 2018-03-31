using System.Collections.Generic;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class TsrDetailsResponse
    {
        public TsrDetailsResponse()
        {
            TsrTaskResults = new List<TsrTaskResult>();
        }

        public int FillId { get; set; }

        public List<TsrTaskResult> TsrTaskResults { get; set; }

        public WorkOrderDetailsResponse WorkOrderDetailsResponse { get; set; }
    }
}
