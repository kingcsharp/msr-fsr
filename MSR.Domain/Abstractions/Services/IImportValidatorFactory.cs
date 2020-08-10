using MSR.Domain.Commanding.Enums;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Abstractions.Services
{
    public interface IImportValidatorFactory
    {
        IValidateImportData Create(EnumMenuItem menuItem);
    }
}
