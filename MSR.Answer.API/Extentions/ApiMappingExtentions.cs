using MSR.Answer.API.V1.Models;
using MSR.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Answer.API.Extentions
{
    public static class ApiMappingExtentions
    {
        public static SystemLogin ToSystemLoginCommand(this SystemLoginRequest request)
        {
            return new SystemLogin();
        }
    }
}
