using AutoMapper;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Infrastructure.Profiles
{
    public class WorkflowStageMapping : Profile
    {
        public WorkflowStageMapping()
        {
            CreateMap<CreateWorkflowStageModel, WorkflowStage>();
            CreateMap<UpdateWorkflowStageModel, WorkflowStage>();
            CreateMap<WorkflowGroupStageMapModel, WorkflowGroupStageMap>();

            CreateMap<WorkflowStage, WorkflowStageModel>()
                .ForMember(dest =>
            dest.CreatedByName,
            opt => opt.MapFrom(src => src.Created.GetFullName()))
                .ForMember(dest =>
            dest.LastUpdatedByName,
            opt => opt.MapFrom(src => src.LastUpdated.GetFullName()));


            CreateMap<WorkflowGroupStageMap, WorkflowGroupStageMapModel>();


        }
    }
}
