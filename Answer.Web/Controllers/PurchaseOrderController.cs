using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Infrastructure.Common.Constansts;
using Msr.Models.Menus;
using Msr.Models.Orders;
using Msr.Services.jqGrid;
using Msr.Services.PurchesOrder;
using Msr.Models.PurchesOrder;
using Msr.Services.PurchesOrder.ViewModels;
using Msr.Services.ProductionPlanning;
using Msr.Services.Workflows;
using System.Web.Helpers;
using Msr.Services.Orders;
using Syncfusion.EJ2.Linq;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Quotes)]
    public class PurchaseOrderController : BaseController
    {
        private readonly PurchesOrderService _purchesOrderService;
        private readonly ProductionPlanningService _productionPlanningService;
        private readonly OrderService _orderService;

        public PurchaseOrderController()
        {
            _purchesOrderService = new PurchesOrderService();
            _productionPlanningService = new ProductionPlanningService();
            _orderService = new OrderService();
        }

        public ActionResult Index()
        {
            ViewBag.ActiveClass = "PurchaseOrder";
            ViewBag.Company = GetCurrentUser().Root_Company;

            return View();
        }

        public ActionResult PurchaseOrderData(JqGridParam param)
        {
            var totalRows = _purchesOrderService.GetPurchesOrderQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PurchesOrderView.Name))
                    {
                        totalRows = totalRows.Where(x => x.Name.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PurchesOrderView.Root))
                    {
                        totalRows = totalRows.Where(x => x.Root.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PurchesOrderView.ReferencePo))
                    {
                        totalRows = totalRows.Where(x => x.ReferencePo.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PurchesOrderView.ReferenceName))
                    {
                        totalRows = totalRows.Where(x => x.ReferenceName.ToLower().Contains(rule.data.ToLower()));
                    }

                    else if (rule.field == nameof(PurchesOrderView.InvoicedBalance))
                    {
                        decimal value;
                        if (decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.InvoicedBalance == value);
                        }
                    }
                    else if (rule.field == nameof(PurchesOrderView.UninvoicedBalance))
                    {
                        decimal value;
                        if (decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.UninvoicedBalance == value);
                        }
                    }

                    else if (rule.field == nameof(PurchesOrderView.Balance))
                    {
                        decimal value;
                        if (decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Balance == value);
                        }
                        else
                        {
                            totalRows = totalRows.Where(x => x.Balance == -1);
                        }
                    }

                    else if (rule.field == nameof(PurchesOrderView.SupplierName))
                    {
                        totalRows = totalRows.Where(x => x.SupplierName.ToLower().Contains(rule.data.ToLower()));
                    }

                    else if (rule.field == nameof(PurchesOrderView.CustomerName))
                    {
                        totalRows = totalRows.Where(x => x.CustomerName.ToLower().Contains(rule.data.ToLower()));
                    }

                    else if (rule.field == nameof(PurchesOrderView.OpenDate))
                    {
                        totalRows = totalRows.Where(x => x.OpenDate.ToString().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PurchesOrderView.CloseDate))
                    {
                        totalRows = totalRows.Where(x => x.CloseDate.ToString().Contains(rule.data.ToLower()));
                    }

                    else if (rule.field == nameof(PurchesOrderView.TotalPurchaseLimit))
                    {
                        decimal value;
                        if (decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.TotalPurchaseLimit == value);
                        }
                        else
                        {
                            totalRows = totalRows.Where(x => x.TotalPurchaseLimit == -1);
                        }
                    }
                    else if (rule.field == nameof(PurchesOrderView.AccType) && rule.data != "")
                    {
                        var list = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToArray();
                        if (list.Any())
                        {
                            totalRows = totalRows.Where(x => list.Contains(x.AccType.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(PurchesOrderView.InvoiceTrigger))
                    {
                        totalRows = totalRows.Where(x => x.InvoiceTrigger.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PurchesOrderView.UnusedAmount))
                    {
                        decimal value;

                        if (decimal.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.UnusedAmount == value);
                        }
                    }
                    else if (rule.field == nameof(PurchesOrderView.Rev))
                    {
                        int value;
                        if (int.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Rev == value);
                        }
                    }

                    else if (rule.field == nameof(PurchesOrderView.Status) && rule.data != "")
                    {
                        var list = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToArray();
                        if (list.Any())
                        {
                            totalRows = totalRows.Where(x => list.Contains(x.Status.ToLower()));
                        }
                    }

                }
            }
            var orderBy = nameof(PurchesOrderView.CreateDate);


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


            totalRows.ForEach(purchaseOrderViewModel =>
                {
                    purchaseOrderViewModel.HasWorkOrders = _orderService.GetWorkOrderQueryable()
                        .Where(s => s.ReferencePo == purchaseOrderViewModel.ReferencePo).ToList().Any();
                });


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

        [HttpGet]
        public ActionResult Create()
        {
            var currentUser = GetCurrentUser();

            var vm = new NewPurchaseOrderViewModel();

            vm.Setup(_purchesOrderService, _productionPlanningService, currentUser);

            return View(vm);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var currentUser = GetCurrentUser();

            var purchaseOrder = new NewPurchaseOrderViewModel();

            var model = _purchesOrderService.GetById(id);

            purchaseOrder = purchaseOrder.MaptoDto(model);

            purchaseOrder.Setup(_purchesOrderService, _productionPlanningService, currentUser);

            return View(purchaseOrder);
        }

        [HttpPost]
        public ActionResult Create(NewPurchaseOrderViewModel model)
        {
            var currentUser = GetCurrentUser();

            // LISTBOX IS DISABLED AND SET TO READ-ONLY - VALUE IS NOT POSTED OR SET ON THE MODEL
            model.SupplierDepartment = "2";

            if (ModelState.IsValid)
            {
                model.NTLogin = currentUser.Id;
                model.SupplierDepartment = "2";

                var response = _purchesOrderService.Create(model);

                if (!response.HasErrors())
                {
                    TempData[NotificationConstants.SuccessMessage] = response.SuccessMessage;

                    if (model.Save == "true")
                    {
                        return RedirectToAction("Edit", new { id = response.Entity.NewId });
                    }

                    if (model.SaveClose == "true")
                    {
                        return RedirectToAction("Index");
                    }

                    if (model.SaveWorkflow == "true")
                    {
                        var returnUrl = Url.Action("Index", "PurchaseOrder");

                        return RedirectToAction("Submit", "Workflow", new { objId = response.Entity.NewId, returnUrl });
                    }
                }

                TempData[NotificationConstants.ErrrorMessage] = response.ErrorMessage;
            }

            model.Setup(_purchesOrderService, _productionPlanningService, currentUser);

            return View(model);
        }
        public JsonResult GetCurresntUser()
        {
            var currentUser = GetCurrentUser();
            return Json(currentUser.Root_Company, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductsList(string id, string supplierCo)
        {
            var products = _purchesOrderService
                .GetCompanyProducts(id,supplierCo)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.OrderId
                }).OrderBy(o => o.Text).ToList();

            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PurchasePoDetails(string id)
        {
            var purchaseOrder = _purchesOrderService.AccountPurchaseOrderById(id);

            if (purchaseOrder == null)
            {
                purchaseOrder = new PurchaseFormAccountViewModel();
            }

            purchaseOrder.Setup(_purchesOrderService);

            return View(purchaseOrder);
        }

        [HttpPost]
        public ActionResult PurchasePoDetails(PurchaseFormAccountViewModel model)
        {
            var response = _purchesOrderService.PurchasedOrderUpdateAndShowOrderItemList(model, GetCurrentUser().Id);

            if (!response.HasErrors())
            {
                return RedirectToAction("CreatePurchase", "PurchaseOrder",
                    new { id = response.Entity, oldId = model.OBJECT_ID, refrencePo = model.REFERENCE_PO, model.Root });
            }
            TempData["ErrorMessage"] = response.ErrorMessage;

            return RedirectToAction("PurchasePoDetails", new { id = model.OBJECT_ID });
        }

        [HttpGet]
        public ActionResult CreatePurchase(string id, string oldId)
        {
            var currentUser = GetCurrentUser();

            var model = _purchesOrderService.PurchasedOrderById(id);
            model.OrderItems = _purchesOrderService.PurchasedOrderOrderItems(model.ID);

            model.oldId = oldId;
            model.newId = model.ID;

            model.Setup(_purchesOrderService, currentUser);

            return View(model);
        }

        [HttpPost]
        public ActionResult CreatePurchase(PurchasePoModel model, string id, string submitType)
        {
            var currentUser = GetCurrentUser();

            var data = _purchesOrderService.CreatePurchaseSaveUpdate(model, currentUser.Id);

            if (!data.HasErrors())
            {
                if (submitType == "SubmitWorkflow")
                {
                    _purchesOrderService.PurchaseOrderWorkFlow(model.OBJECT_ID, currentUser.Id);

                    var url = Url.Action("PurchaseOrderMultifill", "PurchaseOrder", new { id = model.OBJECT_ID });

                    return RedirectToAction("Submit", "Workflow", new { objId = model.OBJECT_ID, returnUrl = url, showCancel = true });


                }
                var modelpo = _purchesOrderService.PurchasedOrderById(model.ID);
                modelpo.Setup(_purchesOrderService, currentUser);
            }

            return RedirectToAction("CreatePurchase", "PurchaseOrder", new { id = model.OBJECT_ID, oldId = model.oldId });
        }

        [HttpPost]
        public ActionResult AddPurchaseOrderItem(string id, string orderid)
        {
            var modelpo = _purchesOrderService.AddPurchasedOrderItem(id, orderid, GetCurrentUser().Id);
            return Json(new { data = "OK" }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult PurchaseOrderMultifill(string id)
        {
            var currentUser = GetCurrentUser();

            var purchaseOrderMultiFillViewModel = new PurchaseOrderMultiFillViewModel();

            var OwnerList = _purchesOrderService.PurchasedOrderOwnerList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = x.Id == "2" // DEFAULT TO MSR-FSR
            }).OrderBy(o => o.Text).ToList();

            var locList = _purchesOrderService.PurchasedOrderLocationList()
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).OrderBy(o => o.Text).ToList();
            locList.Insert(0, new SelectListItem { Value = "", Text = "Select" });

            var purchaseApprovedData = _purchesOrderService.PurchasedApprovedDataByObjId(id);

            if (purchaseApprovedData != null)
            {
                purchaseOrderMultiFillViewModel.PurchaseApprovedData = purchaseApprovedData;

                var purchaseOrderFillViewModel = _purchesOrderService.PurchasedOrderSearchTasks(purchaseApprovedData.HISTORY_REF_ID, currentUser.Id);

                purchaseOrderMultiFillViewModel.SetList(purchaseOrderFillViewModel, locList, OwnerList);
            }
            
            return View(purchaseOrderMultiFillViewModel);
        }

        [HttpPost]
        public ActionResult PurchaseOrderMultifill(PurchaseOrderMultiFillViewModel model)
        {
            if (model.FillList.Count > 0)
            {
                var resp = _purchesOrderService.SavePurchaseMultiFill(model, GetCurrentUser().Id);
                if (resp.HasErrors()) {
                    TempData["ErrorMessage"] = resp.ErrorMessage;
                }

                return RedirectToAction("PurchaseOrderMultifillPage", new { id = model.PurchaseApprovedData.OBJECT_ID });
            }
            return RedirectToAction("PurchaseOrderMultifill", new { id = model.PurchaseApprovedData.OBJECT_ID });

        }

        [HttpGet]
        public ActionResult PurchaseOrderMultifillPage(string id)
        {
            var currentUser = GetCurrentUser();

            var purchaseApprovedData = _purchesOrderService.PurchasedApprovedDataByObjId(id);

            var workflowService = new WorkflowService();

            workflowService.SpRunAdminSql();

            return View(purchaseApprovedData);
        }

        public ActionResult Purchases()
        {
            ViewBag.ActiveClass = "Purchases";

            return View();
        }

        public ActionResult PurchaseViewData(JqGridParam param)
        {
            var totalRows = _purchesOrderService.GetPurchaseViewQueryable().Where(x => x.Status != "DELETED");

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {

                    if (rule.field == nameof(PurchaseView.ObjectId))
                    {
                        totalRows = totalRows.Where(x => x.ObjectId.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PurchaseView.CustPurchNum))
                    {
                        totalRows = totalRows.Where(x => x.CustPurchNum.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PurchaseView.Description))
                    {
                        totalRows = totalRows.Where(x => x.Description.ToLower().Contains(rule.data.ToLower()));
                    }

                    else if (rule.field == nameof(PurchaseView.PurchaseStatus) && rule.data != "")
                    {
                        var list = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToArray();
                        if (list.Any())
                        {
                            totalRows = totalRows.Where(x => list.Contains(x.PurchaseStatus.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(PurchaseView.DateCreated))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.DateCreated == value);
                        }
                    }
                    else if (rule.field == nameof(PurchaseView.DateCreated))
                    {
                        DateTime value;
                        if (DateTime.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.DateCreated.Day == value.Day &&
                                                             x.DateCreated.Month == value.Month &&
                                                             x.DateCreated.Year == value.Year);
                        }
                    }
                    else if (rule.field == nameof(PurchaseView.Status) && rule.data != "")
                    {
                        var list = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToArray();
                        if (list.Any())
                        {
                            totalRows = totalRows.Where(x => list.Contains(x.Status.ToLower()));
                        }
                    }
                }
            }
            var orderBy = nameof(PurchaseView.Id);

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

        public ActionResult CloseAccount(string id)
        {
            var currentUser = GetCurrentUser();

            _purchesOrderService.CloseAccount(id, currentUser.Id);

            return RedirectToAction("Index");
        }
    }
}

