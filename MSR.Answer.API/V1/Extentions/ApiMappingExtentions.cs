using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Models.Workflow;
using MSR.Domain.Commands;
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

        public static GetWorkflowModel ToGetWorkflowCommand(this GetWorkflowRequest request)
        {
            return new GetWorkflowModel()
            {
                Id = request.Id
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
            return AutoMapperHelper.Mapper.Map<UpdateMenuRoleMap>(request);
        }
    }
}
