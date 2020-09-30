using AutoMapper;
using MSR.Domain.Commands;
using MSR.Domain.Models;

namespace MSR.Application.Profiles
{
    public class ApplicationMappingProfiles: Profile
    {
        public ApplicationMappingProfiles()
        {
            CreateMap<CreateFile, FileModel>();
        }
    }
}
