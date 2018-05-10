using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Msr.Services.Documents.ViewModels;

namespace Msr.Services.Orders.Procedures
{
    public class GetReferenceTheories
    {
        public string TheoryId { get; set; }
        public string TheoryName { get; set; }
        public string ObjectId { get; set; }
        public List<DocLink> DocLinks { get; set; }
    }
}
