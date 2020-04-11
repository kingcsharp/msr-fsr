using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Infrastructure.Profiles
{
    public class InfrastructureMappingProfiles: Profile
    {
        public InfrastructureMappingProfiles()
        {
            CreateMap<Resources.EntityFramework.Entities.User, Domain.Models.User>();
        }
    }
}
