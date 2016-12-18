using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Msr.Models.Tasks;
using Msr.Web.ViewModel.Reports;

namespace Msr.Web.ViewModel
{
    public class PartsReportsViewModel
    {
        public PartsReportsViewModel()
        {
            ReportByItems = GetReportByList();
            DateRangeItems = GetDateRangeItems();
            Monitors = GetMonitors();
            Categories = new List<string>();
            DataSets = new List<ReportItemData>();
            MonitorsWithTaskAndResults = new List<MonitorsWithTaskAndResult>();
            ReportTypes = GetReportType();
            PartNumbers = new List<SelectListItem>();
            Serials = new List<SelectListItem>();
        }

        public List<SelectListItem> ReportByItems { get; set; }
        public int SearchById { get; set; }
        public string Number { get; set; }
        public string Serial { get; set; }
        public int DateRangeId { get; set; }
        public string MonitorId { get; set; }
        public int ReportTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List<SelectListItem> DateRangeItems { get; set; }
        public List<SelectListItem> Monitors { get; set; }
        public List<SelectListItem> ReportTypes { get; set; }
        public List<SelectListItem> Serials { get; set; }
        public List<SelectListItem> PartNumbers { get; set; }
        public List<MonitorsWithTaskAndResult> MonitorsWithTaskAndResults { get; set; }
        public List<ReportItemData> DataSets { get; set; }
        public string DataSetsJson { get; set; }
        public List<string> Categories { get; set; }
        public string Ytext { get; set; }


        private List<SelectListItem> GetMonitors()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = MonitorTypeConstants.Densitometer, Value = ReportTypeConstants.Graph.ToString()},
                new SelectListItem {Text = MonitorTypeConstants.PartsUsed , Value = ReportTypeConstants.Report.ToString()}
            };
        }

        private List<SelectListItem> GetReportByList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "-Select-", Value = ""},
                new SelectListItem {Text = "Part Number", Value = "1"},
                new SelectListItem {Text = "Serial Number" , Value = "2"}
            };
        }

        private List<SelectListItem> GetReportType()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Report", Value = "1"},
                new SelectListItem {Text = "Graph" , Value = "2"}
            };
        }

        private List<SelectListItem> GetDateRangeItems()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text = "Last Year", Value = "1"},
                new SelectListItem {Text = "Last Six Months", Value = "2"},
                new SelectListItem {Text = "Last Three Months" , Value = "3"},
                new SelectListItem {Text = "Last Thirty Days" , Value = "4"}
            };
        }

        public void SetDate()
        {
            if (DateRangeId == 1)
            {
                FromDate = DateTime.Now.AddMonths(-12).Date;
                ToDate = DateTime.Now;
            }
            else if (DateRangeId == 2)
            {
                FromDate = DateTime.Now.AddMonths(-6).Date;
                ToDate = DateTime.Now;
            }

            else if (DateRangeId == 3)
            {
                FromDate = DateTime.Now.AddMonths(-3).Date;
                ToDate = DateTime.Now;
            }

            else if (DateRangeId == 3)
            {
                FromDate = DateTime.Now.AddDays(-30).Date;
                ToDate = DateTime.Now;
            }
        }
    }
}