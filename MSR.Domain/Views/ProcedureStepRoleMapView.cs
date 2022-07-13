using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Views
{
    public class ProcedureStepRoleMapView
    {
        public int Id { get; set; }
        public int ProcedureStepId { get; set; }
        public int RoleId { get; set; }
    }
}
