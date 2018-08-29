using Msr.Models.TheoryParagraphs;
using Msr.Services.jqGrid;
using Msr.Services.TheoryParagraph;
using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;

namespace Answer.Web.Controllers
{
    [AuthorizeUser]
    public class TheoryParagraphController : BaseController
    {
        // GET: TheoryParagraph
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetTheoryParagraphs(string callBackId)
        {
            ViewBag.CallBackId = callBackId;

            return PartialView("_TheoryParagraph");
        }
        public ActionResult TheoryParagraphsData(JqGridParam param)
        {
            var theoryParagraphService = new TheoryParagraphService();

            var totalRows = theoryParagraphService.GetTheoryApprovedQueryable(GetCurrentUser().Id);


            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(TheoryParagraphView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(TheoryParagraphView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(TheoryParagraphView.Status))
                    {
                        totalRows = totalRows.Where(x => x.Status.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }
            var orderBy = nameof(TheoryParagraphView.Root);
            if (!string.IsNullOrWhiteSpace(param.sortColumn))
            {
                orderBy = param.sortColumn;
            }
            if (param.sortOrder == "desc")
            {
                totalRows = totalRows.OrderByDescending(orderBy);
            }
            else
            {
                totalRows = totalRows.OrderBy(orderBy);
            }
            var totalRecords = totalRows.Count();
            totalRows = totalRows.Skip(param.pageSize * (param.pageIndex - 1));
            totalRows = totalRows.Take(param.pageSize);
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);
            var results = totalRows.ToList();
            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}