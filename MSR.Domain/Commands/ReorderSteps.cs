using MSR.Domain.Commanding;
using MSR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Commands
{
    public class ReorderSteps: Command
    {
        public int ProcedureId { get; }
        public List<ReorderStep> ProcedureSteps { get; }

        public ReorderSteps(int procedureId, List<ReorderStep> procedureSteps)
        {
            ProcedureId = procedureId;
            ProcedureSteps = procedureSteps;
        }
    }
}
