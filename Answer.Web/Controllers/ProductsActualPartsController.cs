using System;
using System.Linq;
using System.Web.Mvc;
using Answer.Web.Filters;
using Msr.Models.Menus;
using Msr.Models.ProductsActualPart;
using Msr.Services.ActualParts;
using Msr.Services.jqGrid;
using Msr.Services.ProductsActualParts;

namespace Answer.Web.Controllers
{
    [AuthorizeUser(ModuleName = MenuGroupConstants.Parts)]
    public class ProductsActualPartsController : BaseController
    {
        // GET: ProductsActualParts
        public ActionResult Index(string id)
        {
            var actualPartService = new ActualPartsService();

            var viewModel = actualPartService.GetActualPartById(id: id);

            ViewBag.ActiveClass = "Actual Parts";

            return View(viewModel);
        }
        public ActionResult ProductsActualPartData(JqGridParam param, string id)
        {
            var productsActualPartService = new ProductsActualPartService();

            //need to implement where clause and other clause from [dbo].[A_SP_PURCHASE_AGREEMENTS_SEARCH_FOR_ACTUAL_PART]
            var totalRows = productsActualPartService.GetProductsActualPartQueryable().AsQueryable();

            if (param.where != null && param.where.rules.Any())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(ProductsActualPartView.SupplierName))
                    {
                        totalRows = totalRows.Where(x => x.SupplierName == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(ProductsActualPartView.CustomerName))
                    {
                        totalRows = totalRows.Where(x => x.CustomerName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(ProductsActualPartView.ProductName))
                    {
                        totalRows = totalRows.Where(x => x.ProductName.ToLower().Contains(rule.data.ToLower()));
                    }
                }
            }

            var orderBy = nameof(ProductsActualPartView.ProductName);

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