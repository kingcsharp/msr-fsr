using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using MSR.Answer.API.Attributes;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class PurchaseOrderController : BaseApiController
    {
        private const string privilegeApiName = "PurchaseOrder";
        private readonly ICommandDispatcher _dispatcher;

        public PurchaseOrderController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<PurchaseOrderView>>))]
        public async Task<IActionResult> GetPurchases([FromQuery] GetPurchaseOrderRequest filters)
        {
            var getPurchaseOrder = filters.ToGetPurchaseOrderRequestCommand();
            var ret = await _dispatcher.DispatchAsync(getPurchaseOrder);
            return ret.ToOkObjectResponse<IEnumerable<PurchaseOrderView>>();
        }

        //[HttpPost, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
        //[SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<PurchaseModel>))]
        //public async Task<IActionResult> CreatePurchase([FromBody, Required] CreatePurchaseRequest request)
        //{
        //    //var createOnePurchase = request.ToCreateOnePurchaseCommand();
        //    //var ret = await _dispatcher.DispatchAsync(createOnePurchase);
        //    //return ret.ToOkObjectResponse<PurchaseModel>("Purchase has been successfully created.");
        //}

    }
}
