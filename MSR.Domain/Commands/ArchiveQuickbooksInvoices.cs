using System.Collections.Generic;
using MSR.Domain.Commanding;
using MSR.Domain.Models;

namespace MSR.Domain.Commands
{
    public class ArchiveQuickbooksInvoices : Command
    {
        public IEnumerable<QuickbooksFormatterModel> FormattedInvoices { get; set; }
        public string FormatType { get; set; }
    }
}
