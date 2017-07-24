using Msr.Models.Procedures;
using Msr.Services.jqGrid;
using Msr.Services.Procedures;
using Msr.Web.ViewModel.Engineering;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Answer.Web.Controllers
{
    public class ProceduresController : Controller
    {
        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Procedures";

            return View(viewModel);
        }

        public ActionResult ProceduresData(JqGridParam param)
        {
            var procedureService = new ProcedureService();

            var totalRows = procedureService.GetProceduresQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ProcedureView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ProcedureView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureView.VerbTypeName))
                    {
                        totalRows = totalRows.Where(x => x.VerbTypeName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProcedureView.Revision))
                    {
                        var rev = Convert.ToInt32(rule.data);
                        totalRows = totalRows.Where(x => x.Revision == rev);
                    }
                    else if (rule.field == nameof(ProcedureView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());

                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                }
            }

            var orderBy = nameof(ProcedureView.Id);
            var orderDirection = "asc";

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
        
        ////public ActionResult Create()
        ////{
        ////    var saveProcedureViewModel = new SaveProcedureViewModel();

        ////    //need to be dynamic
        ////    var NTLogin = "1618";
        ////    saveProcedureViewModel.Setup(new ProcedureService(), NTLogin);

        ////    return View(saveProcedureViewModel);
        ////}
        ////[AcceptVerbs(HttpVerbs.Post)]
        ////public ActionResult Create(SaveProcedureViewModel model)
        ////{
        ////    var procedureService = new ProcedureService();

        ////    //need to be dynamic
        ////    var NTLogin = "1618";

        ////    if (ModelState.IsValid)
        ////    {
        ////        var response = procedureService.Save(model);
        ////        if (response)
        ////        {
        ////            TempData["SuccessMessage"] = "Verb Type has been created successfully.";

        ////            return RedirectToAction("Index");
        ////        }
        ////        else
        ////        {
        ////            TempData["ErrorMessage"] = "Something went wrong.";

        ////            model.Setup(new ProcedureService(), NTLogin);

        ////            return View(model);
        ////        }
        ////    }


        ////    model.Setup(new ProcedureService(), NTLogin);

        ////    return View(model);
        ////}
        ////public ActionResult Edit(string Id)
        ////{
        ////    var saveProcedureViewModel = new SaveProcedureViewModel();
        ////    var procedureService = new ProcedureService();

        ////    var model = procedureService.GetVerbTypeById(Id: Id);

        ////    //need to be dynamic
        ////    var NTLogin = "1618";
        ////    saveProcedureViewModel = saveProcedureViewModel.MapToDto(model);
        ////    saveProcedureViewModel.Setup(new ProcedureService(), NTLogin);

        ////    return View(saveProcedureViewModel);
        ////}
        ////[AcceptVerbs(HttpVerbs.Post)]
        ////public ActionResult Edit(SaveProcedureViewModel model)
        ////{
        ////    var procedureService = new ProcedureService();

        ////    //need to be dynamic
        ////    var NTLogin = "1618";

        ////    if (ModelState.IsValid)
        ////    {
        ////        var response = procedureService.Edit(model);
        ////        if (response)
        ////        {
        ////            TempData["SuccessMessage"] = "Verb Type has been created successfully.";

        ////            return RedirectToAction("Index");
        ////        }
        ////        else
        ////        {
        ////            TempData["ErrorMessage"] = "Something went wrong.";

        ////            model.Setup(new ProcedureService(), NTLogin);

        ////            return View(model);
        ////        }
        ////    }


        ////    model.Setup(new ProcedureService(), NTLogin);

        ////    return View(model);
        ////}
    }
}