using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using MSR.Domain.Abstractions.Services;
using MSR.Infrastructure.Profiles;
using MSR.Infrastructure.Resources.EntityFramework.Application;
using MSR.Infrastructure.Resources.EntityFramework.Entities;
using MSR.Infrastructure.Resources.EntityFramework.Interfaces;
using MSR.Infrastructure.Resources.Services.Role;
using MSR.Infrastructure.Tests.TestFixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MSR.Infrastructure.Tests.ClassFixtures.Resources.Services
{
    public class RoleServiceTestSetup
    {
        public ServiceProvider ServiceProvider { get; }

        public RoleServiceTestSetup()
        {
            var fake = new Bogus.Faker();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockIRepositoryMenuRole = new Mock<IRepository<MenuRole>>();
            var mockIRepositoryMenuRolePermission = new Mock<IRepository<MenuRolePermission>>();
            var mockIRepositoryRole = new Mock<IRepository<Role>>();
            var mockIRepositoryMenuItem = new Mock<IRepository<MenuItem>>();

            Expression<Func<MenuRole, bool>> successTestExpression = i => i.MenuItemId == CreateMenuRoleMapFixture.SuccessCreateMenuRoleMap.MenuId 
                                                                        && i.RoleId == CreateMenuRoleMapFixture.SuccessCreateMenuRoleMap.RoleId;
            Expression<Func<MenuRole, bool>> existingTestExpression = i => i.MenuItemId == CreateMenuRoleMapFixture.SuccessExistingCreateMenuRoleMap.MenuId 
                                                                        && i.RoleId == CreateMenuRoleMapFixture.SuccessExistingCreateMenuRoleMap.RoleId;

            mockIRepositoryMenuRole.Setup(m => m.FirstOrDefaultAsync(It.IsAny<bool>(),
                                        It.Is<Expression<Func<MenuRole, bool>>>(i => i.ToString() == successTestExpression.ToString()),
                                        It.IsAny<Expression<Func<MenuRole, object>>[]>()))
                .ReturnsAsync((MenuRole)null);
            mockIRepositoryMenuRole.Setup(m => m.FirstOrDefaultAsync(It.IsAny<bool>(),
                                        It.Is<Expression<Func<MenuRole, bool>>>(i => i.ToString() == existingTestExpression.ToString()),
                                        It.IsAny<Expression<Func<MenuRole, object>>[]>()))
                 .ReturnsAsync(MenuRoleFixture.ExistingMenuRole);
            mockIRepositoryMenuRolePermission.Setup(m => m.FirstOrDefaultAsync(It.IsAny<bool>(),
                                        It.IsAny<Expression<Func<MenuRolePermission, bool>>>(),
                                        It.IsAny<Expression<Func<MenuRolePermission, object>>[]>()))
                .ReturnsAsync(MenuRolePermissionFixture.FullMenuRolePermission);
            mockIRepositoryRole.Setup(m => m.FirstOrDefaultAsync(It.IsAny<bool>(),
                                        It.IsAny<Expression<Func<Role, bool>>>(),
                                        It.IsAny<Expression<Func<Role, object>>[]>()))
                .ReturnsAsync(RoleFixture.SuccessRole);
            mockIRepositoryMenuItem.Setup(m => m.FirstOrDefaultAsync(It.IsAny<bool>(),
                                        It.IsAny<Expression<Func<MenuItem, bool>>>(), 
                                        It.IsAny<Expression<Func<MenuItem, object>>[]>()))
                .ReturnsAsync(MenuItemFixture.SuccessMenuItem);
            mockIRepositoryMenuRole.Setup(m => m.AddAsync(It.IsAny<MenuRole>()))
                .Callback((MenuRole i) => { i.Id = MenuRoleFixture.SuccessMenuRole.Id; })
                .ReturnsAsync((EntityEntry<MenuRole>)null);

            mockIRepositoryMenuRole.Setup(m => m.Query())
                .Returns(new List<MenuRole>() { MenuRoleFixture.SuccessMenuRole, MenuRoleFixture.ExistingMenuRole }.AsQueryable());
            mockIRepositoryMenuRolePermission.Setup(m => m.Query())
                .Returns(new List<MenuRolePermission>() { MenuRolePermissionFixture.FullMenuRolePermission }.AsQueryable());
            mockIRepositoryRole.Setup(m => m.Query())
                .Returns(new List<Role>() { RoleFixture.SuccessRole }.AsQueryable());
            mockIRepositoryMenuItem.Setup(m => m.Query())
                .Returns(new List<MenuItem>() { MenuItemFixture.SuccessMenuItem }.AsQueryable());

            mockUnitOfWork.SetupGet(m => m.MenuRoles)
                .Returns(mockIRepositoryMenuRole.Object);
            mockUnitOfWork.SetupGet(m => m.MenuRolePermissions)
                .Returns(mockIRepositoryMenuRolePermission.Object);
            mockUnitOfWork.SetupGet(m => m.Roles)
                .Returns(mockIRepositoryRole.Object);
            mockUnitOfWork.SetupGet(m => m.MenuItems)
                .Returns(mockIRepositoryMenuItem.Object);


            var services = new ServiceCollection()
            .AddLogging()
            .AddScoped<IRoleService, RoleService>()
            .AddAutoMapper(typeof(InfrastructureMappingProfiles))
            .AddSingleton(mockIRepositoryMenuRole.Object)
            .AddSingleton(mockIRepositoryMenuRolePermission.Object)
            .AddSingleton(mockIRepositoryRole.Object)
            .AddSingleton(mockIRepositoryMenuItem.Object)
            .AddSingleton(mockUnitOfWork.Object);
           
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
