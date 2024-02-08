using System;
using Blog.Entity.Entities;
using Blog.Service.Services.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Blog.Service.Services.Concretes
{
	public class RoleService : IRoleService
	{
        private readonly RoleManager<TRole> roleManager;

        public RoleService(RoleManager<TRole> roleManager)
		{
            this.roleManager = roleManager;
        }

        public async Task<List<TRole>> GetAllRolesAsync()
        {
            return roleManager.Roles.ToList();
        }

        public async Task<TRole> GetRoleByIdAsync(Guid roleId)
        {
            return await roleManager.FindByIdAsync(roleId.ToString());
        }
	}
}

