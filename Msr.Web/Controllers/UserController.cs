using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Msr.Models.Orders;
using Msr.Models.Users;
using Msr.Services.jqGrid;
using Msr.Services.Orders;
using Msr.Services.Users;

namespace Msr.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
      //  [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult Master()
        {
            return View();
        }

       // [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult MasterUserData(JqGridParam param)
        {
            var userService = new UserService();

            var totalRows = userService.GetUserQueryable();

            string orderBy = nameof(UserView.FirstName);
            string orderDirection = "asc";

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
            totalRows = totalRows.Skip(param.pageIndex-1);
            totalRows = totalRows.Take(param.pageSize);

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);


            var results = totalRows.Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.FullName,
                x.RoleId,
                x.Phone,
                x.Phone2,
                x.UserName,
                x.PasswordHash,
                x.Email,
                x.IsActive,
                x.TimeZone,
                x.CreatedDate
            }).ToList();

            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Client()
        {
            return View();
        }

        // [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult ClientUserData(JqGridParam param)
        {
            var userService = new UserService();

            var totalRows = userService.GetUserQueryable();

            totalRows = totalRows.Where(x => x.RoleId == RolesConstants.ClientAdmin);

            string orderBy = nameof(UserView.FirstName);
            string orderDirection = "asc";

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
            totalRows = totalRows.Skip(param.pageIndex - 1);
            totalRows = totalRows.Take(param.pageSize);

            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)param.pageSize);


            var results = totalRows.Select(x => new
            {
                x.Id,
                x.FirstName,
                x.LastName,
                x.FullName,
                x.RoleId,
                x.Phone,
                x.Phone2,
                x.UserName,
                x.PasswordHash,
                x.Email,
                x.IsActive,
                x.TimeZone,
                x.CreatedDate
            }).ToList();

            var json = new
            {
                total = totalPages,
                page = param.pageIndex,
                records = totalRecords,
                rows = results
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
      //  [Authorize(Roles = nameof(RolesConstants.SuperAdmin))]
        public ActionResult AddUser(UserSummary userSummary)
        {
            var userService = new UserService();

            var response = userService.AddUser(userSummary);

            if (response.HasErrors())
            {
                return Json(new {responseText = string.Join(",", response.ErrorMessage())}, JsonRequestBehavior.AllowGet);
            }

            return Content("Ok");
        }

    }
}