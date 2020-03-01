using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Models.Delivery;
using Msr.Models.Menus;
using Msr.Services.jqGrid;
using Msr.Services.Delivery;
using Msr.Web.ViewModel.Engineering;

namespace Answer.Web.Controllers
{
    [AuthorizeUser]
    public class DeliveryController : BaseController
    {

        private readonly DeliveryService _deliveryService;

        public DeliveryController()
        {
            _deliveryService = new DeliveryService();
        }

        public ActionResult Index()
        {
            var viewModel = new EngineeringViewModel();

            ViewBag.ActiveClass = "Delivery";

            return View(viewModel);
        }

        public ActionResult DeliveryScreenData(JqGridParam param)
        {
            var totalRows = _deliveryService.GetDeliveryScreenDataToView(GetCurrentUser().Id);

            totalRows = totalRows.Where(x => x.PRODUCT_NAME.Length > 0);

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(DeliveryScreenView.TASK_ID))
                    {
                        totalRows = totalRows.Where(x => x.TASK_ID == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(DeliveryScreenView.CUST_NAME))
                    {
                        totalRows = totalRows.Where(x => x.CUST_NAME.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(DeliveryScreenView.CUST_PURCH_NUM))
                    {
                        totalRows = totalRows.Where(x => x.CUST_PURCH_NUM.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(DeliveryScreenView.COMPANY_PART_NUMBER))
                    {
                        totalRows = totalRows.Where(x => x.COMPANY_PART_NUMBER.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(DeliveryScreenView.CUST_NAME))
                    {
                        totalRows = totalRows.Where(x => x.CUST_NAME.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(DeliveryScreenView.PRODUCT_NAME))
                    {
                        totalRows = totalRows.Where(x => x.PRODUCT_NAME.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(DeliveryScreenView.PROCEDURE_NAME))
                    {
                        totalRows = totalRows.Where(x => x.PROCEDURE_NAME.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(DeliveryScreenView.LOCATION_NAME))
                    {
                        totalRows = totalRows.Where(x => x.LOCATION_NAME.ToLower().Contains(rule.data.ToLower()));
                    }

                }
            }

            var orderBy = nameof(DeliveryScreenView.DUE_DATE);

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