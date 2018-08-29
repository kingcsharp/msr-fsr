using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Models.Menus;
using Msr.Models.PartTypes;
using Msr.Services.jqGrid;
using Msr.Services.PartTypes;
using Msr.Services.PartTypes.ViewModels;
using Msr.Services.Workflows;
using Msr.Web.ViewModel.Engineering;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Parts)]
    public class PartTypesController : BaseController
    {
        private PartTypeService _partTypeService;

        public PartTypesController()
        {
            _partTypeService = new PartTypeService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Parts";

            return View(viewModel);
        }

        public ActionResult PartTypesData(JqGridParam param)
        {

            var totalRows = _partTypeService.GetPartTypesQueryable();

            var defaultStatusList = "CREATING,DENIED,APPROVED,APPROVED_BUT_REVISING,APPROVED_BUT_DELETING".Split(',');

            totalRows = totalRows.Where(x => defaultStatusList.Contains(x.Status));

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PartTypesView.Root))
                    {
                        totalRows = totalRows.Where(x => x.Root == rule.data.ToLower());
                    }
                    if (rule.field == nameof(PartTypesView.Id))
                    {
                        totalRows = totalRows.Where(x => x.Id == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(PartTypesView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartTypesView.Spare))
                    {
                        totalRows = totalRows.Where(x => x.Spare.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartTypesView.Consumable))
                    {
                        totalRows = totalRows.Where(x => x.Consumable.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartTypesView.Unit))
                    {
                        totalRows = totalRows.Where(x => x.Unit.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PartTypesView.Rev))
                    {
                        int value;
                        if (Int32.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }

                    }
                    else if (rule.field == nameof(PartTypesView.Status))
                    {
                        var statusList = rule.data.Split(',').Select(x => x.Trim().ToLower());
                        if (statusList.Any())
                        {
                            totalRows = totalRows.Where(x => statusList.Contains(x.Status.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(PartTypesView.LockedByName))
                    {
                        totalRows = totalRows.Where(x => x.LockedByName.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(PartTypesView.Id);

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
        public ActionResult Create()
        {
            var addModel = new AddPartTypesViewModel();

            addModel.Setup();

            return View(addModel);
        }
        [AcceptVerbs(verbs: HttpVerbs.Post)]
        public ActionResult Create(AddPartTypesViewModel parttype)
        {
            if (ModelState.IsValid)
            {
                //need to de dynamic
                parttype.NTLogin = GetCurrentUser().Id;

                var response = _partTypeService.Create(parttype);

                if (response)
                {
                    TempData["SuccessMessage"] = "PartType has been created successfully.";


                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    parttype.Setup();

                    return View(parttype);
                }
            }

            parttype.Setup();

            return View(parttype);
        }
        public ActionResult Edit(string id)
        {
            var currrentUser = GetCurrentUser();
            var workflowService = new WorkflowService();
            var checkoutEntity = workflowService.CheckOutObject(id, currrentUser.Id);

            var model = _partTypeService.GetById(checkoutEntity.Entity);

            var parttype = new AddPartTypesViewModel();
            parttype.MapToDto(model);

            parttype.Setup();

            return View(parttype);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(AddPartTypesViewModel parttype)
        {
            if (ModelState.IsValid)
            {
                //need to de dynamic
                parttype.NTLogin = GetCurrentUser().Id;

                var response = _partTypeService.Edit(parttype);
                if (response)
                {
                    TempData["SuccessMessage"] = "PartType has been edited successfully.";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Something went wrong.";

                    parttype.Setup();

                    return View(parttype);
                }
            }

            parttype.Setup();

            return View(parttype);
        }


        public ActionResult Delete(string id, string ntlogin)
        {
            var response = _partTypeService.Delete(id: id, ntlogin: ntlogin);

            if (response)
            {
                TempData["SuccessMessage"] = "Part Type deleted successfully.";

                return RedirectToAction("Index");
            }

            TempData["ErrorMessage"] = "Something went wrong.";
            return RedirectToAction("Index");
        }
    }
}