using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Answer.Web.Controllers;
using Answer.Web.Filters;
using Hangfire.Annotations;
using Msr.Models.Archive;
using Msr.Models.Reporting;
using Msr.Services.S3;
using RestSharp;
using HttpCookie = System.Web.HttpCookie;

namespace Msr.Web.Controllers
{
    [AuthorizeUser]
    public class ReportController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.ActiveClass = "WIP";
            return View();
        }

        public ActionResult FinancialDashboard()
        {
            ViewBag.ActiveClass = "WIP";
            ViewBag.CombinedURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/CombinedFinancialData";
            ViewBag.WONoInvoiceURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/WorkOrdersWithoutInvoices";

            return View();
        }

        public ActionResult OperationsDashboard()
        {
            ViewBag.ActiveClass = "WIP";
            ViewBag.RevenueByCustomerURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/CustomerRevenueData";
            ViewBag.RevenueByKitURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/KitRevenueData";
            ViewBag.CountofKitsURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/KitCountsData";
            return View();
        }
        public ActionResult AdHocReports()
        {
            //list of reports
            ViewBag.ReportDataURL = ConfigurationManager.AppSettings["WebsiteUrl"].ToString() + "api/Reporting/AdHocReport";
            return View(new List<AdHocReportItem>() {
                new AdHocReportItem() { Title = "Actual Parts History by Part Number" },
                new AdHocReportItem() { Title = "Actual Parts History by Serial Number" },
                new AdHocReportItem() { Title = "Serial Number History" },
                new AdHocReportItem() { Title = "Work In Process" },
            });
        }

    }
}