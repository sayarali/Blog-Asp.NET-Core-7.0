using System;
using Blog.Entity.Entities;

namespace Blog.Service.Services.Abstracts
{
	public interface IRoleService
	{
        Task<List<TRole>> GetAllRolesAsync();
        Task<TRole> GetRoleByIdAsync(Guid roleId);
    }
}

