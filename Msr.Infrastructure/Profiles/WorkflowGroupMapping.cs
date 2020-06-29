using AutoMapper;
using MSR.Domain.Commands.Workflow;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Profiles
{
    public class WorkflowGroupMapping : Profile
    {
        public WorkflowGroupMapping()
        {
            CreateMap<CreateWorkflowGroupModel, WorkflowGroup>();
            CreateMap<UpdateWorkflowGroupModel, WorkflowGroup>();
            CreateMap<Domain.Models.WorkflowGroupRoleMapModel, WorkflowGroupRoleMap>();

            CreateMap<WorkflowGroup, Domain.Models.WorkflowGroupModel>()
                .ForMember(dest =>
            dest.CreatedByName,
            opt => opt.MapFrom(src => src.Created.GetFullName()))
                .ForMember(dest =>
            dest.LastUpdatedByName,
            opt => opt.MapFrom(src => src.LastUpdated.GetFullName()));

            CreateMap<WorkflowGroupRoleMap, Domain.Models.WorkflowGroupRoleMapModel>()
                .ForMember(dest =>
            dest.Name,
            opt => opt.MapFrom(src => src.Role.Name));
        }
    }
}
