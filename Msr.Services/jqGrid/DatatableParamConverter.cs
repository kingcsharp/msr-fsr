using System;
using System.Web.Mvc;

namespace Msr.Services.jqGrid
{

    public class JqGridParamConverter : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext", "controllerContext is null.");
            }

            if (bindingContext == null)
            {
                throw new ArgumentNullException("bindingContext", "bindingContext is null.");
            }

            var request = controllerContext.RequestContext.HttpContext.Request;

            var param = new JqGridParam
            {
                isSearch = bool.Parse(request["_search"] ?? "false"),
                pageIndex = int.Parse(request["page"] ?? "0"),
                pageSize = int.Parse(request["rows"] ?? "10"),
                sortColumn = request["sidx"] ?? "",
                sortOrder = request["sord"] ?? "asc",
                id = request["id"] ?? "",
                param = request["oper"] ?? "",
                editOper = request["edit"] ?? "",
                addOper = request["add"] ?? "",
                delOper = request["del"] ?? "",
                @where = JqGridFilter.Create(request["filters"] ?? ""),
                operation = (OPERATION)System.Enum.Parse(typeof(OPERATION), request["oper"] ?? "none")
            };

            return param;
        }
    }
}