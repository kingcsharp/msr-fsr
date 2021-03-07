using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Models
{
    public class GetProcedureTypeRequest: BaseApiModel
    {
        public int? Id { get;set;}
        public string Name { get;set;}
    }
}
