using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Application.ApplicationServices
{
    public class UserAppService :
        ICommandHandler<GetUsers>,
        ICommandHandler<CreateUser>,
        ICommandHandler<DeactivateUser>,
        ICommandHandler<UpdateUser>,
        ICommandHandler<GetLoggedInUserData>,
        ICommandHandler<CreateUserRole>,
        ICommandHandler<UpdateUserRole>,
        ICommandHandler<DeleteUserRole>,
        ICommandHandler<GetTrainingCertification>
    {
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public UserAppService(IUserService userService, IFileService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<ICommandResponse> HandleAsync(GetUsers command, CancellationToken cancellationToken = default)
        {
            var ret = await _userService.GetUsersAsync(command);
            if (command.Id.HasValue && ret.Count == 1)
            {
                var file = _fileService.ListFiles(new User().GetType().Name, command.Id.Value).FirstOrDefault();
                if (file != null)
                {
                    ret.FirstOrDefault().FileModel = file;
                }
            }

            return new CommandResponse<ICollection<UserModel>>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(CreateUser command, CancellationToken cancellationToken = default)
        {
            var ret = await _userService.CreateUserAsync(command);

            if (command.File != null && command.File.Base64String.Length > 0)
            {
                var files = await _fileService.AttachFilesAsync(new User().GetType().Name, ret.Id,
                    new List<FileModel>() { command.File });
                ret.FileModel = files.FirstOrDefault();
            }

            return new CommandResponse<UserModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(DeactivateUser command, CancellationToken cancellationToken = default)
        {
            await _userService.DeactivateUserAsync(command);
            return new CommandResponse();
        }

        public async Task<ICommandResponse> HandleAsync(UpdateUser command, CancellationToken cancellationToken = default)
        {
            var ret = await _userService.UpdateUserAsync(command);
            if (command.File != null && command.File.Base64String.Length > 0)
            {
                var files = await _fileService.AttachFilesAsync(new User().GetType().Name, ret.Id, new List<FileModel>() { command.File });

                ret.FileModel = files.FirstOrDefault();
            }
            return new CommandResponse<UserModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(GetLoggedInUserData command, CancellationToken cancellationToken = default)
        {
            var ret = await _userService.GetLoggedInUserData(command.UserId);
            var file = _fileService.ListFiles(new User().GetType().Name, command.UserId).FirstOrDefault();
            if (file != null)
            {
                ret.FileModel = file;
            }

            return new CommandResponse<UserModel>(ret);
        }

        public async Task<ICommandResponse> HandleAsync(UpdateUserRole command, CancellationToken cancellationToken = default)
        {
            await _userService.UpdateUserRoleAsync(command);
            return CommandResponse.SuccessCommand;

        }

        public async Task<ICommandResponse> HandleAsync(CreateUserRole command, CancellationToken cancellationToken = default)
        {
            await _userService.CreateUserRoleAsync(command);
            return CommandResponse.SuccessCommand;
        }

        public async Task<ICommandResponse> HandleAsync(DeleteUserRole command, CancellationToken cancellationToken = default)
        {
            await _userService.DeleteUserRoleAsync(command);
            return CommandResponse.SuccessCommand;
        }

        public async Task<ICommandResponse> HandleAsync(GetTrainingCertification command, CancellationToken cancellationToken = default)
        {
            var ret = await _userService.GetTrainingCertificationAsync(command);
            return new CommandResponse<IEnumerable<TrainingCertificationView>>(ret);
        }
    }
}
