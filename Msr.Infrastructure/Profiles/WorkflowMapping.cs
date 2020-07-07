using AutoMapper;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Profiles
{
    public class WorkflowMapping : Profile
    {
        public WorkflowMapping()
        {
            CreateMap<CreateWorkflowModel, Workflow>()
                .ForMember(dest => dest.ActivityMaps, opt => opt.MapFrom(src => src.ActivityMaps))
                .ForMember(dest => dest.MemberStages, opt => opt.MapFrom(src => src.MemberStages));


            CreateMap<UpdateWorkflowModel, Workflow>();
            CreateMap<WorkflowActivityModel, WorkflowActivity>();

            CreateMap<WorkflowActivityMapModel, WorkflowActivityMap>();
            CreateMap<WorkflowStageMapModel, WorkflowStageMap>();

            CreateMap<Workflow, WorkflowModel>()
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Created.GetFullName()))
                .ForMember(dest => dest.LastUpdatedByName, opt => opt.MapFrom(src => src.LastUpdated.GetFullName()))
                .ForMember(dest => dest.ActivityMaps, opt => opt.MapFrom(src => src.ActivityMaps))
                .ForMember(dest => dest.MemberStages, opt => opt.MapFrom(src => src.MemberStages));

            CreateMap<WorkflowActivityMap, WorkflowActivityMapModel>();
            CreateMap<WorkflowStageMap, WorkflowStageMapModel>();
            CreateMap<WorkflowActivity, WorkflowActivityModel>();
        }
    }
}
