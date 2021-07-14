using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Extentions;
using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Models.Paging;
using MSR.Application.Abstractions;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using NSwag.Annotations;

namespace MSR.Answer.API.V1.Controllers
{
    /// <summary>
    /// WorkOrderController
    /// </summary>
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class WorkOrderController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;
        private IWorkOrderViewService _workOrderViewService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="workOrderViewService"></param>
        public WorkOrderController(ICommandDispatcher dispatcher, IWorkOrderViewService workOrderViewService)
        {
            _dispatcher = dispatcher;
            _workOrderViewService = workOrderViewService;
        }

        /// <summary>
        /// Returns a summary of COMPLETED or CANCELLED WorkOrders
        /// </summary>
        /// <response code="200"></response>
        [HttpGet("History")]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<WorkOrderHistoryView>>))]
        public async Task<IActionResult> WorkOrderGetHistory([FromQuery] GetWorkOrderHistoryRequest request)
        {
            var command = request.ToGetWorkOrderHistory();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ICollection<WorkOrderHistoryView>>();
        }

        /// <summary>
        /// Returns WorkOrder SelectItems
        /// </summary>
        /// <response code="200"></response>
        [HttpGet("SelectItems")]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<WorkOrderSelectItem>>))]
        public async Task<IActionResult> WorkOrderGetSelectItems([FromQuery] GetAssignedWorkOrdersRequest request)
        {
            var command = request.ToWorkOrderSelectItem();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ICollection<WorkOrderSelectItem>>();
        }

        /// <summary>
        /// Returns a summary of WorkOrders IN PROGRESS or WAITING
        /// </summary>
        /// <response code="200"></response>
        [HttpGet("Menu")]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<WorkOrderGridSummary>>))]
        public async Task<IActionResult> WorkOrderGetMenu([FromQuery]GetWorkOrderMenuRequest request)
        {
            var queryModel = request.ToGetWorkOrderQueryModel();
            var workOrderMenuViews = await _workOrderViewService.GetWorkOrderMenuAsync(queryModel);
            return GenerateOkViewResponse(workOrderMenuViews.data, workOrderMenuViews.totalRows);
        }

        /// <summary>
        /// Returns the WorkOrders that are either waiting to start or in Process.
        /// </summary>
        /// <response code="200">The WorkOrders that are either waiting to start or in Process</response>
        [HttpGet("Status")]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<WorkOrderStatus>>))]
        public async Task<IActionResult> WorkOrderGetStatus()
        {
            var workOrderStatusViews = await _workOrderViewService.GetWorkOrderStatusAsync();
            return GenerateOkViewResponse(workOrderStatusViews);
        }

        /// <summary>
        /// Get work order list by id, customerid, locationid, or date
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<WorkOrderModel>>))]
        public async Task<IActionResult> GetWorkOrder([FromQuery] GetWorkOrderRequest request)
        {
            var command = request.ToGetWorkOrderCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ICollection<WorkOrderModel>>();
        }

        /// <summary>
        /// Get InvoiceableWorkOrders
        /// </summary>
        /// <returns></returns>
        [HttpGet("Invoiceable")]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<InvoiceableWorkOrderView>>))]
        public async Task<IActionResult> GetInvoiceableWorkOrders()
        {
            var workOrders = await _workOrderViewService.GetInvoiceableWorkOrdersAsync();
            return GenerateOkViewResponse(workOrders);
        }

        [HttpGet("Portal")]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<PortalWorkOrderView>>))]
        public async Task<IActionResult> GetPortalWorkOrder([FromQuery] GetPortalWorkOrderRequest request)
        {
            var queryModel = request.ToGetPortalWorkOrderQueryModel();
            var portalWorkOrderMenuViews = await _workOrderViewService.GetPortalWorkOrderMenuAsync(queryModel);
            return GenerateOkViewResponse(portalWorkOrderMenuViews.data, portalWorkOrderMenuViews.totalRows);
        }

        [HttpPatch("Message")]
        [SwaggerResponse(typeof(AuditActionResult<WorkOrderMessageModel>))]
        public async Task<IActionResult> AddMessage([FromBody, Required] CreateWorkOrderMessageRequest request)
        {
            var command = request.ToCreateWorkOrderMessageCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<WorkOrderMessageModel>("Message added successfully");

        }

        [HttpPatch("TakeOver")]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<WorkOrderTaskModel>>))]
        public async Task<IActionResult> WorkOrderTakeOver([FromBody] TakeOverWorkOrderRequest request)
        {
            var command = request.ToTakeOverWorkOrderCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ICollection<WorkOrderTaskModel>>("Work Order has been taken over");
        }

        [HttpPatch("WorkOrderCancel")]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<WorkOrderTaskModel>>))]
        public async Task<IActionResult> WorkOrderCancel([FromBody] CancelWorkOrderRequest request)
        {
            var command = request.ToCancelWorkOrderCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ICollection<WorkOrderTaskModel>>("Work Order has been cancelled!");
        }

        [HttpPost("AddNCRWorkOrderTask")]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<WorkOrderTaskModel>>))]
        public async Task<IActionResult> AddNCRWorkOrderTask([FromBody] AddNCRWorkOrderTaskRequest request)
        {
            var command = request.ToAddNCRWorkOrderTaskCommand();
            var ret = await _dispatcher.DispatchAsync(command);
            return ret.ToOkObjectResponse<ICollection<WorkOrderTaskModel>>("NCR Work Order Task has been added!");
        }

        [HttpPost("Create")]
        [SwaggerResponse(typeof(AuditActionResult<string>))]
        public async Task<IActionResult> CreateWorkOrder([FromBody] CreateWorkOrderRequest request)
        {
            var command = request.ToCreateWorkOrderCommand();
            var returnValue = await _dispatcher.DispatchAsync(command);
            return returnValue.ToOkObjectResponse<string>("WorkOrder Created Successfully");
        }
    }
}
