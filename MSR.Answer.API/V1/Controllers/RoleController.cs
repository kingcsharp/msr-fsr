using System;
using Microsoft.AspNetCore.Mvc;
using MSR.Answer.API.Attributes;
using MSR.Domain.Commanding.Abstractions;
using MSR.Domain.Commands;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSR.Domain.Models;
using MSR.Answer.API.V1.Models;
using MSR.Answer.API.V1.Extentions;
using MSR.Domain.Commanding.Enums;
using MSR.Answer.API.Filters;
using MSR.Domain.Views;

namespace MSR.Answer.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [VersionedRoute("[controller]")]
    public class RoleController : BaseApiController
    {
        private ICommandDispatcher _dispatcher;

        public RoleController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }


        [HttpGet, SwaggerResponse(typeof(AuditActionResult<ICollection<Role>>))]
        [HasPrivilegeApi("RoleModulePermission", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> GetRoles()
        {
            var ret = await _dispatcher.DispatchAsync(new GetRoles());
            return ret.ToOkObjectResponse<ICollection<Role>>();
        }

        [HttpGet("Roles"), SwaggerResponse(typeof(AuditActionResult<ICollection<RoleView>>))]
        [HasPrivilegeApi("Roles", EnumPrivilege.CanRead)]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(new AuditActionResult<ICollection<RoleView>>()
            {
                Object = new List<RoleView>()
                {
                    new RoleView()
                    {
                        Created = new UserModel(){FirstName = "Pedro",LastName = "John",CreatedOn = DateTime.UtcNow},
                        LastUpdated = new UserModel(){FirstName = "Pedro",LastName = "John",CreatedOn = DateTime.UtcNow},
                        HasAssignedUsers = false,
                        Id = 1,
                        IsCertificationRole = true,
                        Name = "SuperPotatoe",
                        ParentRoles = new List<RoleView>()
                        {
                            new RoleView()
                            {
                                Created = new UserModel(){FirstName = "Pedro2",LastName = "John2"},
                                LastUpdated = new UserModel(){FirstName = "Pedro",LastName = "John",CreatedOn = DateTime.UtcNow},
                                HasAssignedUsers = false,
                                Id = 2,
                                IsCertificationRole = true,
                                Name = "SuperPotatoe2",
                                ParentRoles = new List<RoleView>()
                            },new RoleView()
                            {
                                Created = new UserModel(){FirstName = "Pedro4",LastName = "John4"},
                                LastUpdated = new UserModel(){FirstName = "Pedro",LastName = "John",CreatedOn = DateTime.UtcNow},
                                HasAssignedUsers = false,
                                Id = 3,
                                IsCertificationRole = true,
                                Name = "SuperPotatoe4",
                                ParentRoles = new List<RoleView>()
                            },
                            new RoleView()
                            {
                                Created = new UserModel(){FirstName = "Pedro",LastName = "John"},
                                HasAssignedUsers = false,
                                Id = 5,
                                IsCertificationRole = true,
                                Name = "SuperAdmin",
                                ParentRoles = new List<RoleView>()
                                {
                                    new RoleView()
                                    {
                                        Created = new UserModel(){FirstName = "ABC",LastName = "OHG"},
                                        HasAssignedUsers = false,
                                        Id = 6,
                                        IsCertificationRole = true,
                                        Name = "Morungan",
                                        ParentRoles = new List<RoleView>()
                                    }
                                }
                            }
                        }
                    },
                    new RoleView()
                    {
                        Created = new UserModel(){FirstName = "Pedro",LastName = "John"},
                        HasAssignedUsers = false,
                        Id = 5,
                        IsCertificationRole = true,
                        Name = "SuperAdmin",
                        ParentRoles = new List<RoleView>()
                        {
                            new RoleView()
                            {
                                Created = new UserModel(){FirstName = "ABC",LastName = "OHG"},
                                HasAssignedUsers = false,
                                Id = 6,
                                IsCertificationRole = true,
                                Name = "Morungan",
                                ParentRoles = new List<RoleView>()
                            }
                        }
                    },
                    new RoleView()
                    {
                        Created = new UserModel(){FirstName = "Pedro2",LastName = "John2"},
                        LastUpdated = new UserModel(){FirstName = "Pedro",LastName = "John",CreatedOn = DateTime.UtcNow},
                        HasAssignedUsers = false,
                        Id = 2,
                        IsCertificationRole = true,
                        Name = "SuperPotatoe2",
                        ParentRoles = new List<RoleView>()
                    },new RoleView()
                    {
                        Created = new UserModel(){FirstName = "Pedro4",LastName = "John4"},
                        LastUpdated = new UserModel(){FirstName = "Pedro",LastName = "John",CreatedOn = DateTime.UtcNow},
                        HasAssignedUsers = false,
                        Id = 3,
                        IsCertificationRole = true,
                        Name = "SuperPotatoe4",
                        ParentRoles = new List<RoleView>()
                    },
                }
            });
        }

        [HttpPost, SwaggerResponse(typeof(AuditActionResult<RoleView>))]
        [HasPrivilegeApi("Roles", EnumPrivilege.CanCreate)]
        public async Task<IActionResult> Post(CreateRoleRequest request)
        {
            return new OkObjectResult(new AuditActionResult<RoleView>()
            {
                SuccessMessage = "Role Successfully Created",
                Object = new RoleView()
                {
                    Created = new UserModel() { FirstName = "aaPedro", LastName = "Jaaaohn" },
                    HasAssignedUsers = false,
                    Id = 8,
                    IsCertificationRole = true,
                    Name = "SuperPotatoe",
                    ParentRoles = null
                }
            });
        }

        [HttpPatch, SwaggerResponse(typeof(AuditActionResult<RoleView>))]
        [HasPrivilegeApi("Roles", EnumPrivilege.CanEdit)]
        public async Task<IActionResult> Patch(UpdateRoleRequest request)
        {
            return new OkObjectResult(new AuditActionResult<RoleView>()
            {
                SuccessMessage = "Role Successfully Updated",
                Object = new RoleView()
                {
                    Created = new UserModel() { FirstName = "Pedrooo", LastName = "Johnooo", CreatedOn = DateTime.UtcNow },
                    LastUpdated = new UserModel() { FirstName = "Pedro", LastName = "John", CreatedOn = DateTime.UtcNow },
                    HasAssignedUsers = false,
                    Id = 1,
                    IsCertificationRole = true,
                    Name = "SuperPotatoe",
                    ParentRoles = new List<RoleView>()
                    {
                        new RoleView()
                        {
                            Created = new UserModel(){FirstName = "Pedro2",LastName = "John2"},
                            LastUpdated = new UserModel(){FirstName = "Pedro",LastName = "John",CreatedOn = DateTime.UtcNow},
                            HasAssignedUsers = false,
                            Id = 2,
                            IsCertificationRole = true,
                            Name = "SuperPotatoe2",
                            ParentRoles = null
                        },new RoleView()
                        {
                            Created = new UserModel(){FirstName = "Pedro4",LastName = "John4"},
                            LastUpdated = new UserModel(){FirstName = "Pedro",LastName = "John",CreatedOn = DateTime.UtcNow},
                            HasAssignedUsers = false,
                            Id = 3,
                            IsCertificationRole = true,
                            Name = "SuperPotatoe4",
                            ParentRoles = null
                        }
                    }
                }
            });
        }

        [HttpDelete("{id}"), SwaggerResponse(typeof(AuditActionResult<Role>))]
        [HasPrivilegeApi("Roles", EnumPrivilege.CanDelete)]
        public async Task<IActionResult> Delete(int id)
        {
            return new OkObjectResult(new AuditActionResult()
            {
                SuccessMessage = "Role Successfully Removed"
            });
        }
    }
}