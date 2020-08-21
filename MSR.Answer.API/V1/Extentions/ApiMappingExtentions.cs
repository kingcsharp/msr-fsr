using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Models.Workflow;
using MSR.Domain.Commands;
using MSR.Domain.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MSR.Answer.API.V1.Extentions
{
    /// <summary>
    ///
    /// </summary>
    public static class ApiMappingExtentions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static SystemLogin ToSystemLoginCommand(this SystemLoginRequest request)
        {
            return new SystemLogin()
            {
                UserName = request.UserName,
                Password = request.Password
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ResetPassword ToResetPasswordCommand(this ResetPasswordRequest request)
        {
            return new ResetPassword()
            {
                Token = request.Token,
                Password = request.NewPassword
            };

        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ForgotPassword ToForgotPasswordCommand(this ForgotPasswordRequest request)
        {
            return new ForgotPassword()
            {
                UserName = request.UserName
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ForgotUserName ToForgotUserNameCommand(this ForgotUserNameRequest request)
        {
            return new ForgotUserName()
            {
                Email = request.Email
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetUsers ToGetUsersCommand(this GetUsersRequest request)
        {
            return new GetUsers()
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Title = request.Title,
                Supervisor = request.Supervisor,
                PrimaryPhone = request.PrimaryPhone,
                Email = request.Email
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateUser ToCreateUserCommand(this CreateUserRequest request)
        {
            return new CreateUser()
            {
                CurrentUser = CurrentUser.GetId(),
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Title = request.Title,
                Email = request.Email,
                SecurityStamp = request.SecurityStamp,
                Phone = request.Phone,
                SupervisorId = request.SupervisorId,
                LocationId = request.LocationId,
                IsActive = request.IsActive,
                IsAnswerUser = request.IsAnswerUser,
                CustomerId = request.CustomerId,
                LockoutEndDateUtc = request.LockoutEndDateUtc,
                LockoutEnabled = request.LockoutEnabled,
                AccessFailedCount = request.AccessFailedCount,
                TimeZoneId = request.TimeZoneId,
                Roles = request.Roles

            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetWorkflowModel ToGetWorkflowCommand(this GetWorkflowRequest request)
        {
            return new GetWorkflowModel()
            {
                Id = request.Id
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetPendingApprovalModel ToGetPendingApprovalCommand(this GetPendingApprovalRequest request)
        {
            return new GetPendingApprovalModel()
            {
                Table = request.Table
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetPendingApprovalDetailsModel ToGetPendingApprovalDetailsCommand(this GetPendingApprovalDetailRequest request)
        {
            return new GetPendingApprovalDetailsModel()
            {
                Table = request.Table,
                Id = request.Id
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static PostApprovalModel ToPostApprovalCommand(this PostPendingApprovalRequest request)
        {
            return new PostApprovalModel()
            {
                Id = request.Id,
                Table = request.Table,
                Comments = request.Comments
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static DeactivateApprovalModel ToDeleteApprovalCommand(this DeletePendingApprovalRequest request)
        {
            return new DeactivateApprovalModel()
            {
                Id = request.Id,
                Table = request.Table
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateWorkflowModel ToCreateWorkflowGroupCommand(this CreateWorkflowRequest request)
        {
            return new CreateWorkflowModel()
            {
                IsActive = request.IsActive,
                Name = request.Name,
                MemberStages = request.MemberStages,
                ActivityMaps = request.ActivityMaps
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateWorkflowModel ToUpdateWorkflowGroupCommand(this UpdateWorkflowRequest request)
        {
            return new UpdateWorkflowModel()
            {
                Id = request.Id,
                IsActive = request.IsActive,
                Name = request.Name,
                MemberStages = request.MemberStages,
                ActivityMaps = request.ActivityMaps
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetWorkflowGroupsModel ToGetWorkflowGroupCommand(this GetWorkflowGroupRequest request)
        {
            return new GetWorkflowGroupsModel()
            {
                Id = request.Id
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateWorkflowGroupModel ToCreateWorkflowGroupCommand(this CreateWorkflowGroupRequest request)
        {
            return new CreateWorkflowGroupModel()
            {
                IsActive = request.IsActive,
                Name = request.Name,
                Roles = request.Roles,
                Users = request.Users
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateWorkflowGroupModel ToUpdateWorkflowGroupCommand(this UpdateWorkflowGroupRequest request)
        {
            return new UpdateWorkflowGroupModel()
            {
                Id = request.Id,
                IsActive = request.IsActive,
                Name = request.Name,
                Roles = request.Roles,
                Users = request.Users
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetWorkflowStageModel ToGetWorkflowStageCommand(this GetWorkflowStageRequest request)
        {
            return new GetWorkflowStageModel()
            {
                Id = request.Id
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateWorkflowStageModel ToCreateWorkflowStageCommand(this CreateWorkflowStageRequest request)
        {
            return new CreateWorkflowStageModel()
            {
                IsActive = request.IsActive,
                Name = request.Name,
                WorkflowGroupStageMapModel = request.WorkflowGroupStageMapModel
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateWorkflowStageModel ToUpdateWorkflowStageCommand(this UpdateWorkflowStageRequest request)
        {
            return new UpdateWorkflowStageModel()
            {
                Id = request.Id,
                IsActive = request.IsActive,
                Name = request.Name,
                WorkflowGroupStageMapModel = request.WorkflowGroupStageMapModel
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateUser ToUpdateUserCommand(this UpdateUserRequest request)
        {
            return new UpdateUser()
            {
                Id = request.Id,
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Title = request.Title,
                Email = request.Email,
                SecurityStamp = request.SecurityStamp,
                Phone = request.Phone,
                SupervisorId = request.SupervisorId,
                LocationId = request.LocationId,
                IsActive = request.IsActive,
                IsAnswerUser = request.IsAnswerUser,
                CustomerId = request.CustomerId,
                LockoutEndDateUtc = request.LockoutEndDateUtc,
                LockoutEnabled = request.LockoutEnabled,
                AccessFailedCount = request.AccessFailedCount,
                TimeZoneId = request.TimeZoneId,
                Roles = request.Roles
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateMenuRoleMap ToCreateMenuRoleMapCommand(this CreateMenuRoleMapRequest request)
        {
            return new CreateMenuRoleMap()
            {
                MenuId = request.MenuId,
                RoleId = request.RoleId
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateMenuRoleMap ToUpdateMenuRoleMapCommand(this UpdateMenuRoleMapRequest request)
        {
            return new UpdateMenuRoleMap()
            {
                CanActivate = request.CanActivate,
                CanApprove = request.CanApprove,
                CanCreate = request.CanCreate,
                CanDelete = request.CanDelete,
                CanEdit = request.CanEdit,
                CanRead = request.CanRead,
                RoleId = request.RoleId,
                MenuId = request.MenuId
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetMultipleCustomers ToGetMultipleCustomersCommand(this GetMultipleCustomersRequest request)
        {
            return new GetMultipleCustomers()
            {
                Id = request.Id,
                Address = request.Address,
                IsActive = request.IsActive,
                LocationId = request.LocationId,
                Name = request.Name,
                Phone = request.Phone,
                PrimaryContactUserId = request.PrimaryContactUserId,
                SecondaryContactUserId = request.SecondaryContactUserId
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateCustomer ToCreateCustomerCommand(this CreateCustomerRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateCustomer>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateCustomer ToUpdateCustomerCommand(this UpdateCustomerRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateCustomer>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetLocations ToGetLocationCommand(this GetLocationRequest request)
        {
            return AutoMapperHelper.Mapper.Map<GetLocations>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateLocation ToCreateLocationCommand(this CreateLocationRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateLocation>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateLocation ToUpdateLocationCommand(this UpdateLocationRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateLocation>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateUserRole ToCreateUserRoleCommand(this CreateUserRoleRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateUserRole>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateUserRole ToUpdateUserRoleCommand(this UpdateUserRoleRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateUserRole>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateHelpPage ToCreateHelpPageCommand(this CreateHelpPageRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateHelpPage>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateHelpPage ToUpdateHelpPageCommand(this UpdateHelpPageRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateHelpPage>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateHelpPageRole ToCreateHelpPageRoleCommand(this CreateHelpPageRoleRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateHelpPageRole>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetHelpPage ToGetHelpPageCommand(this GetHelpPageRequest request)
        {
            return AutoMapperHelper.Mapper.Map<GetHelpPage>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreatePart ToCreatePartCommand(this CreatePartRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreatePart>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdatePart ToUpdatePartCommand(this UpdatePartRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdatePart>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateProcedure ToCreateProcedureCommand(this CreateProcedureRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateProcedure>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateProcedure ToUpdateProcedureCommand(this UpdateProcedureRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateProcedure>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateProcedureStep ToCreateProcedureStepCommand(this CreateProcedureStepRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateProcedureStep>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateProcedureStep ToUpdateProcedureStepCommand(this UpdateProcedureStepRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateProcedureStep>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateProcedureStepMonitor ToCreateProcedureStepMonitorCommand(this CreateProcedureStepMonitorRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateProcedureStepMonitor>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateProcedureStepMonitor ToUpdateProcedureStepMonitorCommand(this UpdateProcedureStepMonitorRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateProcedureStepMonitor>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateProcedureStepTemplate ToCreateProcedureStepTemplateCommand(this CreateProcedureStepTemplateRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateProcedureStepTemplate>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateProcedureStepTemplate ToUpdateProcedureStepTemplateCommand(this UpdateProcedureStepTemplateRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateProcedureStepTemplate>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateProcedureType ToCreateProcedureTypeCommand(this CreateProcedureTypeRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateProcedureType>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateProcedureType ToUpdateProcedureTypeCommand(this UpdateProcedureTypeRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateProcedureType>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateWorkOrder ToCreateWorkOrderCommand(this CreateWorkOrderRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateWorkOrder>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateWorkOrder ToUpdateWorkOrderCommand(this UpdateWorkOrderRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateWorkOrder>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static DeleteWorkOrder ToDeleteWorkOrderCommand(this DeleteWorkOrderRequest request)
        {
            return AutoMapperHelper.Mapper.Map<DeleteWorkOrder>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetWorkOrder ToGetWorkOrderCommand(this GetWorkOrderRequest request)
        {
            return AutoMapperHelper.Mapper.Map<GetWorkOrder>(request);
        }

        /// <summary>
        /// Creates a collection of invoice commands for each InvoiceItem in the command.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateIndividualInvoices ToCreateIndividualInvoicesCommand(this CreateInvoiceRequest request)
        {
            var createIndividualInvoices = new CreateIndividualInvoices()
            {
                Invoices = new List<CreateOneInvoice>()
            };

            foreach (var item in request.InvoiceItems ?? new List<CreateInvoiceItemRequest>())
            {
                var inv = new CreateOneInvoice()
                {
                    CustomerId = request.CustomerId.GetValueOrDefault(),
                    Description = request.Description,
                    InvoiceClass = request.InvoiceClass,
                    InvoiceDate = request.InvoiceDate,
                    TaxPercentage = request.TaxPercentage,
                    InvoiceItems = new List<CreateUpdateInvoiceItem>() {
                            new CreateUpdateInvoiceItem() {
                                PurchaseOrderId = item.PurchaseOrderId.GetValueOrDefault(),
                                WorkOrderId  = item.WorkOrderId.GetValueOrDefault()
                            }
                        }
                };

                createIndividualInvoices.Invoices.Add(inv);
            }

            return createIndividualInvoices;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateOneInvoice ToCreateOneInvoiceCommand(this CreateInvoiceRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateOneInvoice>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetInvoices ToGetInvoicesCommand(this GetInvoicesRequest request)
        {
            return AutoMapperHelper.Mapper.Map<GetInvoices>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetInvoicesGridView ToGetInvoicesGridViewCommand(this GetInvoicesRequest request)
        {
            return AutoMapperHelper.Mapper.Map<GetInvoicesGridView>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UpdateInvoice ToUpdateInvoiceCommand(this UpdateInvoiceRequest request)
        {
            return new UpdateInvoice()
            {
                Id = request.Id.GetValueOrDefault(0),
                Description = request.Description,
                InvoiceDate = request.InvoiceDate,
                TaxPercentage = request.TaxPercentage,
                InvoiceItems = request.InvoiceItems?.Select(x => new CreateUpdateInvoiceItem()
                {
                    Id = x.Id.GetValueOrDefault(0)
                }).ToList()
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static DownloadAsIIFInvoices ToDownloadCommand(this DownloadInvoicesRequest request)
        {
            return AutoMapperHelper.Mapper.Map<DownloadAsIIFInvoices>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static RemoveMenuRoleMap ToRemoveMenuRoleMapCommand(this DeleteMenuRoleMapRequest request)
        {
            return AutoMapperHelper.Mapper.Map<RemoveMenuRoleMap>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static CreateFile ToCreateFileCommand(this CreateFileRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateFile>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static UploadFile ToUploadFileCommand(this UploadFileRequest request)
        {
            var stream = new MemoryStream();
            var incomingFile = request.Upload.First();
            incomingFile.CopyTo(stream);

            var file = new UploadFile()
            {
                ContentType = incomingFile.ContentType,
                FileName = incomingFile.FileName,
                Name = incomingFile.Name,
                FileContents = stream.ToArray()
            };

            return file;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ImportFile ToImportFileCommand(this ImportRequest request)
        {
            return AutoMapperHelper.Mapper.Map<ImportFile>(request);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static GetSensor ToGetSensorCommand(this GetSensorRequest request) => AutoMapperHelper.Mapper.Map<GetSensor>(request);
    }
}
