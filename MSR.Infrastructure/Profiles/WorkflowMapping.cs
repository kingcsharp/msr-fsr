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

            CreateMap<PendingApprovalModel, ApprovalEntity>();
            CreateMap<PendingApprovalModel, UserApproval>();

            CreateMap<UpdateWorkflowModel, Workflow>();
            CreateMap<WorkflowActivityModel, WorkflowActivity>();

            CreateMap<WorkflowActivityMapModel, WorkflowActivityMap>();
            CreateMap<WorkflowStageMapModel, WorkflowStageMap>();

            CreateMap<Workflow, WorkflowModel>()
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Created.GetFullName()))
                .ForMember(dest => dest.LastUpdatedByName, opt => opt.MapFrom(src => src.LastUpdated.GetFullName()))
                .ForMember(dest => dest.ActivityMaps, opt => opt.MapFrom(src => src.ActivityMaps))
                .ForMember(dest => dest.MemberStages, opt => opt.MapFrom(src => src.MemberStages));

            CreateMap<WorkflowActivityMap, WorkflowActivityMapModel>()
                .ForMember(dest => dest.WorkflowActivityName, opt => opt.MapFrom(src => src.WorkflowActivity.Name));

            CreateMap<WorkflowStageMap, WorkflowStageMapModel>()
                .ForMember(dest => dest.WorkflowStageName, opt => opt.MapFrom(src => src.WorkflowStage.Name));

            CreateMap<WorkflowActivity, WorkflowActivityModel>();

            CreateMap<ApprovalEntity, PendingApprovalModel>()
                .ForMember(dest => dest.WorkflowCreatedByName, opt => opt.MapFrom(src => src.Workflow.Created.GetFullName()))
                .ForMember(dest => dest.WorkflowGroupId, opt => opt.MapFrom(src => src.WorkflowGroupId))
                .ForMember(dest => dest.WorkflowGroupName, opt => opt.MapFrom(src => src.WorkflowGroup.Name))
                .ForMember(dest => dest.WorkflowId, opt => opt.MapFrom(src => src.WorkflowId))
                .ForMember(dest => dest.WorkflowName, opt => opt.MapFrom(src => src.Workflow.Name))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Created.GetFullName()))
                .ForMember(dest => dest.ActivityType, opt => opt.MapFrom(src=>src.ActivityType));

            CreateMap<UserApproval, PendingApprovalModel>()
                .ForMember(dest => dest.WorkflowCreatedByName, opt => opt.MapFrom(src => src.Workflow.Created.GetFullName()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetFullName()))
                .ForMember(dest => dest.WorkflowGroupName, opt => opt.MapFrom(src => src.WorkflowGroup.Name))
                .ForMember(dest => dest.WorkflowName, opt => opt.MapFrom(src => src.Workflow.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.CreatedByName, opt => opt.MapFrom(src => src.Created.GetFullName()));
        }
    }
}
