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
    public class PurchaseController : BaseApiController
    {
        private const string privilegeApiName = "Purchase";
        private readonly ICommandDispatcher _dispatcher;

        public PurchaseController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<PurchaseModel>>))]
        public async Task<IActionResult> GetPurchases([FromQuery] GetPurchasesRequest filters)
        {
            //var getPurchasesGridView = filters.ToGetPurchasesGridViewCommand();
            //var ret = await _dispatcher.DispatchAsync(getPurchasesGridView);
            //return ret.ToOkObjectResponse<IEnumerable<PurchaseModel>>();
        }

        [HttpPost, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<PurchaseModel>))]
        public async Task<IActionResult> CreatePurchase([FromBody, Required] CreatePurchaseRequest request)
        {
            //var createOnePurchase = request.ToCreateOnePurchaseCommand();
            //var ret = await _dispatcher.DispatchAsync(createOnePurchase);
            //return ret.ToOkObjectResponse<PurchaseModel>("Purchase has been successfully created.");
        }

        [HttpPatch, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanEdit)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<PurchaseModel>))]
        public async Task<IActionResult> UpdatePurchase([FromBody, Required] UpdatePurchaseRequest request)
        {
            //var updatePurchase = request.ToUpdatePurchaseCommand();
            //var ret = await _dispatcher.DispatchAsync(updatePurchase);
            //return ret.ToOkObjectResponse<PurchaseModel>("Purchase has been successfully updated.");
        }

    }
}
