using AutoMapper;
using MSR.Domain.Commands;
using MSR.Domain.Models;
using MSR.Domain.Views;

namespace MSR.Application.Profiles
{
    public class ApplicationMappingProfiles: Profile
    {
        public ApplicationMappingProfiles()
        {
            CreateMap<CreateFile, FileModel>();
            CreateMap<dynamic, InvoiceableWorkOrderView>().ReverseMap();
        }
    }
}
