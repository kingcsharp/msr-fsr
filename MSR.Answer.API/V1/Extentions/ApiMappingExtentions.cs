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
    public static class ApiMappingExtentions
    {
        public static SystemLogin ToSystemLoginCommand(this SystemLoginRequest request)
        {
            return new SystemLogin()
            {
                UserName = request.UserName,
                Password = request.Password
            };
        }

        public static ResetPassword ToResetPasswordCommand(this ResetPasswordRequest request)
        {
            return new ResetPassword()
            {
                Token = request.Token,
                Password = request.NewPassword
            };

        }

        public static ForgotPassword ToForgotPasswordCommand(this ForgotPasswordRequest request)
        {
            return new ForgotPassword()
            {
                UserName = request.UserName
            };
        }

        public static ForgotUserName ToForgotUserNameCommand(this ForgotUserNameRequest request)
        {
            return new ForgotUserName()
            {
                Email = request.Email
            };
        }

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

        public static GetWorkflowModel ToGetWorkflowCommand(this GetWorkflowRequest request)
        {
            return new GetWorkflowModel()
            {
                Id = request.Id
            };
        }

        public static GetPendingApprovalModel ToGetPendingApprovalCommand(this GetPendingApprovalRequest request)
        {
            return new GetPendingApprovalModel()
            {
                Table = request.Table
            };
        }

        public static GetPendingApprovalDetailsModel ToGetPendingApprovalDetailsCommand(this GetPendingApprovalDetailRequest request)
        {
            return new GetPendingApprovalDetailsModel()
            {
                Table = request.Table,
                Id = request.Id
            };
        }

        public static PostApprovalModel ToPostApprovalCommand(this PostPendingApprovalRequest request)
        {
            return new PostApprovalModel()
            {
                Id = request.Id,
                Table = request.Table,
                Comments = request.Comments
            };
        }

        public static DeactivateApprovalModel ToDeleteApprovalCommand(this DeletePendingApprovalRequest request)
        {
            return new DeactivateApprovalModel()
            {
                Id = request.Id,
                Table = request.Table
            };
        }

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
        public static GetWorkflowGroupsModel ToGetWorkflowGroupCommand(this GetWorkflowGroupRequest request)
        {
            return new GetWorkflowGroupsModel()
            {
                Id = request.Id
            };
        }

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

        public static GetWorkflowStageModel ToGetWorkflowStageCommand(this GetWorkflowStageRequest request)
        {
            return new GetWorkflowStageModel()
            {
                Id = request.Id
            };
        }
        public static CreateWorkflowStageModel ToCreateWorkflowStageCommand(this CreateWorkflowStageRequest request)
        {
            return new CreateWorkflowStageModel()
            {
                IsActive = request.IsActive,
                Name = request.Name,
                WorkflowGroupStageMapModel = request.WorkflowGroupStageMapModel
            };
        }
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

        public static CreateMenuRoleMap ToCreateMenuRoleMapCommand(this CreateMenuRoleMapRequest request)
        {
            return new CreateMenuRoleMap()
            {
                MenuId = request.MenuId,
                RoleId = request.RoleId
            };
        }

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

        public static CreateCustomer ToCreateCustomerCommand(this CreateCustomerRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateCustomer>(request);
        }

        public static UpdateCustomer ToUpdateCustomerCommand(this UpdateCustomerRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateCustomer>(request);
        }

        public static GetLocations ToGetLocationCommand(this GetLocationRequest request)
        {
            return AutoMapperHelper.Mapper.Map<GetLocations>(request);
        }

        public static CreateLocation ToCreateLocationCommand(this CreateLocationRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateLocation>(request);
        }
        public static UpdateLocation ToUpdateLocationCommand(this UpdateLocationRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateLocation>(request);
        }
        public static CreateUserRole ToCreateUserRoleCommand(this CreateUserRoleRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateUserRole>(request);
        }
        public static UpdateUserRole ToUpdateUserRoleCommand(this UpdateUserRoleRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateUserRole>(request);
        }
        public static CreateHelpPage ToCreateHelpPageCommand(this CreateHelpPageRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateHelpPage>(request);
        }
        public static UpdateHelpPage ToUpdateHelpPageCommand(this UpdateHelpPageRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateHelpPage>(request);
        }
        public static CreateHelpPageRole ToCreateHelpPageRoleCommand(this CreateHelpPageRoleRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateHelpPageRole>(request);
        }
        public static GetHelpPage ToGetHelpPageCommand(this GetHelpPageRequest request)
        {
            return AutoMapperHelper.Mapper.Map<GetHelpPage>(request);
        }

        public static CreatePart ToCreatePartCommand(this CreatePartRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreatePart>(request);
        }
        public static UpdatePart ToUpdatePartCommand(this UpdatePartRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdatePart>(request);
        }

        public static CreateProcedure ToCreateProcedureCommand(this CreateProcedureRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateProcedure>(request);
        }
        public static UpdateProcedure ToUpdateProcedureCommand(this UpdateProcedureRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateProcedure>(request);
        }

        public static CreateProcedureStep ToCreateProcedureStepCommand(this CreateProcedureStepRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateProcedureStep>(request);
        }
        public static UpdateProcedureStep ToUpdateProcedureStepCommand(this UpdateProcedureStepRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateProcedureStep>(request);
        }

        public static CreateProcedureStepMonitor ToCreateProcedureStepMonitorCommand(this CreateProcedureStepMonitorRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateProcedureStepMonitor>(request);
        }
        public static UpdateProcedureStepMonitor ToUpdateProcedureStepMonitorCommand(this UpdateProcedureStepMonitorRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateProcedureStepMonitor>(request);
        }

        public static CreateProcedureStepTemplate ToCreateProcedureStepTemplateCommand(this CreateProcedureStepTemplateRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateProcedureStepTemplate>(request);
        }
        public static UpdateProcedureStepTemplate ToUpdateProcedureStepTemplateCommand(this UpdateProcedureStepTemplateRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateProcedureStepTemplate>(request);
        }

        public static CreateProcedureType ToCreateProcedureTypeCommand(this CreateProcedureTypeRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateProcedureType>(request);
        }
        public static UpdateProcedureType ToUpdateProcedureTypeCommand(this UpdateProcedureTypeRequest request)
        {
            return AutoMapperHelper.Mapper.Map<UpdateProcedureType>(request);
        }


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
                    CustomerId = request.CustomerId,
                    Description = request.Description,
                    InvoiceClass = request.InvoiceClass,
                    InvoiceDate = request.InvoiceDate,
                    TaxPercentage = request.TaxPercentage,
                    InvoiceItems = new List<CreateUpdateInvoiceItem>() {
                            new CreateUpdateInvoiceItem() {
                                PurchaseOrderId = item.PurchaseOrderId,
                                WorkOrderId  = item.WorkOrderId
                            }
                        }
                };

                createIndividualInvoices.Invoices.Add(inv);
            }

            return createIndividualInvoices;
        }

        public static CreateOneInvoice ToCreateOneInvoiceCommand(this CreateInvoiceRequest request)
        {
            return new CreateOneInvoice()
            {
                CustomerId = request.CustomerId,
                Description = request.Description,
                InvoiceClass = request.InvoiceClass,
                InvoiceDate = request.InvoiceDate,
                TaxPercentage = request.TaxPercentage,
                InvoiceItems = request.InvoiceItems?.Select(x => new CreateUpdateInvoiceItem()
                {
                    PurchaseOrderId = x.PurchaseOrderId,
                    WorkOrderId = x.WorkOrderId
                }).ToList()
            };
        }

        public static GetInvoices ToGetInvoicesCommand(this GetInvoicesRequest request)
        {
            return new GetInvoices()
            {
                Id = request.Id,
                CustomerId = request.CustomerId.GetValueOrDefault(0),
                InvoiceDate = request.InvoiceDate.GetValueOrDefault(DateTime.MinValue),
                Total = request.Total,
                Description = request.Description,
                StatusId = request.StatusId
            };
        }

        public static UpdateInvoice ToUpdateInvoiceCommand(this UpdateInvoiceRequest request)
        {
            return new UpdateInvoice()
            {
                Id = request.Id,
                Description = request.Description,
                InvoiceDate = request.InvoiceDate,
                TaxPercentage = request.TaxPercentage,
                InvoiceItems = request.InvoiceItems?.Select(x => new CreateUpdateInvoiceItem()
                {
                    PurchaseOrderId = x.PurchaseOrderId,
                    WorkOrderId = x.WorkOrderId
                }).ToList()
            };
        }

        public static DownloadAsIIFInvoices ToDownloadCommand(this DownloadInvoicesRequest request)
        {
            return new DownloadAsIIFInvoices()
            {
                CustomerId = request.CustomerId,
                InvoiceDate = request.InvoiceDate.GetValueOrDefault(DateTime.MinValue),
                Total = request.Total,
                Description = request.Description,
                StatusId = request.StatusId
            };
        }



        public static RemoveMenuRoleMap ToRemoveMenuRoleMapCommand(this DeleteMenuRoleMapRequest request)
        {
            return AutoMapperHelper.Mapper.Map<RemoveMenuRoleMap>(request);
        }

        public static CreateFile ToCreateFileCommand(this CreateFileRequest request)
        {
            return AutoMapperHelper.Mapper.Map<CreateFile>(request);
        }

        public static UploadFile ToUploadFileCommand(this UploadFileRequest request)
        {
            var file = AutoMapperHelper.Mapper.Map<UploadFile>(request);
            var stream = new MemoryStream();
            request.Image.CopyTo(stream);
            file.Base64String = Convert.ToBase64String(stream.ToArray());
            return file;
        }
    }
}
