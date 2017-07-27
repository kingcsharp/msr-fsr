using System;

namespace Msr.Services.Orders.Procedures
{
    public class TheoryGetRevisionDataResult
    {
        public string Status { get; set; }
        public string Creator_Name { get; set; }
        public string Rev { get; set; }
        public string Wf_Name { get; set; }
        public string Rev_Info { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime Approval_Date { get; set; }
    }
}
