using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msr.Services.jqGrid;
using Msr.Services.Procedures;
using Msr.Services.Procedures.ViewModels;

namespace Answer.Web.Controllers
{
    public class ProcedureMonitorsController : BaseController
    {
        // GET: ProcedureMonitors
        public ActionResult Monitors(string objectId)
        {

            return View();
        }
        public ActionResult ProcedureMonitorsData(JqGridParam param, string objectId)
        {
            var procedureService = new ProceduresService();

            var model = new ProcedureMonitorsViewModel();

            // model.Related_Object_Id = objectId; //need to uncomment after testing

            //temporary to test Data, should remove after testing

            model.Related_Object_Id = "89407";
            model.Procedure_Step_Id = "89409";

            model.StrNTLogin = GetCurrentUser().Id;

            var totalRows = procedureService.GetMonitors(model);


            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ProcedureMonitorsViewModel.Description))
                    {
                        totalRows = totalRows.Where(x => x.Description == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ProcedureMonitorsViewModel.Monitor_Type))
                    {
                        totalRows = totalRows.Where(x => x.Monitor_Type.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureMonitorsViewModel.Should_Be))
                    {
                        totalRows = totalRows.Where(x => x.Should_Be.ToLower().Contains(rule.data.ToLower()));
                    }

                }
            }

            var orderBy = nameof(ProcedureMonitorsViewModel.Description);

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