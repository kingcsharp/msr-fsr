using AutoMapper;
using MSR.Domain.Abstractions.Email;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Commanding.Enums;
using MSR.Domain.Commands;
using MSR.Domain.Exceptions;
using MSR.Domain.Models.Config;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSR.Infrastructure.Resources.Services.Help
{
    public class HelpService : IHelpService
    {
        IEmailService _emailService;
        IMapper _mapper;
        EmailInformation _emailInformation;
        IUnitOfWork _unitOfWork;

        public HelpService(IEmailService emailService, IMapper mapper, EmailInformation emailInformation, IUnitOfWork unitOfWork)
        {
            _emailService = emailService;
            _mapper = mapper;
            _emailInformation = emailInformation;
            _unitOfWork = unitOfWork;
        }

        public async Task<Domain.Models.HelpPage> CreateHelpPage(CreateHelpPage command)
        {
            var helpPage = new EntityFramework.Entities.HelpPage()
            {
                Content = command.HelpContent,
                FriendlyUrl = command.FriendlyURL,
                Title = command.Title
            };

            await _unitOfWork.HelpPages.AddAsync(helpPage);
            await _unitOfWork.SaveChangesAsync();

            foreach(var roleId in command.RoleIds)
            {
                var role = await _unitOfWork.Roles.FirstOrDefaultAsync(false, i => i.Id == roleId);
                if (role is null) continue;
                var helpPageRole = new HelpPageRoleMap()
                {
                    HelpPage = helpPage,
                    HelpPageId = helpPage.Id,
                    Role = role,
                    RoleId = role.Id
                };

                await _unitOfWork.HelpPageRoles.AddAsync(helpPageRole);
            }

            await _unitOfWork.SaveChangesAsync();

            var domHelpPage = _mapper.Map<Domain.Models.HelpPage>(helpPage);
            domHelpPage.Roles = helpPage.Roles.Select(i => _mapper.Map<Domain.Models.Role>(i.Role)).ToList();
            return domHelpPage;
        }

        public async Task<Domain.Models.HelpPage> CreateHelpPageRole(CreateHelpPageRole command)
        {
            var helpPage = await _unitOfWork.HelpPages.FirstOrDefaultAsync(false, i => i.Id == command.HelpPageId);
            var role = await _unitOfWork.Roles.FirstOrDefaultAsync(false, i => i.Id == command.RoleId);


            if(helpPage is null || role is null)
            {
                throw new DomainException($"{nameof(Domain.Models.HelpPage)} or {nameof(Role)} not found");
            }

            var helpPageRole = new HelpPageRoleMap()
            {
                HelpPage = helpPage,
                HelpPageId = helpPage.Id,
                Role = role,
                RoleId = role.Id
            };

            await _unitOfWork.HelpPageRoles.AddAsync(helpPageRole);
            await _unitOfWork.SaveChangesAsync();

            var domHelpPage = _mapper.Map<Domain.Models.HelpPage>(helpPage);
            domHelpPage.Roles = helpPage.Roles.Select(i => _mapper.Map<Domain.Models.Role>(i.Role)).ToList();
            return domHelpPage;
        }

        public async Task DeleteHelpPage(DeleteHelpPage command)
        {
            var helpPage = await _unitOfWork.HelpPages.FirstOrDefaultAsync(false, i => i.Id == command.HelpPageId);

            if(helpPage is null)
            {
                throw new DomainException($"{nameof(Domain.Models.HelpPage)} not found with ID {command.HelpPageId}", DomainError.NotFound);
            }

            var helpPageRoles = helpPage.Roles;

            foreach(var helpPageRole in helpPageRoles ?? new List<HelpPageRoleMap>())
            {
                _unitOfWork.HelpPageRoles.Delete(false,helpPageRole);
            }

            _unitOfWork.HelpPages.Delete(false, helpPage);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteHelpPageRole(DeleteHelpPageRole command)
        {
            var helpPageRole = await _unitOfWork.HelpPageRoles.FirstOrDefaultAsync(false, i => i.Id == command.HelpPageRoleId);
            if (helpPageRole is null)
            {
                throw new DomainException($"{nameof(Domain.Models.HelpPage)} not found with ID: {command.HelpPageRoleId}");
            }

            _unitOfWork.HelpPageRoles.Delete(false, helpPageRole);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<Domain.Models.HelpPage>> GetHelpPages(GetHelpPage command)
        {
            var helpPages = _unitOfWork.HelpPages.Query();

            if (command.Id.HasValue)
            {
                helpPages = helpPages.Where(i => i.Id == command.Id.Value);
            }

            var pageList = new List<Domain.Models.HelpPage>();

            foreach(var helpPage in helpPages.ToList())
            {
                var page = _mapper.Map<Domain.Models.HelpPage>(helpPage);

                page.Roles = helpPage.Roles.Select(i => _mapper.Map<Domain.Models.Role>(i.Role)).ToList();

                pageList.Add(page);
            }

            return pageList;
        }

        public async Task UpdateHelpPage(UpdateHelpPage command)
        {
            var helpPage = await _unitOfWork.HelpPages.FirstOrDefaultAsync(false, i => i.Id == command.HelpPageId);
            if (helpPage is null)
            {
                throw new DomainException($"{nameof(Domain.Models.HelpPage)} not found with ID {command.HelpPageId}", DomainError.NotFound);
            }

            helpPage.Content = command.HelpContent ?? helpPage.Content;
            helpPage.FriendlyUrl = command.FriendlyURL ?? helpPage.FriendlyUrl;
            helpPage.Title = command.Title ?? helpPage.Title;

            _unitOfWork.HelpPages.Update(helpPage);
            await _unitOfWork.SaveChangesAsync();

            var curHelpPageRoles = helpPage.Roles.Select(i => i.RoleId).ToList();
            var helpPageRolesToAdd = command.RoleIds.Where(i => !curHelpPageRoles.Contains(i)).ToList();
            var helpPageRolesToRemoveId = curHelpPageRoles.Where(i => !command.RoleIds.Contains(i)).ToList();
            var helpPageRolesToRemove = _unitOfWork.HelpPageRoles.Query().Where(i => i.HelpPageId == command.HelpPageId && helpPageRolesToRemoveId.Contains(i.RoleId)).ToList();

            foreach(var addRole in helpPageRolesToAdd)
            {
                var role = await _unitOfWork.Roles.FirstOrDefaultAsync(false, i => i.Id == addRole);

                if (role is null) continue;

                _unitOfWork.HelpPageRoles.Add(new HelpPageRoleMap()
                {
                    HelpPage = helpPage,
                    HelpPageId = helpPage.Id,
                    Role = role,
                    RoleId = role.Id,
                });
            }

            foreach(var removeRole in (helpPageRolesToRemove))
            {
                _unitOfWork.HelpPageRoles.Delete(false,removeRole);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
