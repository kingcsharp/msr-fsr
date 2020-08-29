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
    /// <summary>
    /// Purchase Controller
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class PurchaseController : BaseApiController
    {
        private const string privilegeApiName = "Purchase";
        private readonly ICommandDispatcher _dispatcher;

        /// <summary>
        /// PurchaseController Constructor
        /// </summary>
        /// <param name="dispatcher"></param>
        public PurchaseController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        /// <summary>
        /// Get Purchase by id
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        [HttpGet, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<ICollection<PurchaseModel>>))]
        public async Task<IActionResult> GetPurchases([FromQuery] GetPurchasesRequest filters)
        {
            var getPurchases = filters.ToGetPurchasesCommand();
            var ret = await _dispatcher.DispatchAsync(getPurchases);
            return ret.ToOkObjectResponse<ICollection<PurchaseModel>>();
        }

        /// <summary>
        /// Create a new purchase
        /// </summary>
        /// <description>
        /// Create a new purchase.  This includes submitting the request
        /// to create the associated work order for the purchase.
        /// </description>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<PurchaseModel>))]
        public async Task<IActionResult> CreatePurchase([FromBody, Required] CreatePurchaseRequest request)
        {
            //var createOnePurchase = request.ToCreateOnePurchaseCommand();
            //var ret = await _dispatcher.DispatchAsync(createOnePurchase);
            //return ret.ToOkObjectResponse<PurchaseModel>("Purchase has been successfully created.");
            throw new NotImplementedException();
        }

    }
}
