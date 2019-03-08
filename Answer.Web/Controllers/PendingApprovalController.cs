using System;
using System.Linq;
using System.Web.Mvc;
using Msr.Models.Menus;
using Msr.Models.Workflows;
using Msr.Services.jqGrid;
using Msr.Services.Menus;
using Msr.Services.Users.Messages;
using Msr.Services.Workflows;

namespace Answer.Web.Controllers
{
    public class PendingApprovalController : BaseController
    {
        private readonly PendingApprovalService _pendingApprovalService;

        private readonly MenuService _menuService;

        public PendingApprovalController()
        {
            _pendingApprovalService = new PendingApprovalService();
            _menuService = new MenuService();
        }

        public ActionResult Index()
        {
            var getCurrentUser = GetCurrentUser();

            ViewBag.ActiveClass = "Pending Approvals";

            ViewBag.HasQuoteViewPermissions = HasQuoteViewPermissions(getCurrentUser);

            return View();
        }

        public ActionResult PendingApprovalsData(JqGridParam param, string itemType)
        {
            var totalRows = _pendingApprovalService.GetPedningApprovalsQueryable()
                .Where(x => x.ItemType != TableConstants.ProductHistory);

            if (!string.IsNullOrWhiteSpace(itemType) && !param.HasGridFilters())
            {
                totalRows = totalRows.Where(x => x.ItemType == itemType);
            }

            return ApplyFilters(param, totalRows);
        }

        public ActionResult PendingApprovalsProductData(JqGridParam param)
        {
            var totalRows = _pendingApprovalService.GetPedningApprovalsQueryable()
                .Where(x => x.ItemType == TableConstants.ProductHistory);

            return ApplyFilters(param, totalRows);
        }

        private ActionResult ApplyFilters(JqGridParam param, IQueryable<PendingApprovalView> totalRows)
        {
            var getCurrentUser = GetCurrentUser();

            totalRows = totalRows.Where(x => x.PersonId == getCurrentUser.Id);

            if (param.HasGridFilters())
            {
                foreach (var rule in param.where.rules)
                {
                    if (rule.field == nameof(PendingApprovalView.ObjectId))
                    {
                        totalRows = totalRows.Where(x => x.ObjectId.ToLower().Contains(rule.data.ToLower()));
                    }
                    if (rule.field == nameof(PendingApprovalView.ItemType))
                    {
                        totalRows = totalRows.Where(x => x.ItemType.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PendingApprovalView.ItemName))
                    {
                        totalRows = totalRows.Where(x => x.ItemName.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PendingApprovalView.ItemNumber))
                    {
                        totalRows = totalRows.Where(x => x.ItemNumber.ToLower().Contains(rule.data.ToLower()));
                    }
                    else if (rule.field == nameof(PendingApprovalView.ObjectId))
                    {
                        totalRows = totalRows.Where(x => x.ObjectId == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(PendingApprovalView.StageName))
                    {
                        totalRows = totalRows.Where(x => x.StageName == rule.data.ToLower());
                    }
                    else if (rule.field == nameof(PendingApprovalView.WfName))
                    {
                        totalRows = totalRows.Where(x => x.WfName == rule.data.ToLower());
                    }

                    else if (rule.field == nameof(PendingApprovalView.Revision))
                    {
                        int value;

                        if (int.TryParse(rule.data, out value))
                        {
                            totalRows = totalRows.Where(x => x.Revision == value);
                        }
                        else
                        {
                            totalRows = totalRows.Where(x => x.Revision.ToString().Contains(rule.data.ToLower()));
                        }
                    }
                    else if (rule.field == nameof(PendingApprovalView.Status) && rule.data != "")
                    {
                        var list = rule.data.Split(',').Select(x => x.Trim().ToLower()).ToArray();
                        if (list.Any())
                        {
                            totalRows = totalRows.Where(x => list.Contains(x.Status.ToLower()));
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(param.sortColumn))
            {
                param.sortColumn = nameof(PendingApprovalView.ObjectId);
            }

            var result = totalRows.ApplyPaging(param);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private bool HasQuoteViewPermissions(LoggedUserIdResult getCurrentUser)
        {
            var menus = _menuService.GetMenu(getCurrentUser.Id);

            return menus.Any(x => x.Name == MenuGroupConstants.Quotes);
        }
    }
}