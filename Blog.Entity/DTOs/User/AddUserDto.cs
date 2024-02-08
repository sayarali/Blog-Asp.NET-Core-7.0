using System;
using Blog.Entity.Entities;

namespace Blog.Entity.DTOs.User
{
	public class AddUserDto
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public List<TRole>? Roles { get; set; }
    }
}

