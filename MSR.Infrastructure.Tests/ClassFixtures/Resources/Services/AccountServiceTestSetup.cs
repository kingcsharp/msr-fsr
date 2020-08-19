using Microsoft.Extensions.DependencyInjection;
using MSR.Infrastructure.Resources.Services.Account;
using MSR.Infrastructure.Resources.Services.Workflow;
using Moq;
using MSR.Domain.Abstractions.Services;
using MSR.Domain.Abstractions.Services.Workflow;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Interfaces;
using System.Linq.Expressions;
using System;
using MSR.Infrastructure.Tests.TestFixtures;
using AutoMapper;
using MSR.Infrastructure.Profiles;
using MSR.Domain.Abstractions.Email;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using MSR.Domain.Models.Config;
using System.Linq;
using MSR.Infrastructure.Helpers.Abstractions;
using MockQueryable.Moq;

namespace MSR.Infrastructure.Tests.ClassFixtures.Resources.Services
{
    public class AccountServiceTestSetup
    {
        public ServiceProvider ServiceProvider { get; }

        public AccountServiceTestSetup()
        {
            Mock<IUnitOfWork> mockUnitOfWork = DatabaseFake.DatabaseFakeSetup();
            ServiceProvider = DatabaseFake
                .ServiceFakeSetup(mockUnitOfWork.Object)
                .BuildServiceProvider();
        }
    }
}
