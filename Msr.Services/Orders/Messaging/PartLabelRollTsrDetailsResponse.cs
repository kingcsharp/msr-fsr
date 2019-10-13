using System.Collections.Generic;
using Msr.Services.Orders.Procedures;

namespace Msr.Services.Orders.Messaging
{

    public class PartLabelRollTsrDetailsResponse
    {

        public PartLabelRollTsrDetailsResponse()
        {

            GetPartsAndKitsLabelsResult = new List<GetPartsAndKitsLabelsResult>();

        }

        public int FillId { get; set; }

        public List<GetPartsAndKitsLabelsResult> GetPartsAndKitsLabelsResult { get; set; }

        public int Count { get; set; }

    }

}
