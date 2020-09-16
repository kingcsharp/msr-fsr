using System;
using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSR.Domain.Models;
using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Extentions;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;
using MSR.Domain.Views;
using Rollbar.Common;
using MSR.Domain.Helpers;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class ReportController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;


        public ReportController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<ReportModel>>))]
        [HasPrivilegeApi("Reports", EnumPrivilege.CanRead)]
        public async Task<IActionResult> Get()
        {

            var categories = new List<ReportCategoryMapModel>() { new ReportCategoryMapModel {
                ReportCategory = new ReportCategoryModel(){Id=1,Name="WorkOrder" },
                ReportCategoryId=1,
                ReportId=1
            }
            };

            var response = new AuditActionResult<IEnumerable<ReportModel>>()
            {
                SuccessMessage = "Search Successfully returned 2 results",
                Object = new List<ReportModel>() {
                    new ReportModel(){Categories=categories, Id=1,Name="WorkOrder Parts History",Subtitle="Subtitleby Part Number",Description="Reports historical WorkOrder Parts Information by Part Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/1",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"},
                    new ReportModel(){Categories=categories,Id=5,Name="WorkOrder Parts History",Subtitle="Subtitleby Serial Number",Description="Reports historical WorkOrder Parts Information by Part Serial Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/5",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"},
                    new ReportModel(){Categories=categories,Id=7,Name="Serial Number History",Subtitle="Subtitleby Serial Number",Description="Reports historical WorkOrder Parts Information by Serial Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/7",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"},
                    new ReportModel(){Categories=categories,Id=10,Name="Work In Process",Subtitle="Subtitleby WorkOrder",Description="Reports monitors recorded by WorkOrder, Part, and Serial Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/10",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"},
                    new ReportModel(){Categories=categories, Id=1,Name="WorkOrder Parts History",Subtitle="Subtitleby Part Number",Description="Reports historical WorkOrder Parts Information by Part Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/1",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"},
                    new ReportModel(){Categories=categories,Id=5,Name="WorkOrder Parts History",Subtitle="Subtitleby Serial Number",Description="Reports historical WorkOrder Parts Information by Part Serial Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/5",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"},
                    new ReportModel(){Categories=categories,Id=7,Name="Serial Number History",Subtitle="Subtitleby Serial Number",Description="Reports historical WorkOrder Parts Information by Serial Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/7",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"},
                    new ReportModel(){Categories=categories,Id=10,Name="Work In Process",Subtitle="Subtitleby WorkOrder",Description="Reports monitors recorded by WorkOrder, Part, and Serial Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/10",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"},
                    new ReportModel(){Categories=categories, Id=1,Name="WorkOrder Parts History",Subtitle="Subtitleby Part Number",Description="Reports historical WorkOrder Parts Information by Part Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/1",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"},
                    new ReportModel(){Categories=categories,Id=5,Name="WorkOrder Parts History",Subtitle="Subtitleby Serial Number",Description="Reports historical WorkOrder Parts Information by Part Serial Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/5",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"},
                    new ReportModel(){Categories=categories,Id=7,Name="Serial Number History",Subtitle="Subtitleby Serial Number",Description="Reports historical WorkOrder Parts Information by Serial Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/7",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"},
                    new ReportModel(){Categories=categories,Id=10,Name="Work In Process",Subtitle="Subtitleby WorkOrder",Description="Reports monitors recorded by WorkOrder, Part, and Serial Number",APIEndPointURL="https://reporting-api.cmhworks.com/v1/answer/report/10",ImageUrl="https://cmh-images.s3.amazonaws.com/report-grid.png"}
                }
            };

            return new OkObjectResult(response);
        }
    }
}
