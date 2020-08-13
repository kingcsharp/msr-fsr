using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using NSwag.Annotations;
using MSR.Answer.API.Attributes;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class InvoiceController : BaseApiController
    {
        private const string privilegeApiName = "Invoices";
        private readonly ICommandDispatcher _dispatcher;

        public InvoiceController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<IEnumerable<InvoiceView>>))]
        public async Task<IActionResult> GetInvoices([FromQuery] GetInvoicesRequest filters)
        {
            var getInvoicesGridView = filters.ToGetInvoicesGridViewCommand();
            var ret = await _dispatcher.DispatchAsync(getInvoicesGridView);
            return ret.ToOkObjectResponse<IEnumerable<InvoiceView>>();
        }

        [HttpPost("CreateOneInvoice"), HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.Created, typeof(AuditActionResult<InvoiceView>))]
        public async Task<IActionResult> CreateOneInvoice([FromBody, Required] CreateInvoiceRequest request)
        {
            var createOneInvoice = request.ToCreateOneInvoiceCommand();
            var ret = await _dispatcher.DispatchAsync(createOneInvoice);
            return ret.ToOkObjectResponse<InvoiceView>("Invoice has been successfully created.");
        }

        [HttpPost("CreateIndividualInvoices"), HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanCreate)]
        [SwaggerResponse(HttpStatusCode.Created, typeof(AuditActionResult<IEnumerable<InvoiceView>>))]
        public async Task<IActionResult> CreateIndividualInvoices([FromBody, Required] CreateInvoiceRequest request)
        {
            var createIndividualInvoices = request.ToCreateIndividualInvoicesCommand();
            var ret = await _dispatcher.DispatchAsync(createIndividualInvoices);
            return ret.ToOkObjectResponse<IEnumerable<InvoiceView>>("Invoice has been successfully created.");
        }

        [HttpPatch, HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanEdit)]
        [SwaggerResponse(HttpStatusCode.OK, typeof(AuditActionResult<InvoiceView>))]
        public async Task<IActionResult> UpdateInvoice([FromBody, Required] UpdateInvoiceRequest request)
        {
            var updateInvoice = request.ToUpdateInvoiceCommand();
            var ret = await _dispatcher.DispatchAsync(updateInvoice);
            return ret.ToOkObjectResponse<InvoiceView>("Invoice has been successfully updated.");
        }

        [HttpGet("Download"), HasPrivilegeApi(privilegeApiName, EnumPrivilege.CanRead)]
        [SwaggerResponse(typeof(FileStreamResult))]
        public async Task<IActionResult> DownloadAsync([FromQuery] DownloadInvoicesRequest filters)
        {
            // Command Download Invoices using Filter
            var downloadInvoices = filters.ToDownloadCommand();

            var ret = await _dispatcher.DispatchAsync(downloadInvoices);

            return ret.ToFileResponse<MemoryStream>(this, $"All Invoices - {DateTime.Now.ToShortDateString()}.zip");
        }
    }
}
