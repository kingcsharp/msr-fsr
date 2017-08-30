using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Msr.Models.Procedures
{
    public class ProcedureStepOtherStepListView
    {
        public string Id { get; set; }
        public string Procedure_Id { get; set; }
        public string Step_Text { get; set; }
        public int? Start_On_counter { get; set; }
        public int? counter_value { get; set; }
        public string counter_Unit { get; set; }
        public string Rel_Or_Abs { get; set; }
        public string From_Start_or_stop { get; set; }
        public string System_task { get; set; }
        public string destination { get; set; }
        public string special_Location { get; set; }
        public string reference_verb { get; set; }
        public string reference_object { get; set; }
        public string comments { get; set; }
        public int? Goto_step { get; set; }
        public string Goto_step_id { get; set; }
        public string cycles { get; set; }
        public int? cycle_on_counter { get; set; }
        public int? cycle_count { get; set; }
        public string cycle_Unit { get; set; }
        public DateTime? Drcm { get; set; }
        public string modby { get; set; }
        public double? print_Order { get; set; }
        public string Old_procedure_id { get; set; }
        public string Old_step_Id { get; set; }
        public double? Duration { get; set; }
        public string Duration_type { get; set; }
        public DateTime? date_last_modified { get; set; }
    }
}
