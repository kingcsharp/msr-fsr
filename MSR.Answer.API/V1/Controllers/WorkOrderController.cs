using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Answer.API.V1.Models;
using MSR.Domain.Models;
using NSwag.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class WorkOrderController : BaseApiController
    {
        [HttpGet]
        [SwaggerResponse(typeof(AuditActionResult<ICollection<WorkOrderModel>>))]
        public async Task<IActionResult> Get([FromQuery] GetWorkOrderRequest request)
        {
            var ret = new List<WorkOrderModel>() {
               new WorkOrderModel()
            {
                ActualEndDate = DateTime.Now,
                ActualStartDate = DateTime.Now.AddDays(-3),
                HasNCR = true,
                Id = 1,
                Location = new LocationModel() { Id = 22, Name = "Loc1" },
                LocationId = 22,
                Price = 23,
                Product = new ProductModel() { Name = "Prod1" },
                ProductId = 1,
                Purchase = new PurchaseModel()
                {
                    CustomerPurchaseNumber = "ababsf123",
                    Id = 24
                },
                PurchaseId=24
            },
               new WorkOrderModel()
            {
                ActualEndDate = DateTime.Now,
                ActualStartDate = DateTime.Now.AddDays(-4),
                HasNCR = false,
                Id = 2,
                Location = new LocationModel() { Id = 12, Name = "Loc12" },
                LocationId = 12,
                Price = 55,
                Product = new ProductModel() { Name = "Prod12" },
                ProductId = 2,
                Purchase = new PurchaseModel()
                {
                    CustomerPurchaseNumber = "bbbbbb",
                    Id = 23
                },
                PurchaseId=23
            }
               };

            return new OkObjectResult(new AuditActionResult<ICollection<WorkOrderModel>>()
            {
                Object = ret,
                SuccessMessage = "GOD GUY"
            });
        }
    }
}