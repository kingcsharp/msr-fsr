using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Models.Workflow;
using MSR.Domain.Commands;
using MSR.Domain.Commands.Workflow;
using MSR.Domain.Helpers;

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
                CurrentUser = DelegateHandler.GetCurrentUserId(),
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

        public static GetWorkflowGroupsModel ToGetWorkflowGroupCommand(this GetWorkflowGroupRequest request)
        {
            return new GetWorkflowGroupsModel()
            {
                Id = request.Id
            };
        }

        public static CreateWorkflowGroup ToCreateWorkflowGroupCommand(this CreateWorkflowGroupRequest request)
        {
            return new CreateWorkflowGroup()
            {
                IsActive = request.IsActive,
                Name = request.Name,
                Roles = request.Roles
            };
        }

        public static UpdateWorkflowGroupModel ToUpdateWorkflowGroupCommand(this UpdateWorkflowGroupRequest request)
        {
            return new UpdateWorkflowGroupModel()
            {
                Id = request.Id,
                IsActive = request.IsActive,
                Name = request.Name,
                Roles = request.Roles
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
                Name = request.Name
            };
        }

        public static UpdateWorkflowStageModel ToUpdateWorkflowStageCommand(this UpdateWorkflowStageRequest request)
        {
            return new UpdateWorkflowStageModel()
            {
                Id = request.Id,
                IsActive = request.IsActive,
                Name = request.Name
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
                MenuRoleId = request.MenuRoleId
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
            return new CreateCustomer()
            {
                Address = request.Address,
                LocationId = request.LocationId.GetValueOrDefault(0),
                Name = request.Name,
                Phone = request.Phone,
                PrimaryContactUserId = request.PrimaryContactUserId.GetValueOrDefault(0),
                SecondaryContactUserId = request.SecondaryContactUserId.GetValueOrDefault(0)
            };
        }

        public static UpdateCustomer ToUpdateCustomerCommand(this UpdateCustomerRequest request)
        {
            return new UpdateCustomer()
            {
                Address = request.Address,
                IsActive = request.IsActive,
                LocationId = request.LocationId,
                Name = request.Name,
                Phone = request.Phone,
                PrimaryContactUserId = request.PrimaryContactUserId,
                SecondaryContactUserId = request.SecondaryContactUserId
            };
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
    }
}
