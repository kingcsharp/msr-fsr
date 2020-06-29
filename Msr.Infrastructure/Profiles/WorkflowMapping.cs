using AutoMapper;
using MSR.Domain.Commands.Workflow;
using MSR.Domain.Models.Workflow;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Profiles
{
    public class WorkflowMapping : Profile
    {
        public WorkflowMapping()
        {
            CreateMap<CreateWorkflowModel, Workflow>();
            CreateMap<UpdateWorkflowModel, Workflow>();
            CreateMap<WorkflowActivityMapModel, WorkflowActivityMap>();
            CreateMap<WorkflowStageMapModel, WorkflowStageMap>();

            CreateMap<Workflow, WorkflowModel>()
                .ForMember(dest =>
            dest.CreatedByName,
            opt => opt.MapFrom(src => src.Created.GetFullName()))
                .ForMember(dest =>
            dest.LastUpdatedByName,
            opt => opt.MapFrom(src => src.LastUpdated.GetFullName()));

            CreateMap<WorkflowActivityMap, WorkflowActivityMapModel>();
            CreateMap<WorkflowStageMap, WorkflowStageMapModel>();

        }
    }
}
