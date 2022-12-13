using AutoMapper;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;
using MSR.Infrastructure.Resources.EntityFramework.Entities;

namespace MSR.Application.Profiles
{
    public class ApplicationMappingProfiles: Profile
    {
        public ApplicationMappingProfiles()
        {
            CreateMap<CreateFile, FileModel>();
            CreateMap<dynamic, InvoiceableWorkOrderView>().ReverseMap();
            CreateMap<ProductModel,ProductDownloadView>()
                .ForMember(dest => dest.CustomerName, opts => opts.MapFrom(src => src.Customer == null ? null : src.Customer.Name))
                .ForMember(dest => dest.ProcedureName, opts => opts.MapFrom(src => src.Procedure == null ? null : src.Procedure.Name))
                .ForMember(dest => dest.PartName, opts => opts.MapFrom(src => src.Part == null ? null : src.Part.Name))
                .ForMember(dest => dest.PartKitNo, opts => opts.MapFrom(src => src.Part == null ? null : src.Part.PartNumber))
                .ReverseMap();
            CreateMap<SubPart, SubPartModel>()
                .ForMember(dest => dest.Qty, opts => opts.MapFrom(src => src.Qty.HasValue ? src.Qty : 1))
                .ForMember(dest => dest.CycleCount, opts => opts.MapFrom(src => src.CycleCount.HasValue ? src.CycleCount.Value : 0));
            CreateMap<QuoteImportItem, CreateProduct>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.DivisionFab, opts => opts.MapFrom(src => src.DivisionFab))
                .ForMember(dest => dest.PartId, opts => opts.MapFrom(src => src.PartId))
                .ForMember(dest => dest.ProcedureId, opts => opts.MapFrom(src => src.ProcedureId))
                .ForMember(dest => dest.Revision, opts => opts.MapFrom(src => src.Revision))
                .ForMember(dest => dest.TotalSalePrice, opts => opts.MapFrom(src => src.TotalSalePrice))
                .ForMember(dest => dest.CycleTime, opts => opts.MapFrom(src => src.CycleTime))
                ;    
        }
    }
}
