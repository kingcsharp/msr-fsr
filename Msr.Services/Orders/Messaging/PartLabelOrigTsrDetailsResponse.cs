using System.Collections.Generic;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{
    public class PartLabelOrigTsrDetailsResponse
    {
        public PartLabelOrigTsrDetailsResponse()
        {
            GetPartsAndKitsLabelsResult = new List<GetPartsAndKitsLabelsResult>();
        }

        public int FillId { get; set; }

        public List<GetPartsAndKitsLabelsResult> GetPartsAndKitsLabelsResult { get; set; }

        public int Count { get; set; }
    }
}
